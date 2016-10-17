using System.IO;
using System.Reflection;

namespace SqlColumnSchemaExtractor
{
    public static class ConsoleFunctions
    {
        public static void DisplayHelpToStdout()
        {
            using (var helpSceenStream = Assembly.GetEntryAssembly().GetManifestResourceStream("SqlColumnsSchemaExtractor.Resources.HelpScreen.txt"))
            {
                using (var reader = new StreamReader(helpSceenStream))
                {
                    System.Console.WriteLine(reader.ReadToEnd());
                }
            }
        }

        public static void DisplayArguments(Arguments arguments, System.IO.TextWriter output)
        {
            output.WriteLine("# Arguments");
            output.WriteLine("#   ConnectionString: " + arguments.ConnectionString);
            output.WriteLine("#   SqlStatement: " + arguments.SqlStatement);
            output.WriteLine("#   OutputFile: " + arguments.OutputFile);
            output.WriteLine("#   UseSchemeOnlyCommandBehavior: " + arguments.UseSchemeOnlyCommandBehavior);
            output.WriteLine("#   Verbose: " + arguments.Verbose);
            if (arguments.UnrecognizedParameters.Count > 0)
            {
                output.WriteLine("# UnrecognizedArguments");
                for (int i = 0; i < arguments.UnrecognizedParameters.Count; i++)
                {
                    output.WriteLine("#   " + arguments.UnrecognizedParameters[i]);
                }
            }
        }

        public static void DisplayErrorMessages(System.Collections.Generic.IEnumerable<string> errorMessages) 
        {
            System.Console.Error.WriteLine("# Errors were found while parsing commandline arguments.");
            foreach (var errorMessage in errorMessages)
            {
                System.Console.Error.WriteLine("# " + errorMessage);
            }
        }
    }
}