using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VfpClient;
using VfpEntityFrameworkProvider.Tests.Dal.CodeFirst;
using VfpEntityFrameworkProvider.Tests.Dal.Northwind;
using VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models;

namespace VfpEntityFrameworkProvider.Tests {
    [TestClass]
    public abstract class TestBase {
        public TestContext TestContext { get; set; }

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context) {
            VfpProviderFactory.Register();
            File.WriteAllBytes("NorthwindVfp.zip", Properties.Resources.NorthwindVfp);
            File.WriteAllBytes("DecimalTable.zip", Properties.Resources.DecimalTable);
            File.WriteAllBytes("AutoGenId.zip", Properties.Resources.AutoGenId);
            File.WriteAllBytes("AllTypes.zip", Properties.Resources.AllTypes);


            var zip = new FastZip();
            zip.ExtractZip("NorthwindVfp.zip", context.TestDeploymentDir, String.Empty);
            zip.ExtractZip("DecimalTable.zip", Path.Combine(context.TestDeploymentDir, "Decimal"), String.Empty);
            zip.ExtractZip("AutoGenId.zip", Path.Combine(context.TestDeploymentDir, @"AutoGenId\Data"), String.Empty);
            Directory.CreateDirectory(Path.Combine(context.TestDeploymentDir, "AllTypes"));
            zip.ExtractZip("AllTypes.zip", Path.Combine(context.TestDeploymentDir, @"AllTypes"), String.Empty);

            VfpClientTracing.Tracer = new TraceSource("VfpClient", SourceLevels.Information);
            VfpClientTracing.Tracer.Listeners.Add(new TestContextTraceListener(context));
        }

        protected CodeFirstContext GetCodeFirstContext() {
            var connectionString = Path.Combine(TestContext.TestDeploymentDir, @"CodeFirstData\CodeFirst.dbc");
            var connection = new VfpConnection(connectionString);

            EnableTracing(connection);

            return new CodeFirstContext(connection);
        }

        protected IQueryable<Order> GetOrderQuery() {
            return this.GetContext().Orders
                                    .OrderBy(x => x.OrderID)
                                    .Where(x => x.OrderID == 10248);
        }

        protected NorthwindContext GetContext() {
            return new NorthwindContext(GetConnection());
        }

        protected virtual VfpConnection GetConnection() {
            var connectionString = Path.Combine(TestContext.TestDeploymentDir, "northwind.dbc");
            var connection = new VfpConnection(connectionString);

            EnableTracing(connection);

            return connection;
        }

        protected void EnableTracing(VfpConnection connection) {
            if (Debugger.IsAttached) {
                return;
            }

            connection.CommandExecuting = details => TestContext.WriteLine(GetExecutionDetails(details));
            connection.CommandFailed = details => TestContext.WriteLine(GetExecutionDetails(details));
            connection.CommandFinished = details => TestContext.WriteLine(GetExecutionDetails(details));
        }

        private static string GetExecutionDetails(VfpCommandExecutionDetails details) {
            return Environment.NewLine + details.ToTraceString() + Environment.NewLine;
        }

        protected void AssertException<T>(Action action) where T : Exception {
            T exception = null;

            try {
                action();
            }
            catch (Exception ex) {
                exception = GetException<T>(ex);
            }

            if (exception == null) {
                throw new Exception(typeof(T).Name + " was not thrown");
            }
        }

        private static T GetException<T>(Exception exception) where T : Exception {
            while (exception != null) {
                var specificException = exception as T;

                if (specificException != null) {
                    return specificException;
                }

                exception = exception.InnerException;
            }

            return null;
        }

        private class TestContextTraceListener : TraceListener {
            private readonly TestContext _context;

            public TestContextTraceListener(TestContext context) {
                _context = context;
            }

            public override void Write(string message) {
                _context.WriteLine(message);
            }

            public override void WriteLine(string message) {
                _context.WriteLine(message.Replace("{", "{{").Replace("}", "}}"));
            }
        }
    }
}