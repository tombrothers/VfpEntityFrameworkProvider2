namespace SampleQueries.Harness {
    public abstract class SampleSuite {
        protected internal IObjectDumper ObjectDumper { get; set; }

        public virtual void InitSample(string connectionString) {
        }

        public virtual void TearDownSample() {
        }
    }
}