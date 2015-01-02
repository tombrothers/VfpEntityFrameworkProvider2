using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Diagnostics;

namespace VfpEntityFrameworkProvider.SqlGeneration {
    /// <summary>
    /// A SqlSelectStatement represents a canonical SQL SELECT statement.
    /// It has fields for the 5 main clauses
    /// <list type="number">
    /// <item>SELECT</item>
    /// <item>FROM</item>
    /// <item>WHERE</item>
    /// <item>GROUP BY</item>
    /// <item>ORDER BY</item>
    /// </list>
    /// We do not have HAVING, since it does not correspond to anything in the DbCommandTree.
    /// Each of the fields is a SqlBuilder, so we can keep appending SQL strings
    /// or other fragments to build up the clause.
    ///
    /// We have a IsDistinct property to indicate that we want distict columns.
    /// This is given out of band, since the input expression to the select clause
    /// may already have some columns projected out, and we use append-only SqlBuilders.
    /// The DISTINCT is inserted when we finally write the object into a string.
    /// 
    /// Also, we have a Top property, which is non-null if the number of results should
    /// be limited to certain number. It is given out of band for the same reasons as DISTINCT.
    ///
    /// The FromExtents contains the list of inputs in use for the select statement.
    /// There is usually just one element in this - Select statements for joins may
    /// temporarily have more than one.
    ///
    /// If the select statement is created by a Join node, we maintain a list of
    /// all the extents that have been flattened in the join in AllJoinExtents
    /// <example>
    /// in J(j1= J(a,b), c)
    /// FromExtents has 2 nodes JoinSymbol(name=j1, ...) and Symbol(name=c)
    /// AllJoinExtents has 3 nodes Symbol(name=a), Symbol(name=b), Symbol(name=c)
    /// </example>
    ///
    /// If any expression in the non-FROM clause refers to an extent in a higher scope,
    /// we add that extent to the OuterExtents list.  This list denotes the list
    /// of extent aliases that may collide with the aliases used in this select statement.
    /// It is set by <see cref="SqlGenerator.Visit(VfpVariableReferenceExpression)"/>.
    /// An extent is an outer extent if it is not one of the FromExtents.
    ///
    ///
    /// </summary>
    internal class SqlSelectStatement : SqlFragmentBase {
        internal SqlBuilder Select { get; private set; }
        internal SqlBuilder From { get; private set; }
        internal bool IsDistinct { get; set; }
        internal List<Symbol> AllJoinExtents { get; set; }

        #region FromExtents

        private List<Symbol> _fromExtents;

        internal List<Symbol> FromExtents {
            get { return _fromExtents ?? (_fromExtents = new List<Symbol>()); }
        }

        #endregion

        #region OuterExtents

        private Dictionary<Symbol, bool> _outerExtents;

        internal Dictionary<Symbol, bool> OuterExtents {
            get { return _outerExtents ?? (_outerExtents = new Dictionary<Symbol, bool>()); }
        }

        #endregion

        #region Top

        private TopClause _top;

        internal TopClause Top {
            get {
                return _top;
            }
            set {
                Debug.Assert(_top == null, "SqlSelectStatement.Top has already been set");
                _top = value;
            }
        }

        #endregion

        #region Where

        private SqlBuilder _where;

        internal SqlBuilder Where {
            get { return _where ?? (_where = new SqlBuilder()); }
        }

        #endregion

        #region GroupBy

        private SqlBuilder _groupBy;

        internal SqlBuilder GroupBy {
            get { return _groupBy ?? (_groupBy = new SqlBuilder()); }
        }

        #endregion

        #region OrderBy

        private SqlBuilder _orderBy;

        public SqlBuilder OrderBy {
            get { return _orderBy ?? (_orderBy = new SqlBuilder()); }
        }

        #endregion

        //indicates whether it is the top most select statement, 
        // if not Order By should be omitted unless there is a corresponding TOP
        internal bool IsTopMost { get; set; }

