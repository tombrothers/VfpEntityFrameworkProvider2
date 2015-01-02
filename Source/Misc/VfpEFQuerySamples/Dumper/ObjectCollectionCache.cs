using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace SampleQueries.Dumper {
    public class ObjectCollectionCache : IEnumerable {
        public readonly Type OriginalType;
        public readonly IList Values;
        private string _traceString = "";

        public ObjectCollectionCache(object o) {
            if (o != null) {
                // cache ToTraceString() result for objects that support it

                MethodInfo mi = o.GetType().GetMethod("ToTraceString");
                if (mi != null)
                    _traceString = (string)mi.Invoke(o, null);
            }

            OriginalType = o.GetType();
            try {
                Values = new List<object>();
                foreach (object o2 in (IEnumerable)o) {
                    Values.Add(o2);
                }
            }
            catch (Exception ex) {
                Values = new List<object>();
                OriginalType = ex.GetType();
                Values.Clear();
                Values.Add(ex);
            }
        }

        public IEnumerator GetEnumerator() {
            return Values.GetEnumerator();
        }

        public string ToTraceString() {
            return _traceString;
        }
    }
}