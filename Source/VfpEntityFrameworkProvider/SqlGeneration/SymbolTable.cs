using System;
using System.Collections.Generic;

namespace VfpEntityFrameworkProvider.SqlGeneration {
    internal sealed class SymbolTable {
        private readonly List<Dictionary<string, Symbol>> _symbols = new List<Dictionary<string, Symbol>>();

        internal void EnterScope() {
            _symbols.Add(new Dictionary<string, Symbol>(StringComparer.OrdinalIgnoreCase));
        }

        internal void ExitScope() {
            _symbols.RemoveAt(_symbols.Count - 1);
        }

        internal void Add(string name, Symbol value) {
            _symbols[_symbols.Count - 1][name] = value;
        }

        internal Symbol Lookup(string name) {
            for (var i = _symbols.Count - 1; i >= 0; --i) {
                if (_symbols[i].ContainsKey(name)) {
                    return _symbols[i][name];
                }
            }

            return null;
        }
    }
}