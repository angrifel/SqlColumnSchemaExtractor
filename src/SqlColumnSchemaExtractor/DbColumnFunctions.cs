namespace SqlColumnSchemaExtractor
{
    public static class DbColumnFunctions
    {
        private readonly static string[] dbColumnProperties =
            new string[]
            {
                "AllowDBNull",
                "BaseCatalogName",
                "BaseColumnName",
                "BaseSchemaName",
                "BaseServerName",
                "BaseTableName",
                "ColumnName",
                "ColumnOrdinal",
                "ColumnSize",
                "IsAliased",
                "IsAutoIncrement",
                "IsExpression",
                "IsHidden",
                "IsIdentity",
                "IsKey",
                "IsLong",
                "IsReadOnly",
                "IsUnique",
                "NumericPrecision",
                "NumericScale",
                "UdtAssemblyQualifiedName",
                "DataType",
                "DataTypeName"
            };

        public static int GetDbColumnPropertyCount()
        {
            return dbColumnProperties.Length;
        }

        public static void WriteDbColumnPropertyNames(string[] output, int startIndex)
        {
            for (int i = 0; i < dbColumnProperties.Length; i++)
            {
                output[startIndex + i] = dbColumnProperties[i];
            }
        }

        public static void WriteDbColumnPropertyValues(string[] output, int startIndex, System.Data.Common.DbColumn dbColumn)
        {
            for (var i = 0; i < dbColumnProperties.Length; i++)
            {
                var @value = dbColumn[dbColumnProperties[i]];
                if (@value != null)
                {
                    output[startIndex + i] = @value.ToString();
                }
            }
        }
    }
}