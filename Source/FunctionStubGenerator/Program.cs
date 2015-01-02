using VfpEntityFrameworkProvider;

namespace FunctionStubGenerator {
    public class Program {
        public static void Main(string[] args) {
            VfpProviderFactory.Register();
            LinqFunctionStubsCodeGen.Generate("..\\..\\..\\VfpEntityFrameworkProvider\\VfpFunctions.cs");
        }
    }
}