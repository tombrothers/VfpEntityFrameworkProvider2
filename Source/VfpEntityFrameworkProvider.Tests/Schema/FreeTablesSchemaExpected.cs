using System;
using System.Data;

namespace VfpEntityFrameworkProvider.Tests.Schema {
    class FreeTablesSchemaExpected {
        public static DataTable GetViewForeignKeys() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Tocolumnid", typeof(System.String));
            dataTable.Columns.Add("Fromcolumnid", typeof(System.String));
            dataTable.Columns.Add("Constraintid", typeof(System.String));
            dataTable.Columns.Add("Ordinal", typeof(System.Int32));

            #endregion columns

            return dataTable;
        }

        public static DataTable GetTableForeignKeys() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Tocolumnid", typeof(System.String));
            dataTable.Columns.Add("Fromcolumnid", typeof(System.String));
            dataTable.Columns.Add("Constraintid", typeof(System.String));
            dataTable.Columns.Add("Ordinal", typeof(System.Int32));

            #endregion columns

            return dataTable;
        }

        public static DataTable GetTableForeignKeyConstraints() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Updaterule", typeof(System.String));
            dataTable.Columns.Add("Deleterule", typeof(System.String));

            #endregion columns

            return dataTable;
        }

        public static DataTable GetViewConstraintColumns() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Constraintid", typeof(System.String));
            dataTable.Columns.Add("Columnid", typeof(System.String));

            #endregion columns

            return dataTable;
        }

        public static DataTable GetTableConstraintColumns() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Constraintid", typeof(System.String));
            dataTable.Columns.Add("Columnid", typeof(System.String));

            #endregion columns

            #region rows

            DataRow row;

            #region row 0

            row = dataTable.NewRow();
            row["Constraintid"] = "PK_Sample_C1";
            row["Columnid"] = "Sample_C1.Flagfield";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 1

            row = dataTable.NewRow();
            row["Constraintid"] = "PK_Alltypes";
            row["Columnid"] = "Alltypes.Ifield";
            dataTable.Rows.Add(row);

            #endregion row

            #endregion rows

            return dataTable;
        }

        public static DataTable GetViewConstraints() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Parentid", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Constrainttype", typeof(System.String));
            dataTable.Columns.Add("Isdeferrable", typeof(System.Boolean));
            dataTable.Columns.Add("Isinitiallydeferred", typeof(System.Boolean));
            dataTable.Columns.Add("Expression", typeof(System.String));
            dataTable.Columns.Add("Updaterule", typeof(System.String));
            dataTable.Columns.Add("Deleterule", typeof(System.String));

            #endregion columns

            return dataTable;
        }

        public static DataTable GetTableConstraints() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Parentid", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Constrainttype", typeof(System.String));
            dataTable.Columns.Add("Isdeferrable", typeof(System.Boolean));
            dataTable.Columns.Add("Isinitiallydeferred", typeof(System.Boolean));

            #endregion columns

            #region rows

            DataRow row;

            #region row 0

            row = dataTable.NewRow();
            row["Id"] = "PK_Sample_C1";
            row["Parentid"] = "Sample_C1";
            row["Name"] = "PK_Sample_C1";
            row["Constrainttype"] = "PRIMARY KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #region row 1

            row = dataTable.NewRow();
            row["Id"] = "PK_Alltypes";
            row["Parentid"] = "Alltypes";
            row["Name"] = "PK_Alltypes";
            row["Constrainttype"] = "PRIMARY KEY";
            row["Isdeferrable"] = false;
            row["Isinitiallydeferred"] = false;
            dataTable.Rows.Add(row);

            #endregion row

            #endregion rows

            return dataTable;
        }

        public static DataTable GetProcedureParameters() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Parentid", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Ordinal", typeof(System.Int32));
            dataTable.Columns.Add("Typename", typeof(System.String));
            dataTable.Columns.Add("Maxlength", typeof(System.String));
            dataTable.Columns.Add("Precision", typeof(System.Int32));
            dataTable.Columns.Add("Datetimeprecision", typeof(System.Int32));
            dataTable.Columns.Add("Scale", typeof(System.Int32));
            dataTable.Columns.Add("Collationcatalog", typeof(System.String));
            dataTable.Columns.Add("Collationschema", typeof(System.String));
            dataTable.Columns.Add("Collationname", typeof(System.String));
            dataTable.Columns.Add("Charactersetcatalog", typeof(System.String));
            dataTable.Columns.Add("Charactersetschema", typeof(System.String));
            dataTable.Columns.Add("Charactersetname", typeof(System.String));
            dataTable.Columns.Add("Ismultiset", typeof(System.Boolean));
            dataTable.Columns.Add("Mode", typeof(System.String));
            dataTable.Columns.Add("Default", typeof(System.String));

            #endregion columns

            return dataTable;
        }

        public static DataTable GetProcedures() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Catalogname", typeof(System.String));
            dataTable.Columns.Add("Schemaname", typeof(System.String));

            #endregion columns

            return dataTable;
        }

        public static DataTable GetFunctionParameters() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Parentid", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Ordinal", typeof(System.Int32));
            dataTable.Columns.Add("Typename", typeof(System.String));
            dataTable.Columns.Add("Maxlength", typeof(System.String));
            dataTable.Columns.Add("Precision", typeof(System.Int32));
            dataTable.Columns.Add("Datetimeprecision", typeof(System.Int32));
            dataTable.Columns.Add("Scale", typeof(System.Int32));
            dataTable.Columns.Add("Collationcatalog", typeof(System.String));
            dataTable.Columns.Add("Collationschema", typeof(System.String));
            dataTable.Columns.Add("Collationname", typeof(System.String));
            dataTable.Columns.Add("Charactersetcatalog", typeof(System.String));
            dataTable.Columns.Add("Charactersetschema", typeof(System.String));
            dataTable.Columns.Add("Charactersetname", typeof(System.String));
            dataTable.Columns.Add("Ismultiset", typeof(System.Boolean));
            dataTable.Columns.Add("Mode", typeof(System.String));
            dataTable.Columns.Add("Default", typeof(System.String));

            #endregion columns

            return dataTable;
        }

        public static DataTable GetFunctions() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Catalogname", typeof(System.String));
            dataTable.Columns.Add("Schemaname", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Returntypename", typeof(System.String));
            dataTable.Columns.Add("Returnmaxlength", typeof(System.String));
            dataTable.Columns.Add("Returnprecision", typeof(System.Int32));
            dataTable.Columns.Add("Returndatetimeprecision", typeof(System.Int32));
            dataTable.Columns.Add("Returnscale", typeof(System.Int32));
            dataTable.Columns.Add("Returncollationcatalog", typeof(System.String));
            dataTable.Columns.Add("Returncollationschema", typeof(System.String));
            dataTable.Columns.Add("Returncollationname", typeof(System.String));
            dataTable.Columns.Add("Returncharactersetcatalog", typeof(System.String));
            dataTable.Columns.Add("Returncharactersetschema", typeof(System.String));
            dataTable.Columns.Add("Returncharactersetname", typeof(System.String));
            dataTable.Columns.Add("Returnismultiset", typeof(System.Boolean));
            dataTable.Columns.Add("Isaggregate", typeof(System.Boolean));
            dataTable.Columns.Add("Isbuiltin", typeof(System.Boolean));
            dataTable.Columns.Add("Isniladic", typeof(System.Boolean));
            dataTable.Columns.Add("Istvf", typeof(System.Boolean));

            #endregion columns

            return dataTable;
        }

        public static DataTable GetViewColumns() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Parentid", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Ordinal", typeof(System.Int32));
            dataTable.Columns.Add("Isnullable", typeof(System.Boolean));
            dataTable.Columns.Add("Maxlength", typeof(System.Int32));
            dataTable.Columns.Add("Precision", typeof(System.Int32));
            dataTable.Columns.Add("Scale", typeof(System.Int32));
            dataTable.Columns.Add("Datetimeprecision", typeof(System.String));
            dataTable.Columns.Add("Charactersetcatalog", typeof(System.String));
            dataTable.Columns.Add("Charactersetschema", typeof(System.String));
            dataTable.Columns.Add("Charactersetname", typeof(System.String));
            dataTable.Columns.Add("Collationcatalog", typeof(System.String));
            dataTable.Columns.Add("Collationschema", typeof(System.String));
            dataTable.Columns.Add("Collationname", typeof(System.String));
            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Ismultiset", typeof(System.Boolean));
            dataTable.Columns.Add("Isstoregenerated", typeof(System.Boolean));
            dataTable.Columns.Add("Isidentity", typeof(System.Boolean));
            dataTable.Columns.Add("Default", typeof(System.String));
            dataTable.Columns.Add("Typename", typeof(System.String));

            #endregion columns

            return dataTable;
        }

        public static DataTable GetViews() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Catalogname", typeof(System.String));
            dataTable.Columns.Add("Schemaname", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Isupdatable", typeof(System.Boolean));
            dataTable.Columns.Add("Viewdefinition", typeof(System.String));

            #endregion columns

            return dataTable;
        }

        public static DataTable GetTableColumns() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Parentid", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Ordinal", typeof(System.Int32));
            dataTable.Columns.Add("Isnullable", typeof(System.Boolean));
            dataTable.Columns.Add("Maxlength", typeof(System.Int32));
            dataTable.Columns.Add("Precision", typeof(System.Int32));
            dataTable.Columns.Add("Scale", typeof(System.Int32));
            dataTable.Columns.Add("Datetimeprecision", typeof(System.String));
            dataTable.Columns.Add("Charactersetcatalog", typeof(System.String));
            dataTable.Columns.Add("Charactersetschema", typeof(System.String));
            dataTable.Columns.Add("Charactersetname", typeof(System.String));
            dataTable.Columns.Add("Collationcatalog", typeof(System.String));
            dataTable.Columns.Add("Collationschema", typeof(System.String));
            dataTable.Columns.Add("Collationname", typeof(System.String));
            dataTable.Columns.Add("Id", typeof(System.String));
            dataTable.Columns.Add("Ismultiset", typeof(System.Boolean));
            dataTable.Columns.Add("Isstoregenerated", typeof(System.Boolean));
            dataTable.Columns.Add("Isidentity", typeof(System.Boolean));
            dataTable.Columns.Add("Default", typeof(System.String));
            dataTable.Columns.Add("Typename", typeof(System.String));

            #endregion columns

            #region rows

            DataRow row;

            #region row 0

            row = dataTable.NewRow();
            row["Parentid"] = "Alltypes";
            row["Name"] = "Wfield";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = 2147483647;
            row["Precision"] = 2147483647;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Alltypes.Wfield";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "general";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 1

            row = dataTable.NewRow();
            row["Parentid"] = "Alltypes";
            row["Name"] = "Cfield";
            row["Ordinal"] = 2;
            row["Isnullable"] = false;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Alltypes.Cfield";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 2

            row = dataTable.NewRow();
            row["Parentid"] = "Alltypes";
            row["Name"] = "Cfield2";
            row["Ordinal"] = 3;
            row["Isnullable"] = false;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Alltypes.Cfield2";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 3

            row = dataTable.NewRow();
            row["Parentid"] = "Alltypes";
            row["Name"] = "Yfield";
            row["Ordinal"] = 4;
            row["Isnullable"] = false;
            row["Maxlength"] = 8;
            row["Precision"] = 8;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Alltypes.Yfield";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "currency";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 4

            row = dataTable.NewRow();
            row["Parentid"] = "Alltypes";
            row["Name"] = "Dfield";
            row["Ordinal"] = 5;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Alltypes.Dfield";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "date";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 5

            row = dataTable.NewRow();
            row["Parentid"] = "Alltypes";
            row["Name"] = "Tfield";
            row["Ordinal"] = 6;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Alltypes.Tfield";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "datetime";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 6

            row = dataTable.NewRow();
            row["Parentid"] = "Alltypes";
            row["Name"] = "Bfield";
            row["Ordinal"] = 7;
            row["Isnullable"] = false;
            row["Maxlength"] = 8;
            row["Precision"] = 8;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Alltypes.Bfield";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "double";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 7

            row = dataTable.NewRow();
            row["Parentid"] = "Alltypes";
            row["Name"] = "Ffield";
            row["Ordinal"] = 8;
            row["Isnullable"] = false;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Alltypes.Ffield";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "numeric";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 8

            row = dataTable.NewRow();
            row["Parentid"] = "Alltypes";
            row["Name"] = "Gfield";
            row["Ordinal"] = 9;
            row["Isnullable"] = false;
            row["Maxlength"] = 2147483647;
            row["Precision"] = 2147483647;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Alltypes.Gfield";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "general";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 9

            row = dataTable.NewRow();
            row["Parentid"] = "Alltypes";
            row["Name"] = "Ifield";
            row["Ordinal"] = 10;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Alltypes.Ifield";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = true;
            row["Default"] = DBNull.Value;
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 10

            row = dataTable.NewRow();
            row["Parentid"] = "Alltypes";
            row["Name"] = "Lfield";
            row["Ordinal"] = 11;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Alltypes.Lfield";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "logical";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 11

            row = dataTable.NewRow();
            row["Parentid"] = "Alltypes";
            row["Name"] = "Nfield";
            row["Ordinal"] = 12;
            row["Isnullable"] = false;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Alltypes.Nfield";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "numeric";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 12

            row = dataTable.NewRow();
            row["Parentid"] = "Alltypes";
            row["Name"] = "Qfield";
            row["Ordinal"] = 13;
            row["Isnullable"] = false;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Alltypes.Qfield";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "general";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 13

            row = dataTable.NewRow();
            row["Parentid"] = "Alltypes";
            row["Name"] = "Vfield";
            row["Ordinal"] = 14;
            row["Isnullable"] = false;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Alltypes.Vfield";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 14

            row = dataTable.NewRow();
            row["Parentid"] = "Alltypes";
            row["Name"] = "Vfield2";
            row["Ordinal"] = 15;
            row["Isnullable"] = false;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Alltypes.Vfield2";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 15

            row = dataTable.NewRow();
            row["Parentid"] = "Alltypes";
            row["Name"] = "Mfield";
            row["Ordinal"] = 16;
            row["Isnullable"] = false;
            row["Maxlength"] = 2147483647;
            row["Precision"] = 2147483647;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Alltypes.Mfield";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "memo";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 16

            row = dataTable.NewRow();
            row["Parentid"] = "Alltypes";
            row["Name"] = "Mfield2";
            row["Ordinal"] = 17;
            row["Isnullable"] = false;
            row["Maxlength"] = 2147483647;
            row["Precision"] = 2147483647;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Alltypes.Mfield2";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "memo";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 17

            row = dataTable.NewRow();
            row["Parentid"] = "Candidates";
            row["Name"] = "Key1";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Candidates.Key1";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 18

            row = dataTable.NewRow();
            row["Parentid"] = "Candidates";
            row["Name"] = "Key2";
            row["Ordinal"] = 2;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Candidates.Key2";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 19

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C";
            row["Name"] = "Flaglabel";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = 50;
            row["Precision"] = 50;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C.Flaglabel";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 20

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C";
            row["Name"] = "Flagfield";
            row["Ordinal"] = 2;
            row["Isnullable"] = false;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C.Flagfield";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 21

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C";
            row["Name"] = "Flagvalue";
            row["Ordinal"] = 3;
            row["Isnullable"] = false;
            row["Maxlength"] = 250;
            row["Precision"] = 250;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C.Flagvalue";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 22

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C";
            row["Name"] = "Sequence";
            row["Ordinal"] = 4;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C.Sequence";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 23

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C";
            row["Name"] = "Descript";
            row["Ordinal"] = 5;
            row["Isnullable"] = false;
            row["Maxlength"] = 2147483647;
            row["Precision"] = 2147483647;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C.Descript";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "memo";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 24

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C";
            row["Name"] = "Active";
            row["Ordinal"] = 6;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C.Active";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "logical";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 25

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C";
            row["Name"] = "Flagid";
            row["Ordinal"] = 7;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C.Flagid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 26

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C";
            row["Name"] = "Dsid";
            row["Ordinal"] = 8;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C.Dsid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 27

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C";
            row["Name"] = "Property";
            row["Ordinal"] = 9;
            row["Isnullable"] = false;
            row["Maxlength"] = 2147483647;
            row["Precision"] = 2147483647;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C.Property";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "memo";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 28

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C";
            row["Name"] = "Tt";
            row["Ordinal"] = 10;
            row["Isnullable"] = false;
            row["Maxlength"] = 1;
            row["Precision"] = 1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C.Tt";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 29

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C1";
            row["Name"] = "Flaglabel";
            row["Ordinal"] = 1;
            row["Isnullable"] = false;
            row["Maxlength"] = 50;
            row["Precision"] = 50;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C1.Flaglabel";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 30

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C1";
            row["Name"] = "Flagfield";
            row["Ordinal"] = 2;
            row["Isnullable"] = false;
            row["Maxlength"] = 10;
            row["Precision"] = 10;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C1.Flagfield";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = true;
            row["Default"] = DBNull.Value;
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 31

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C1";
            row["Name"] = "Flagvalue";
            row["Ordinal"] = 3;
            row["Isnullable"] = false;
            row["Maxlength"] = 250;
            row["Precision"] = 250;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C1.Flagvalue";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 32

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C1";
            row["Name"] = "Sequence";
            row["Ordinal"] = 4;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C1.Sequence";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 33

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C1";
            row["Name"] = "Descript";
            row["Ordinal"] = 5;
            row["Isnullable"] = false;
            row["Maxlength"] = 2147483647;
            row["Precision"] = 2147483647;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C1.Descript";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "memo";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 34

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C1";
            row["Name"] = "Active";
            row["Ordinal"] = 6;
            row["Isnullable"] = false;
            row["Maxlength"] = -1;
            row["Precision"] = -1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C1.Active";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "logical";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 35

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C1";
            row["Name"] = "Flagid";
            row["Ordinal"] = 7;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C1.Flagid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 36

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C1";
            row["Name"] = "Dsid";
            row["Ordinal"] = 8;
            row["Isnullable"] = false;
            row["Maxlength"] = 4;
            row["Precision"] = 4;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C1.Dsid";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "integer";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 37

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C1";
            row["Name"] = "Property";
            row["Ordinal"] = 9;
            row["Isnullable"] = false;
            row["Maxlength"] = 2147483647;
            row["Precision"] = 2147483647;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C1.Property";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "memo";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 38

            row = dataTable.NewRow();
            row["Parentid"] = "Sample_C1";
            row["Name"] = "Tt";
            row["Ordinal"] = 10;
            row["Isnullable"] = false;
            row["Maxlength"] = 1;
            row["Precision"] = 1;
            row["Scale"] = 0;
            row["Datetimeprecision"] = DBNull.Value;
            row["Charactersetcatalog"] = DBNull.Value;
            row["Charactersetschema"] = DBNull.Value;
            row["Charactersetname"] = DBNull.Value;
            row["Collationcatalog"] = DBNull.Value;
            row["Collationschema"] = DBNull.Value;
            row["Collationname"] = DBNull.Value;
            row["Id"] = "Sample_C1.Tt";
            row["Ismultiset"] = false;
            row["Isstoregenerated"] = false;
            row["Isidentity"] = false;
            row["Default"] = DBNull.Value;
            row["Typename"] = "character";
            dataTable.Rows.Add(row);

            #endregion row

            #endregion rows

            return dataTable;
        }

        public static DataTable GetTables() {
            DataTable dataTable = new DataTable();

            #region columns

            dataTable.Columns.Add("Catalogname", typeof(System.String));
            dataTable.Columns.Add("Schemaname", typeof(System.String));
            dataTable.Columns.Add("Name", typeof(System.String));
            dataTable.Columns.Add("Id", typeof(System.String));

            #endregion columns

            #region rows

            DataRow row;

            #region row 0

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Alltypes";
            row["Id"] = "Alltypes";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 1

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Candidates";
            row["Id"] = "Candidates";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 2

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Sample_C";
            row["Id"] = "Sample_C";
            dataTable.Rows.Add(row);

            #endregion row

            #region row 3

            row = dataTable.NewRow();
            row["Catalogname"] = DBNull.Value;
            row["Schemaname"] = DBNull.Value;
            row["Name"] = "Sample_C1";
            row["Id"] = "Sample_C1";
            dataTable.Rows.Add(row);

            #endregion row

            #endregion rows

            return dataTable;
        }
    }
}