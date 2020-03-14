using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VfpClient;
using VfpEntityFrameworkProvider.Tests.Dal.CodeFirst;
using VfpEntityFrameworkProvider.Tests.Dal.Northwind;
using VfpEntityFrameworkProvider.Tests.Dal.Northwind.Models;

namespace VfpEntityFrameworkProvider.Tests {
    [TestClass]
    public abstract class TestBase {
        public TestContext TestContext { get; set; }
        private static Guid testRun;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context) {
            testRun = Guid.NewGuid();
            VfpProviderFactory.Register();
            CreateZip("NorthwindVfp.zip", Properties.Resources.NorthwindVfp);
            CreateZip("DecimalTable.zip", Properties.Resources.DecimalTable);
            CreateZip("AutoGenId.zip", Properties.Resources.AutoGenId);
            CreateZip("AllTypes.zip", Properties.Resources.AllTypes);

            UnZip("NorthwindVfp.zip", GetTestDeploymentDir(context));
            UnZip("DecimalTable.zip", Path.Combine(GetTestDeploymentDir(context), "Decimal"));
            UnZip("AutoGenId.zip", Path.Combine(GetTestDeploymentDir(context), @"AutoGenId\Data"));
            UnZip("AllTypes.zip", Path.Combine(GetTestDeploymentDir(context), @"AllTypes"));

            VfpClientTracing.Tracer = new TraceSource("VfpClient", SourceLevels.Information);
            VfpClientTracing.Tracer.Listeners.Add(new TestContextTraceListener(context));
        }

        private static void UnZip(string zipFile, string directory) {
            if(Directory.Exists(directory)) {
                Directory.Delete(directory);
            }

            Directory.CreateDirectory(directory);

            ZipFile.ExtractToDirectory(zipFile, directory);
        }

        private static void CreateZip(string file, byte[] content) {
            if(File.Exists(file)) {
                File.Delete(file);
            }

            File.WriteAllBytes(file, content);
        }

        protected CodeFirstContext GetCodeFirstContext() {
            var connectionString = Path.Combine(GetTestDeploymentDir(TestContext), @"CodeFirstData\CodeFirst.dbc");
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
            var connectionString = Path.Combine(GetTestDeploymentDir(TestContext), "northwind.dbc");
            var connection = new VfpConnection(connectionString);

            EnableTracing(connection);

            return connection;
        }

        protected static string GetTestDeploymentDir(TestContext context) =>
            Path.Combine(context.TestDeploymentDir, testRun.ToString());

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