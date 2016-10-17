namespace SqlColumnSchemaExtractor 
{
    public class Arguments
    {
        public string ConnectionString { get; set; }

        public string SqlStatement { get; set; }

        public string OutputFile { get; set; }

        public bool Verbose { get; set; }

        public bool UseSchemeOnlyCommandBehavior { get; set; }

        public System.Collections.Generic.IList<string> UnrecognizedParameters { get; set; }
    }
}