        public SqlSelectStatement(SqlBuilder select = null, SqlBuilder from = null, List<Symbol> fromExtents = null, Dictionary<Symbol, bool> outerExtents = null,
                                    TopClause top = null, SqlBuilder where = null, SqlBuilder groupBy = null, SqlBuilder orderBy = null, bool isDistinct = false,
                                    List<Symbol> allJoinExtents = null)
            : base(SqlFragmentType.SqlSelectStatement) {
            Select = select ?? new SqlBuilder();
            From = from ?? new SqlBuilder();
            _fromExtents = fromExtents;
            _outerExtents = outerExtents;
            _top = top;
            _where = where;
            _groupBy = groupBy;
            _orderBy = orderBy;
            IsDistinct = IsDistinct;
            AllJoinExtents = allJoinExtents;
        }

        public override void WriteSql(SqlWriter writer, SqlVisitor visitor) {
            #region Check if FROM aliases need to be renamed

            // Create a list of the aliases used by the outer extents
            // JoinSymbols have to be treated specially.
            List<string> outerExtentAliases = null;
            if ((null != _outerExtents) && (0 < _outerExtents.Count)) {
                foreach (var outerExtent in _outerExtents.Keys) {
                    var joinSymbol = outerExtent as JoinSymbol;

                    if (joinSymbol != null) {
                        foreach (var symbol in joinSymbol.FlattenedExtentList) {
                            if (null == outerExtentAliases) {
                                outerExtentAliases = new List<string>();
                            }

                            outerExtentAliases.Add(symbol.NewName);
                        }
                    }
                    else {
                        if (null == outerExtentAliases) {
                            outerExtentAliases = new List<string>();
                        }

                        outerExtentAliases.Add(outerExtent.NewName);
                    }
                }
            }

            // An then rename each of the FromExtents we have
            // If AllJoinExtents is non-null - it has precedence.
            // The new name is derived from the old name - we append an increasing int.
            var extentList = AllJoinExtents ?? _fromExtents;

            if (null != extentList) {
                foreach (var fromAlias in extentList) {
                    if ((null != outerExtentAliases) && outerExtentAliases.Contains(fromAlias.Name)) {
                        var i = visitor.AllExtentNames[fromAlias.Name];
                        string newName;

                        do {
                            ++i;
                            newName = fromAlias.Name + i.ToString(System.Globalization.CultureInfo.InvariantCulture);
                        } while (visitor.AllExtentNames.ContainsKey(newName));

                        visitor.AllExtentNames[fromAlias.Name] = i;
                        fromAlias.NewName = newName;

                        // Add extent to list of known names (although i is always incrementing, "prefix11" can
                        // eventually collide with "prefix1" when it is extended)
                        visitor.AllExtentNames[newName] = 0;
                    }

                    // Add the current alias to the list, so that the extents
                    // that follow do not collide with me.
                    if (null == outerExtentAliases) {
                        outerExtentAliases = new List<string>();
                    }

                    outerExtentAliases.Add(fromAlias.NewName);
                }
            }
            #endregion

            // Increase the indent, so that the Sql statement is nested by one tab.
            writer.Indent += 1; // ++ can be confusing in this context

            writer.Write("SELECT ");
            if (IsDistinct) {
                writer.Write("DISTINCT ");
            }

            if (Top != null) {
                Top.WriteSql(writer, visitor);
            }

            if ((null == Select) || Select.IsEmpty) {
                writer.Write("*");
            }
            else {
                Select.WriteSql(writer, visitor);
            }

            writer.WriteLine();
            writer.Write("FROM ");
            From.WriteSql(writer, visitor);

            if ((null != _where) && !Where.IsEmpty) {
                writer.WriteLine();
                writer.Write("WHERE ");
                Where.WriteSql(writer, visitor);
            }

            if ((null != _groupBy) && !GroupBy.IsEmpty) {
                writer.WriteLine();
                writer.Write("GROUP BY ");
                GroupBy.WriteSql(writer, visitor);
            }

            if ((null != _orderBy) && !OrderBy.IsEmpty && (IsTopMost || Top != null)) {
                writer.WriteLine();
                writer.Write("ORDER BY ");
                OrderBy.WriteSql(writer, visitor);
            }

            --writer.Indent;
        }
    }
}