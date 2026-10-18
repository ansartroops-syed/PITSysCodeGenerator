namespace PITSysCodeGenerator.Templates
{
    /// <summary>
    /// Template constants for Service class generation.
    /// </summary>
    public static class ServiceTemplate
    {
        public const string ProcessMethodSignatureFormat = "public void process({0} _contract)";

        public const string ProcessMethodBodyTemplate = @"
        // TODO: Implement business logic here
        try
        {{
            ttsbegin;

            // Your processing logic

            ttscommit;
        }}
        catch (Exception::Error)
        {{
            error(""An error occurred during processing."");
        }}";
    }
}