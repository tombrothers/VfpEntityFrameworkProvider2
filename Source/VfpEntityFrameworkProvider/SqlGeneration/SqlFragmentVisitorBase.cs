using System.Collections.Generic;
using System.Linq;

namespace VfpEntityFrameworkProvider.SqlGeneration {
    internal abstract class SqlFragmentVisitorBase {
        public virtual ISqlFragment Visit(ISqlFragment fragment) {
            switch (fragment.SqlFragmentType) {
                case SqlFragmentType.Binary:
                    return Visit((BinaryFragment)fragment);
                case SqlFragmentType.SqlBuilder:
                    return Visit((SqlBuilder)fragment);
                case SqlFragmentType.TopClause:
                    return Visit((TopClause)fragment);
                case SqlFragmentType.Symbol:
                    return Visit((Symbol)fragment);
                case SqlFragmentType.SqlSelectStatement:
                    return Visit((SqlSelectStatement)fragment);
            }

            return fragment;
        }

        public virtual ISqlFragment Visit(SqlSelectStatement fragment) {
            return new SqlSelectStatement(select: (SqlBuilder)fragment.Select.Accept(this),
                                          from: (SqlBuilder)fragment.From.Accept(this),
                                          fromExtents: VisitSymbolList(fragment.FromExtents),
                                          outerExtents: VisitSymbolDictionary(fragment.OuterExtents),
                                          top: VisitTopClause(fragment.Top),
                                          where: (SqlBuilder)fragment.Where.Accept(this),
                                          groupBy: (SqlBuilder)fragment.GroupBy.Accept(this),
                                          orderBy: (SqlBuilder)fragment.OrderBy.Accept(this),
                                          isDistinct: fragment.IsDistinct,
                                          allJoinExtents: VisitSymbolList(fragment.AllJoinExtents));
        }

        private TopClause VisitTopClause(TopClause topClause) {
            if (topClause != null) {
                topClause = (TopClause)topClause.Accept(this);
            }

            return topClause;
        }

        private Dictionary<Symbol, bool> VisitSymbolDictionary(Dictionary<Symbol, bool> list) {
            if (list == null) {
                return null;
            }

            return list.Select(item => new {
                Key = (Symbol)item.Key.Accept(this),
                item.Value
            }).ToDictionary(item => item.Key, item => item.Value);
        }

        private List<Symbol> VisitSymbolList(List<Symbol> list) {
            if (list == null) {
                return null;
            }

            return list.Select(item => (Symbol)item.Accept(this)).ToList();
        }

        public virtual ISqlFragment Visit(Symbol fragment) {
            return fragment;
        }

        public virtual ISqlFragment Visit(TopClause fragment) {
            return fragment;
        }

        public virtual ISqlFragment Visit(SqlBuilder fragment) {
            var result = new SqlBuilder();

            foreach (object item in fragment.SqlFragments) {
                var fragmentItem = item as ISqlFragment;

                result.Append(fragmentItem != null ? fragmentItem.Accept(this) : item);
            }

            return result;
        }

        public virtual ISqlFragment Visit(BinaryFragment fragment) {
            return fragment;
        }
    }
}