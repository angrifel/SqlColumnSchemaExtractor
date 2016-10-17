namespace SqlColumnSchemaExtractor
{
    public static class CSVFunctions
    {
        public static void WriteCSVLine(System.IO.TextWriter writer, string[] values)
        {
            if (values[0] != null)
            {
                WriteCSVValue(writer, values[0]);
            }

            for (int i = 1; i < values.Length; i++)
            {
                writer.Write(',');
                if (values[i] != null)
                {
                    WriteCSVValue(writer, values[i]);
                }
            }

            writer.WriteLine();
        }

        public static void WriteCSVValue(System.IO.TextWriter writer, string @value)
        {
            writer.Write("\"");
            writer.Write(@value.Replace("\"", "\"\""));
            writer.Write("\"");
        }
    }
}