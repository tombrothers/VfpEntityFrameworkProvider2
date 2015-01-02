using System.Diagnostics;

namespace VfpEntityFrameworkProvider.SqlGeneration {
    /// <summary>
    /// The SymbolPair exists to solve the record flattening problem.
    /// <see cref="SqlGenerator.Visit(VfpPropertyExpression)"/>
    /// Consider a property expression D(v, "j3.j2.j1.a.x")
    /// where v is a VarRef, j1, j2, j3 are joins, a is an extent and x is a columns.
    /// This has to be translated eventually into {j'}.{x'}
    /// 
    /// The source field represents the outermost SqlStatement representing a join
    /// expression (say j2) - this is always a Join symbol.
    /// 
    /// The column field keeps moving from one join symbol to the next, until it
    /// stops at a non-join symbol.
    /// 
    /// This is returned by <see cref="SqlGenerator.Visit(VfpPropertyExpression)"/>,
    /// but never makes it into a SqlBuilder.
    /// </summary>
    internal class SymbolPair : SqlFragmentBase {
        public Symbol Source;
        public Symbol Column;

        public SymbolPair(Symbol source, Symbol column)
            : base(SqlFragmentType.SymbolPair) {
            Source = source;
            Column = column;
        }

        public override void WriteSql(SqlWriter writer, SqlVisitor visitor) {
            // Symbol pair should never be part of a SqlBuilder.
            Debug.Assert(false);
        }
    }
}