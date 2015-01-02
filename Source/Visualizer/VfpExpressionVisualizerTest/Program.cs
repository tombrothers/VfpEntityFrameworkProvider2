using Microsoft.VisualStudio.DebuggerVisualizers;
using VfpEntityFrameworkProvider.Expressions;
using VfpExpressionVisualizer;

namespace VfpExpressionVisualizerTest {
    internal class Program {
        public static void Main(string[] args) {
            var left = VfpExpressionBuilder.Constant(0);
            var right = VfpExpressionBuilder.Constant(0);
            var binary = left.And(right);
            var host = new VisualizerDevelopmentHost(binary,
                                                     typeof(VfpExpressionTreeVisualizer),
                                                     typeof(VfpExpressionTreeVisualizerObjectSource));

            host.ShowVisualizer();
        }
    }
}