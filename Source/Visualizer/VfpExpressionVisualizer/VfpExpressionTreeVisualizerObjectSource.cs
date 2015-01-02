using System.IO;
using Microsoft.VisualStudio.DebuggerVisualizers;
using VfpEntityFrameworkProvider.Expressions;

namespace VfpExpressionVisualizer {
    public class VfpExpressionTreeVisualizerObjectSource : VisualizerObjectSource {
        public override void GetData(object target, Stream outgoingData) {
            var expr = (VfpExpression)target;
            var browser = new VfpExpressionTreeNode(expr);
            var container = new VfpExpressionTreeContainer(browser);

            Serialize(outgoingData, container);
        }
    }
}