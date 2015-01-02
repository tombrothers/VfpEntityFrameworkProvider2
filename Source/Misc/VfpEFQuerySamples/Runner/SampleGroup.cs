using System.Collections.Generic;
using SampleQueries.Harness;

namespace SampleQueries.Runner {
    public class SampleGroup : SampleBase {
        private readonly IList<SampleBase> _children = new List<SampleBase>();

        public SampleGroup(SampleSuite sampleSuite, string title, string description)
            : base(sampleSuite, title, description) {
        }

        public IList<SampleBase> Children {
            get { return _children; }
        }
    }
}
