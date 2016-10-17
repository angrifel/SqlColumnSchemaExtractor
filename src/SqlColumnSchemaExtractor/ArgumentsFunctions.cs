namespace SqlColumnSchemaExtractor
{
    public static class ArgumentsFunctions
    {
        public static Arguments Parse(string[] args)
        {
            if (args.Length == 0) return null;
            var result = new Arguments() { UnrecognizedParameters = new System.Collections.Generic.List<string>() };
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                if (arg.Equals("-ConnectionString", System.StringComparison.OrdinalIgnoreCase))
                {
                    if (i + 1 < args.Length)
                    {
                        result.ConnectionString = args[i + 1];
                    }

                    i += 1;
                    continue;
                }
                else if (arg.Equals("-SqlStatement", System.StringComparison.OrdinalIgnoreCase))
                {
                    if (i + 1 < args.Length)
                    {
                        result.SqlStatement = args[i + 1];
                    }

                    result.SqlStatement = args[i + 1];
                    i += 1;
                    continue;
                }
                else if (arg.Equals("-OutputFile", System.StringComparison.OrdinalIgnoreCase))
                {
                    if (i + 1 < args.Length)
                    {
                        result.OutputFile = args[i + 1];
                    }

                    result.OutputFile = args[i + 1];
                    i += 1;
                    continue;
                }
                else if (arg.Equals("-UseSchemeOnlyCommandBehavior", System.StringComparison.OrdinalIgnoreCase))
                {
                    result.UseSchemeOnlyCommandBehavior = true;
                }
                else if (arg.Equals("-Verbose", System.StringComparison.OrdinalIgnoreCase))
                {
                    result.Verbose = true;
                }
                else
                {
                    result.UnrecognizedParameters.Add(arg);
                }
            }

            return result;
        }

        public static System.Collections.Generic.IList<string> ValidateArguments(Arguments arguments)
        {
            var result = new System.Collections.Generic.List<string>();

            if (string.IsNullOrWhiteSpace(arguments.ConnectionString))
            {
                result.Add("ConnectionString was not supplied or is empty.");
            }
            if (string.IsNullOrWhiteSpace(arguments.SqlStatement))
            {
                result.Add("SqlStatement was not supplied or is empty");
            }
            if (arguments.UnrecognizedParameters.Count > 0)
            {
                result.Add("Unrecognized parameters were found.");
            }

            return result;
        }
    }
}