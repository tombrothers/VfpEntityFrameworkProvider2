using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Objects.DataClasses;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SampleQueries.Dumper {
    public class PrettyPrinter {
        private TextWriter _writer;
        private int _maximumDepth;
        private Stack<object> _ancestors = new Stack<object>();
        private static object _omittedValue = new object();

        private int Level {
            get { return _ancestors.Count; }
        }

        public PrettyPrinter(TextWriter output, int maximumDepth) {
            this._writer = output;
            this._maximumDepth = maximumDepth;
        }

        public void Write(string s) {
            _writer.Write(s);
        }

        private void WriteIndent() {
            for (int i = 0; i < Level; i++)
                _writer.Write("    ");
        }

        public void WriteLine() {
            _writer.WriteLine();
        }

        private void WriteCommaIfNotEmpty(ref bool empty) {
            if (empty) {
                empty = false;
            }
            else {
                Write(", ");
            }
        }

        private void Write(Type t) {
            string name = t.Name;
            if (t.IsGenericType) {
                if (name.Contains("AnonymousType")) {
                    Write("(AnonymousType)");
                }
                else if (name.Contains("SelectIterator")) {
                    Write("(SelectIterator)");
                }
                else {
                    Write(name.Remove(name.LastIndexOf("`")));
                }
                Write("<");
                bool empty = true;
                foreach (Type ga in t.GetGenericArguments()) {
                    WriteCommaIfNotEmpty(ref empty);
                    Write(ga);
                }
                Write(">");
            }
            else {
                Write(t.Name);
            }
        }

        private class Member {
            public string Name;
            public object Value;
        }

        private void Write(Member m) {
            if (m.Name != null) {
                Write(m.Name);
                Write(" = ");
            }
            Write(m.Value);
        }

        private void Write(IEnumerable<Member> members) {
            Write("{");
            bool empty = true;
            foreach (var m in members) {
                WriteCommaIfNotEmpty(ref empty);
                WriteLine();
                WriteIndent();
                Write(m);
            }

            _ancestors.Pop();

            if (!empty) {
                WriteLine();
                WriteIndent();
            }
            Write("}");
        }

        public void Write(object o) {
            if (o == null) {
                Write("null");
            }
            else if (o == _omittedValue) {
                Write("{...}");
            }
            else if (o is DateTime) {
                Write("{");
                Write(((DateTime)o).ToString());
                Write("}");
            }
            else if (o is ValueType) {
                Write(o.ToString());
            }
            else if (o is Type) {
                Write(((Type)o).Name);
            }
            else if (o is Exception) {
                Write("EXCEPTION: " + o.ToString());
            }
            else if (o is byte[]) {
                byte[] arr = (byte[])o;
                int length = Math.Min(arr.Length, 32);
                string t = "Byte[" + arr.Length + "] = " + BitConverter.ToString(arr, 0, length) + ((length != arr.Length) ? "..." : "");
                Write(t);
            }
            else if (o is string) {
                Write("\"");
                Write(o as string);
                Write("\"");
            }
            else {
                if (o is ObjectCollectionCache) {
                    Write(((ObjectCollectionCache)o).OriginalType);
                }
                else {
                    Write(o.GetType());
                }
                Write(" ");

                if (_ancestors.Contains(o) || (Level > _maximumDepth + 1)) {
                    Write("{...}");
                }
                else {
                    _ancestors.Push(o);
                    if (o is IEnumerable) {
                        var members = from object element in (o as IEnumerable)
                                      select new Member { Name = null, Value = element };

                        Write(members);
                    }
                    else if (o is DbDataRecord) {
                        DbDataRecord rec = o as DbDataRecord;

                        var members = from element in Enumerable.Range(0, rec.FieldCount)
                                      select new Member { Name = rec.GetName(element), Value = rec.IsDBNull(element) ? null : rec.GetValue(element) };

                        Write(members);
                    }
                    else {
                        var members = from element in o.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance)
                                      let p = element as PropertyInfo
                                      let f = element as FieldInfo
                                      where p != null || f != null
                                      select new Member { Name = element.Name, Value = p != null ? p.GetValue(o, null) : f.GetValue(o) };

                        // remove members which cause explosion of the tree
                        if (o is EntityReference)
                            members = members.Select(c => new Member { Name = c.Name, Value = (c.Name == "RelationshipSet" ? _omittedValue : c.Value) });

                        Write(members);
                    }
                }
            }
        }
    }
}