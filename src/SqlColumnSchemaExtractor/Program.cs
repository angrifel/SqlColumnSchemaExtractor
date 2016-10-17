namespace SqlColumnSchemaExtractor
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.IO;
    using System.Text;

    public class Program
    {
        public static void Main(string[] args)
        {
            var outputEncoding = new UTF8Encoding(false, true);
            System.Console.OutputEncoding = outputEncoding;
            var arguments = ArgumentsFunctions.Parse(args);
            if (arguments == null)
            {
                ConsoleFunctions.DisplayHelpToStdout();
                return;
            }

            var outputStream = (Stream)null;
            var fileWriter = (TextWriter)null;
            var output = (TextWriter)null;
            var conn = (SqlConnection)null;
            try
            {
                var errorMessages = ArgumentsFunctions.ValidateArguments(arguments);
                if (errorMessages.Count > 0) 
                {
                    ConsoleFunctions.DisplayErrorMessages(errorMessages);
                    return;
                }

                outputStream = arguments.OutputFile != null ? File.Open(arguments.OutputFile, FileMode.Create, FileAccess.Write, FileShare.None) : (Stream)null;
                fileWriter = outputStream != null ? new StreamWriter(outputStream, outputEncoding) : null;
                output = fileWriter ?? Console.Out;
                if (arguments.Verbose)
                    ConsoleFunctions.DisplayArguments(arguments, output);

                using (conn = new SqlConnection(arguments.ConnectionString))
                {
                    if (arguments.Verbose)
                        output.WriteLine("# Connecting to server... ");
                    conn.Open();
                    using (var tran = conn.BeginTransaction(IsolationLevel.ReadUncommitted))
                    {
                        try
                        {
                            using (var command = new SqlCommand(arguments.SqlStatement, conn, tran))
                            {
                                if (arguments.Verbose)
                                    output.WriteLine("# Executing statement...");
                                using (var csvOutput = new StringWriter())
                                {
                                    using (var reader = command.ExecuteReader(arguments.UseSchemeOnlyCommandBehavior ? CommandBehavior.SchemaOnly : CommandBehavior.Default))
                                    {
                                        var columnSchema = reader.GetColumnSchema();
                                        if (columnSchema.Count > 0)
                                        {
                                            if (arguments.Verbose)
                                                output.WriteLine("# Enumerating result sets...");

                                            var resultSetIndex = 0;
                                            var @continue = true;
                                            var csvValues = new string[1 + DbColumnFunctions.GetDbColumnPropertyCount()];
                                            csvValues[0] = "ResultSetIndex";
                                            DbColumnFunctions.WriteDbColumnPropertyNames(csvValues, 1);
                                            CSVFunctions.WriteCSVLine(csvOutput, csvValues);

                                            for (int i = 0; i < csvValues.Length; i++)
                                                csvValues[i] = null;

                                            while (@continue)
                                            {
                                                csvValues[0] = resultSetIndex.ToString();
                                                for (int i = 0; i < columnSchema.Count; i++)
                                                {
                                                    DbColumnFunctions.WriteDbColumnPropertyValues(csvValues, 1, columnSchema[i]);
                                                    CSVFunctions.WriteCSVLine(csvOutput, csvValues);
                                                }

                                                resultSetIndex += 1;
                                                @continue = reader.NextResult();
                                                columnSchema = reader.GetColumnSchema();
                                            }
                                        }
                                        else
                                        {
                                            if (arguments.Verbose)
                                                output.WriteLine("# No result sets returned.");
                                        }
                                    }

                                    // we write the csv output to a separate reader just in case
                                    // we gen an exception from SQL server.
                                    // exceptions are not fired deterministically and may apear between csv lines.
                                    // by using a separate writer we only write the output when we are sure there
                                    // are no exceptions coming from the SqlDataReader.
                                    output.Write(csvOutput.ToString());
                                }
                            }
                        }
                        finally
                        {
                            tran.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("# Error while getting metadata: " + ex.Message);
                return;
            }
            finally
            {
                outputStream?.Flush();
                if (conn?.State != ConnectionState.Closed)
                    conn?.Close();
                fileWriter?.Dispose();
                outputStream?.Dispose();
            }
        }
    }
}
