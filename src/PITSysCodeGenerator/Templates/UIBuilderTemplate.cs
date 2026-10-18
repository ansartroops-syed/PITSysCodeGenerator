namespace PITSysCodeGenerator.Templates
{
    /// <summary>
    /// Template constants for UIBuilder class generation.
    /// </summary>
    public static class UIBuilderTemplate
    {
        public const string BaseClass = "SysOperationAutomaticUIBuilder";
        public const string DialogFieldFormat = "DialogField dlg{0};";
        public const string BindInfoFormat = "dlg{0} = this.bindInfo().getDialogField(contract, methodStr({1}, {2}));";
    }
}