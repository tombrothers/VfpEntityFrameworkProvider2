using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace VfpEntityFrameworkProvider.SqlGeneration {
    /// <summary>
    /// <see cref="SymbolTable"/>
    /// This class represents an extent/nested select statement,
    /// or a column.
    ///
    /// The important fields are Name, Type and NewName.
    /// NewName starts off the same as Name, and is then modified as necessary.
    ///
    ///
    /// The rest are used by special symbols.
    /// e.g. NeedsRenaming is used by columns to indicate that a new name must
    /// be picked for the column in the second phase of translation.
    ///
    /// IsUnnest is used by symbols for a collection expression used as a from clause.
    /// This allows <see cref="SqlGenerator.AddFromSymbol(SqlSelectStatement, string, Symbol, bool)"/> to add the column list
    /// after the alias.
    ///
    /// </summary>
    internal class Symbol : SqlFragmentBase {
        internal Dictionary<string, Symbol> Columns { get; private set; }
        internal bool NeedsRenaming { get; set; }
        internal bool IsUnnest { get; set; }
        internal string Name { get; set; }
        internal string NewName { get; set; }
        internal TypeUsage Type { get; set; }

        public Symbol(string name, TypeUsage type)
            : base(SqlFragmentType.Symbol) {
            Name = name;
            NewName = name;
            Type = type;
            Columns = new Dictionary<string, Symbol>(StringComparer.CurrentCultureIgnoreCase);
        }

        public override void WriteSql(SqlWriter writer, SqlVisitor visitor) {
            if (NeedsRenaming) {
                string newName;

                var i = 0;

                if (visitor.AllColumnNames.ContainsKey(this.NewName)) {
                    i = visitor.AllColumnNames[this.NewName];
                }
                do {
                    ++i;
                    newName = Name + i.ToString(System.Globalization.CultureInfo.InvariantCulture);
                } while (visitor.AllColumnNames.ContainsKey(newName));

                visitor.AllColumnNames[this.NewName] = i;

                // Prevent it from being renamed repeatedly.
                NeedsRenaming = false;
                NewName = newName;

                // Add this column name to list of known names so that there are no subsequent
                // collisions
                visitor.AllColumnNames[newName] = 0;
            }

            writer.Write(NewName);
        }
    }
}