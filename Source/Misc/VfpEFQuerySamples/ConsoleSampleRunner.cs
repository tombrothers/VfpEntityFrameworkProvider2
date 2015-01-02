using System;
using SampleQueries.Runner;

namespace SampleQueries {
    public class ConsoleSampleRunner : TextWriterSampleRunner {
        public ConsoleSampleRunner()
            : base(Console.Out) {
        }

        public override void OnFailure(Sample sample, Exception ex) {
            ConsoleColor oldcolor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            base.OnFailure(sample, ex);
            Console.ForegroundColor = oldcolor;
        }

        public override void OnSuccess(Sample sample) {
            ConsoleColor oldcolor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            base.OnSuccess(sample);
            Console.ForegroundColor = oldcolor;
        }

        public override void OnStartingGroup(SampleGroup group) {
            ConsoleColor oldcolor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            base.OnStartingGroup(group);
            Console.ForegroundColor = oldcolor;
        }

        public override void OnFinishedGroup(SampleGroup group) {
            ConsoleColor oldcolor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            base.OnFinishedGroup(group);
            Console.ForegroundColor = oldcolor;
        }
    }
}