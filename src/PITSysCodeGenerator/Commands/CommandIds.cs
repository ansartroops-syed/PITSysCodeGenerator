namespace PITSysCodeGenerator.Commands
{
    /// <summary>
    /// Command ID constants. Must match the IDs declared in the .vsct file.
    /// </summary>
    internal static class CommandIds
    {
        /// <summary>
        /// Command menu group GUID (must match .vsct guidPITSysCodeGeneratorCmdSet)
        /// </summary>
        public const string CommandSetGuidString = "D4E5F6A7-B8C9-0123-DEF0-1234567890AB";

        /// <summary>
        /// Command ID for the Generate Code command (must match .vsct cmdidGenerateCode)
        /// </summary>
        public const int GenerateCodeCommandId = 0x0100;
    }
}