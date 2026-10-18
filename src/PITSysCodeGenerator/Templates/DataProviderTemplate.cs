namespace PITSysCodeGenerator.Templates
{
    /// <summary>
    /// Template constants for DataProvider (RDP) class generation.
    /// </summary>
    public static class DataProviderTemplate
    {
        public const string BaseClass = "SRSReportDataProviderBase";
        public const string ParameterAttributeFormat = "[SRSReportParameterAttribute(classStr({0}))]";
        public const string DataSetAttributeFormat = "[SRSReportDataSetAttribute(tableStr({0}))]";
    }
}