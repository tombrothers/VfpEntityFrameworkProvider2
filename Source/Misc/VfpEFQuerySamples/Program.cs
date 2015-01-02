using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SampleQueries.Runner;
using SampleQueries.Samples;
using SampleQueries.Utils;
using VfpEntityFrameworkProvider;

namespace SampleQueries {
    static class Program {
        [STAThread]
        static void Main(string[] args) {
            VfpProviderFactory.Register();

            bool runAll = false;
            bool pause = false;
            string logFile = null;

            string connectionString = ConfigurationManager.ConnectionStrings
                .Cast<ConnectionStringSettings>()
                .Where(c => c.ProviderName == "System.Data.EntityClient")
                .First()
                .ConnectionString;

            for (int i = 0; i < args.Length; ++i) {
                switch (args[i]) {
                    case "/runall":
                        runAll = true;
                        break;

                    case "/pause":
                        pause = true;
                        break;


                    case "/log":
                        logFile = args[++i];
                        break;

                    case "/connectionString":
                        connectionString = ConfigurationManager.ConnectionStrings[args[++i]].ConnectionString;
                        break;
                }
            }


            // set up a loader and source code search paths
            SampleLoader loader = new SampleLoader();
            loader.AddSourceDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "."));
            loader.AddSourceDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Samples"));
            loader.AddSourceDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Samples"));

            // load samples
            SampleGroup allSamples = new SampleGroup(null, "Entity Framework Query Samples", "");
            allSamples.Children.Add(loader.Load(new EntitySQLSamples()));
            allSamples.Children.Add(loader.Load(new LinqToEntitiesSamples()));
            allSamples.Children.Add(loader.Load(new BuilderMethodSamples()));
            //allSamples.Children.Add(loader.Load(new DesignTimeSamples()));
            allSamples.Children.Add(loader.Load(new ObjectServicesSamples()));

            if (runAll) {
                NativeMethods.AllocConsole();
                SampleRunner runner;

                if (logFile != null) {
                    StreamWriter sw = File.CreateText(logFile);
                    sw.AutoFlush = true;
                    runner = new TextWriterSampleRunner(sw);
                }
                else
                    runner = new ConsoleSampleRunner();
                runner.ConnectionString = connectionString;
                runner.Run(allSamples);
                if (pause)
                    Console.ReadKey();
            }
            else {
                // launch WinForms UI
                Application.EnableVisualStyles();
                Application.Run(new FormSampleRunner(allSamples));
            }
        }
    }
}