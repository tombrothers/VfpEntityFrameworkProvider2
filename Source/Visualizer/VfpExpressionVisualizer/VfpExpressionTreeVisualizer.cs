using System;
using System.Diagnostics;
using Microsoft.VisualStudio.DebuggerVisualizers;
using VfpEntityFrameworkProvider.Expressions;
using VfpExpressionVisualizer;

[assembly: DebuggerVisualizer(typeof(VfpExpressionTreeVisualizer), 
                              typeof(VfpExpressionTreeVisualizerObjectSource), 
                              Target = typeof(VfpExpression), 
                              Description = "DbExpression Tree Visualizer")]

namespace VfpExpressionVisualizer {
    public class VfpExpressionTreeVisualizer : DialogDebuggerVisualizer {
        private IDialogVisualizerService modalService;

        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider) {
            modalService = windowService;

            if (modalService == null) {
                throw new NotSupportedException("This debugger does not support modal visualizers");
            }

            var container = (VfpExpressionTreeContainer)objectProvider.GetObject();
            var treeForm = new TreeWindow(container.Tree);

            modalService.ShowDialog(treeForm);
        }
    }
}