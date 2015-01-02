using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SampleQueries.Dumper {
    public class TreeGenerator {
        private class Member {
            public string Name;
            public object Value;
        }

        private static object _omittedValue = new object();

        private int _expandDepth;
        private int _maxLoadDepth;
        private int _level = 0;
        private Stack _ancestors = new Stack();

        public TreeGenerator(int expandDepth, int maxLoadDepth) {
            _expandDepth = expandDepth;
            _maxLoadDepth = maxLoadDepth;
        }

        private string GetNiceTypeName(Type t) {
            if (t.Name.Contains("AnonymousType"))
                return "(AnonymousType)";

            if (t.Name.Contains("SelectIterator"))
                return "(SelectIterator)";

            if (t.IsGenericType) {
                string name = t.Name;
                int backquote = name.IndexOf('`');
                if (backquote >= 0)
                    name = name.Substring(0, backquote);

                StringBuilder sb = new StringBuilder();
                sb.Append(name);
                sb.Append("<");
                Type[] args = t.GetGenericArguments();
                for (int i = 0; i < args.Length; ++i) {
                    if (i > 0)
                        sb.Append(", ");
                    sb.Append(GetNiceTypeName(args[i]));
                }
                sb.Append(">");
                return sb.ToString();
            }

            return t.Name;
        }
        public TreeNode GetTreeNode(object o) {
            if (o == null)
                return GetNullNode();

            if (o == _omittedValue)
                return new TreeNode("{ ... }");

            if (o is Exception)
                return GetExceptionNode((Exception)o);

            if (o is DateTime)
                return GetDateTimeNode((DateTime)o);

            if (o is String)
                return GetStringNode((string)o);

            if (o is Type)
                return GetTypeNode((Type)o);

            if (o is byte[]) {
                return GetByteArrayNode((byte[])o);
            }

            if (o is ValueType)
                return GetValueTypeNode(o);

            if (_ancestors.Contains(o) || _level >= _maxLoadDepth) {
                return new TreeNode("{ ... }");
            }

            TreeNode parentNode = new TreeNode(GetNiceTypeName(o.GetType()));
            if (o is ObjectCollectionCache) {
                parentNode.Text = GetNiceTypeName(((ObjectCollectionCache)o).OriginalType);
            }

            _ancestors.Push(o);
            int oldLevel = _level;
            _level++;

            if (o is IEnumerable) {
                parentNode.SelectedImageIndex = parentNode.ImageIndex = 4;
                var members = (from object element in (o as IEnumerable)
                               select new Member { Name = null, Value = element }).ToList();

                AttachChildren(parentNode, members); ;
                parentNode.Text += " (" + members.Count + " item" + (members.Count != 1 ? "s" : "") + ")";
            }
            else if (o is DbDataRecord) {
                parentNode.SelectedImageIndex = parentNode.ImageIndex = 2;
                DbDataRecord rec = o as DbDataRecord;

                var members = from element in Enumerable.Range(0, rec.FieldCount)
                              select new Member { Name = rec.GetName(element), Value = rec.IsDBNull(element) ? null : rec.GetValue(element) };

                AttachChildren(parentNode, members); ;
            }
            else {
                parentNode.SelectedImageIndex = parentNode.ImageIndex = 1;

                var members = from element in o.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance)
                              let p = element as PropertyInfo
                              let f = element as FieldInfo
                              where p != null || f != null
                              select new Member { Name = element.Name, Value = p != null ? p.GetValue(o, null) : f.GetValue(o) };

                // replace members who would lead to tree explosion with _omittedValue
                // which emits { ... }

                if (o is EntityReference)
                    members = members.Select(c => new Member { Name = c.Name, Value = (c.Name == "RelationshipSet" ? _omittedValue : c.Value) });

                AttachChildren(parentNode, members); ;

                IEntityWithKey ewk = o as IEntityWithKey;
                if (ewk != null) {
                    parentNode.Text += " { " + GetEntityKeyText(ewk.EntityKey) + " }";
                }
            }
            if (_level <= _expandDepth)
                parentNode.Expand();
            _ancestors.Pop();
            _level = oldLevel;

            return parentNode;
        }

        private string GetEntityKeyText(EntityKey key) {
            StringBuilder sb = new StringBuilder();

            if (key != null && key.EntitySetName != null) {
                sb.Append("EntitySet=");
                sb.Append(key.EntityContainerName);
                sb.Append(".");
                sb.Append(key.EntitySetName);
                sb.Append("");
            }
            if (sb.Length > 0)
                sb.Append(", ");
            sb.Append("KeyValues={ ");
            if (key != null && key.EntityKeyValues != null) {
                for (int i = 0; i < key.EntityKeyValues.Length; ++i) {
                    if (i > 0)
                        sb.Append(", ");
                    sb.Append(key.EntityKeyValues[i].Key);
                    sb.Append("=");
                    sb.Append(key.EntityKeyValues[i].Value);
                }
            }
            sb.Append(" }");
            return sb.ToString();
        }

        private void AttachChildren(TreeNode parentNode, IEnumerable<Member> members) {
            int counter = 0;

            foreach (Member m in members) {
                TreeNode valueNode = GetTreeNode(m.Value);
                if (m.Name != null)
                    valueNode.Text = m.Name + " = " + valueNode.Text;
                else
                    valueNode.Text = "#" + counter++ + ": " + valueNode.Text;
                parentNode.Nodes.Add(valueNode);
            }
        }

        private TreeNode GetDateTimeNode(DateTime dateTime) {
            return new TreeNode(dateTime.ToString());
        }

        private TreeNode GetNullNode() {
            return new TreeNode("null");
        }

        private TreeNode GetStringNode(string s) {
            return new TreeNode("\"" + s + "\"");
        }

        private TreeNode GetValueTypeNode(object o) {
            return new TreeNode(Convert.ToString(o));
        }

        private TreeNode GetExceptionNode(Exception ex) {
            if (ex is TargetInvocationException)
                ex = ex.InnerException;

            TreeNode node = new TreeNode(ex.GetType().Name + ": " + ex.Message);
            node.Nodes.Add(new TreeNode("Source: " + ex.Source));
            if (ex.InnerException != null) {
                TreeNode innerException = GetExceptionNode(ex.InnerException);
                innerException.Text = "InnerException: " + innerException.Text;
                node.Nodes.Add(innerException);
            }
            node.ExpandAll();
            return node;
        }

        private TreeNode GetTypeNode(Type t) {
            TreeNode node = new TreeNode(GetNiceTypeName(t));
            return node;
        }

        private TreeNode GetByteArrayNode(byte[] arr) {
            int length = Math.Min(arr.Length, 32);
            string t = "Byte[" + arr.Length + "] = " + BitConverter.ToString(arr, 0, length) + ((length != arr.Length) ? "..." : "");
            TreeNode node = new TreeNode(t);
            return node;
        }


    }
}
