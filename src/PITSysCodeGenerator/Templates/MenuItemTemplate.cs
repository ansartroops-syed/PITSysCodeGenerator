namespace PITSysCodeGenerator.Templates
{
    /// <summary>
    /// Template constants for MenuItem XML generation.
    /// </summary>
    public static class MenuItemTemplate
    {
        public const string ActionMenuItemFormat = @"<?xml version=""1.0"" encoding=""utf-8""?>
<AxMenuItemAction xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"">
    <Name>{0}</Name>
    <Label>{1}</Label>
    <ObjectType>Class</ObjectType>
    <Object>{2}</Object>
    <RunOn>Server</RunOn>
    <NeedsRecord>No</NeedsRecord>
    <OpenMode>Auto</OpenMode>
</AxMenuItemAction>";
    }
}