using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Migrations.Sql;
using System.Data.OleDb;
using System.IO;
using VfpEntityFrameworkProvider.Properties;
using VfpEntityFrameworkProvider.SqlGeneration;
using VfpEntityFrameworkProvider.VfpOleDb;

namespace VfpEntityFrameworkProvider {
    public class VfpProviderServices : DbProviderServices {
        public static readonly VfpProviderServices Instance = new VfpProviderServices();
        public static readonly string ProviderInvariantName = Resources.Provider_Invariant;

        private VfpProviderServices() {
            AddDependencyResolver(new SingletonDependencyResolver<IDbConnectionFactory>(new VfpConnectionFactory()));
            AddDependencyResolver(new SingletonDependencyResolver<Func<MigrationSqlGenerator>>(() => new VfpMigrationSqlGenerator(), Resources.Provider_Invariant));
        }

        protected override DbCommandDefinition CreateDbCommandDefinition(DbProviderManifest providerManifest, DbCommandTree commandTree) {
            var command = CreateCommand(providerManifest, commandTree);

            return CreateCommandDefinition(command);
        }

        internal static DbCommand CreateCommand(DbProviderManifest manifest, DbCommandTree commandTree) {
            ArgumentUtility.CheckNotNull("manifest", manifest);
            ArgumentUtility.CheckNotNull("commandTree", commandTree);

            var vfpManifest = manifest as VfpProviderManifest;

            if (vfpManifest == null) {
                throw new ArgumentException("The provider manifest given is not of type 'VfpProviderManifest'.");
            }

            var command = new VfpCommand();

            List<DbParameter> parameters;
            CommandType commandType;

            command.CommandText = SqlGenerator.GenerateSql(vfpManifest, commandTree, out parameters, out commandType);
            command.CommandType = commandType;

            //if (command.CommandType == CommandType.Text) {
            //    command.CommandText += Environment.NewLine + Environment.NewLine;
            //}

            // Get the function (if any) implemented by the command tree since this influences our interpretation of parameters
            EdmFunction function = null;
            if (commandTree is DbFunctionCommandTree) {
                function = ((DbFunctionCommandTree)commandTree).EdmFunction;
            }

            var parameterHelper = new VfpParameterHelper();

            // Now make sure we populate the command's parameters from the CQT's parameters:
            foreach (var queryParameter in commandTree.Parameters) {
                VfpClient.VfpParameter parameter;

                // Use the corresponding function parameter TypeUsage where available (currently, the SSDL facets and 
                // type trump user-defined facets and type in the EntityCommand).
                FunctionParameter functionParameter;
                if (null != function && function.Parameters.TryGetValue(queryParameter.Key, false, out functionParameter)) {
                    parameter = parameterHelper.CreateVfpParameter(functionParameter.Name, functionParameter.TypeUsage, functionParameter.Mode, DBNull.Value);
                }
                else {
                    parameter = parameterHelper.CreateVfpParameter(queryParameter.Key, queryParameter.Value, ParameterMode.In, DBNull.Value);
                }

                command.Parameters.Add(parameter);
            }

            foreach (var parameter in parameters) {
                command.Parameters.Add(parameter);
            }

            return command;
        }

        protected override DbProviderManifest GetDbProviderManifest(string manifestToken) {
            return new VfpProviderManifest();
        }

        protected override string GetDbProviderManifestToken(DbConnection connection) {
            return "VFP9";
        }

        protected override void DbCreateDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection) {
            ArgumentUtility.CheckNotNull("storeItemCollection", storeItemCollection);

            var vfpConnection = ConvertToVfpConnection(connection);
            var script = DdlBuilder.CreateObjectsScript(storeItemCollection, true);

            script = script.Replace(";" + Environment.NewLine, " ");

            CreateDbc(vfpConnection);

            using (var command = vfpConnection.CreateCommand()) {
                vfpConnection.DoConnected(() => {
                    var commands = script.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                    if (commands.Length <= 0) {
                        return;
                    }

                    for (int index = 0, total = commands.Length; index < total; index++) {
                        command.CommandText = commands[index];
                        //try
                        //{
                        command.ExecuteNonQuery();
                        //}
                        //catch (OleDbException ex)
                        //{
                        //    Trace.WriteLine("VFP EF Provider - DDL Command Failed:  " + command.CommandText + Environment.NewLine + ex.ToString());
                        //}
                    }
                });
            }
        }

        private static void CreateDbc(VfpConnection vfpConnection) {
            var connectionStringBuilder = new OleDbConnectionStringBuilder(vfpConnection.ConnectionString);
            var databaseContainerFullPath = Path.GetFullPath(connectionStringBuilder.DataSource);
            
            CreateDataPath(Path.GetDirectoryName(databaseContainerFullPath));

            File.WriteAllBytes(databaseContainerFullPath, Resources.BlankDbc);
            File.WriteAllBytes(Path.ChangeExtension(databaseContainerFullPath, "dct"), Resources.BlankDct);
            File.WriteAllBytes(Path.ChangeExtension(databaseContainerFullPath, "dcx"), Resources.BlankDcx);
        }

        private static void CreateDataPath(string dataPath) {
            if (!Directory.Exists(dataPath)) {
                Directory.CreateDirectory(dataPath);
            }
        }

        protected override string DbCreateDatabaseScript(string providerManifestToken, StoreItemCollection storeItemCollection) {
            ArgumentUtility.CheckNotNull("storeItemCollection", storeItemCollection);

            return DdlBuilder.CreateObjectsScript(storeItemCollection);
        }

        protected override void DbDeleteDatabase(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection) {
            ArgumentUtility.CheckNotNull("storeItemCollection", storeItemCollection);

            var vfpConnection = ConvertToVfpConnection(connection);
            var connectionStringBuilder = new OleDbConnectionStringBuilder(vfpConnection.ConnectionString);
            var databaseContainerFullPath = Path.GetFullPath(connectionStringBuilder.DataSource);
            
            DeleteDataPath(Path.GetDirectoryName(databaseContainerFullPath));
        }

        private static void DeleteDataPath(string dataPath) {
            if (Directory.Exists(dataPath)) {
                Directory.Delete(dataPath, true);
            }
        }

        protected override bool DbDatabaseExists(DbConnection connection, int? commandTimeout, StoreItemCollection storeItemCollection) {
            try {
                var vfpConnection = ConvertToVfpConnection(connection);

                vfpConnection.Open();
                vfpConnection.Close();

                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        private static VfpConnection ConvertToVfpConnection(DbConnection connection) {
            var vfpConnection = connection as VfpConnection;

            if (vfpConnection == null) {
                throw new ApplicationException("Invalid Connection.");
            }

            if (!vfpConnection.IsDbc) {
                throw new ApplicationException("Database Container is required.");
            }

            return vfpConnection;
        }
    }
}