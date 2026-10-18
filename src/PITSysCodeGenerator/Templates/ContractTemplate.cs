namespace PITSysCodeGenerator.Templates
{
    /// <summary>
    /// Template constants for Contract class generation.
    /// </summary>
    public static class ContractTemplate
    {
        public const string DataContractAttribute = "[DataContractAttribute]";
        public const string DataMemberAttributeFormat = "[DataMemberAttribute('{0}')]";
        public const string LabelAttributeFormat = "SysOperationLabelAttribute(literalStr(\"{0}\"))";
        public const string HelpTextAttributeFormat = "SysOperationHelpTextAttribute(literalStr(\"{0}\"))";
        public const string MandatoryAttribute = "SysOperationMandatoryAttribute";
        public const string DisplayOrderAttributeFormat = "SysOperationDisplayOrderAttribute('{0}')";
        public const string GroupMemberAttributeFormat = "SysOperationGroupMemberAttribute('{0}')";

        public const string ParmMethodFormat = @"
    public {0} {1}({0} _{2} = {2})
    {{
        {2} = _{2};
        return {2};
    }}";
    }
}