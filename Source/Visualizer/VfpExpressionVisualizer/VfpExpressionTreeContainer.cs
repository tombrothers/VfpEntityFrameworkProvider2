using System;

namespace VfpExpressionVisualizer {
    [Serializable]
    public class VfpExpressionTreeContainer {
        public VfpExpressionTreeNode Tree { get; private set; }

        public VfpExpressionTreeContainer(VfpExpressionTreeNode tree) {
            Tree = tree;
        }
    }
}