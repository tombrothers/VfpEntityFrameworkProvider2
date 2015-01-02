using SampleQueries.Harness;

namespace SampleQueries.Runner {
    public abstract class SampleBase {
        private readonly string _title;
        private readonly string _description;
        private readonly SampleSuite _sampleSuite;

        protected SampleBase(SampleSuite sampleSuite, string title, string description) {
            _title = title;
            _description = description;
            _sampleSuite = sampleSuite;
        }

        public SampleSuite Suite {
            get { return _sampleSuite; }
        }

        public string Title {
            get { return _title; }
        }

        public string Description {
            get { return _description; }
        }
    }
}