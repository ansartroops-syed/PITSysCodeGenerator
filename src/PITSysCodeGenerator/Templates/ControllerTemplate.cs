namespace PITSysCodeGenerator.Templates
{
    /// <summary>
    /// Template constants for Controller class generation.
    /// </summary>
    public static class ControllerTemplate
    {
        public const string BaseClass = "SysOperationServiceController";
        public const string ConstructorFormat = @"
    public void new()
    {{
        super();
        this.parmClassName(classStr({0}));
        this.parmMethodName(methodStr({0}, process));
        this.parmDialogCaption(""{1}"");
    }}";

        public const string MainMethodFormat = @"
    public static void main(Args _args)
    {{
        {0} controller = {0}::construct();
        controller.parmShowBatchTab(true);
        controller.startOperation();
    }}";

        public const string TemplateFormMethod = @"
    public FormName templateForm()
    {{
        return formStr(SysOperationTemplateForm);
    }}";
    }
}