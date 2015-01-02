using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows.Forms;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpExpressionVisualizer {
    [Serializable]
    public class VfpExpressionTreeNode : TreeNode {
        private readonly string _namespace = typeof(VfpExpression).Namespace;

        protected VfpExpressionTreeNode(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }

        public VfpExpressionTreeNode(object value, Color? color = null) {
            var dbExpression = value as VfpExpression;
            color = color ?? Color.Black;

            if (dbExpression != null) {
                switch (dbExpression.ExpressionKind) {
                    case VfpExpressionKind.XmlToCursor:
                        color = Color.Blue;
                        break;
                    case VfpExpressionKind.Constant:
                        color = Color.Crimson;
                        break;
                    case VfpExpressionKind.Property:
                        color = Color.Purple;
                        break;
                    case VfpExpressionKind.Scan:
                        color = Color.SeaGreen;
                        break;
                    case VfpExpressionKind.VariableReference:
                        color = Color.Violet;
                        break;
                }
            }

            ForeColor = color.Value;

            var type = value.GetType();
            Text = type.ObtainOriginalName();

            if (type.Namespace == _namespace) {
                foreach (var propertyInfo in GetProperties(type)) {
                    Nodes.Add(new AttributeNode(value, propertyInfo, color.Value));
                }
            }
            else {
                Text = "\"" + value + "\"";
            }
        }

        private IEnumerable<PropertyInfo> GetProperties(Type type) {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                       .Where(x => x.Name != "ExpressionKind");
        }
    }
}