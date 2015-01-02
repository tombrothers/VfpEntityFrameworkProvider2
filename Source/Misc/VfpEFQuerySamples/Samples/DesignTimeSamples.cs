using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using NorthwindEFModel;
using SampleQueries.Harness;

namespace SampleQueries.Samples {
    [Title("Design APIs")]
    [Prefix("DesignTime")]
    class DesignTimeSamples : SchemaInformationBasedSample {
        #region SchemaInformation

        [Category("SchemaInformation")]
        [Title("Query - Tables")]
        [Description("This sample lists all tables in the schema.")]
        public void DesignTime1() {
            using (EntityCommand cmd = new EntityCommand("SELECT t.Name FROM SchemaInformation.Tables AS t", (EntityConnection)context.Connection)) {
                using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess)) {
                    ObjectDumper.Write(reader);
                }
            }

            ObjectDumper.Write(context.Tables);
        }

        [Category("SchemaInformation")]
        [Title("Query - Views")]
        [Description("This sample lists all views in the schema.")]
        public void DesignTime2() {
            ObjectDumper.Write(context.Views);
        }

        [Category("SchemaInformation")]
        [Title("Query - Functions")]
        [Description("This sample lists all functions in the schema.")]
        public void DesignTime3() {
            ObjectDumper.Write(context.Functions);
        }

        [Category("SchemaInformation")]
        [Title("Query - Function Parameters")]
        [Description("This sample lists all function parameters in the schema.")]
        public void DesignTime4() {
            ObjectDumper.Write(context.Functions);
        }

        [Category("SchemaInformation")]
        [Title("Query - Procedures")]
        [Description("This sample lists all procedures in the schema.")]
        public void DesignTime5() {
            ObjectDumper.Write(context.Procedures);
        }

        [Category("SchemaInformation")]
        [Title("Query - Procedure Parameters")]
        [Description("This sample lists all procedure parameters in the schema.")]
        public void DesignTime6() {
            ObjectDumper.Write(context.ProcedureParameters);
        }

        [Category("SchemaInformation")]
        [Title("Query - Foreign Keys")]
        [Description("This sample lists all foreign keys in the schema.")]
        public void DesignTime7() {
            var tfkList = context.TableForeignKeys.ToList();
            var vfkList = context.ViewForeignKeys.ToList();
            var list = tfkList.Union(vfkList);
            ObjectDumper.Write(list);
        }

        [Category("SchemaInformation")]
        [Title("Query - Table Constraints")]
        [Description("This sample lists all table constraints.")]
        public void DesignTime8() {
            ObjectDumper.Write(context.TableConstraints);
        }

        [Category("SchemaInformation")]
        [Title("Query - View Constraints")]
        [Description("This sample lists all view constraints.")]
        public void DesignTime9() {
            ObjectDumper.Write(context.ViewConstraints);
        }
        #endregion
    }
}