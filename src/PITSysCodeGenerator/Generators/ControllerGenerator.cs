using PITSysCodeGenerator.Helpers;
using PITSysCodeGenerator.Models;
using System.Text;

namespace PITSysCodeGenerator.Generators
{
    /// <summary>
    /// Generates X++ Controller class extending SysOperationServiceController.
    /// </summary>
    public class ControllerGenerator : ICodeGenerator
    {
        public string Generate(GeneratorOptions options)
        {
            var sb = new StringBuilder();
            var className = options.ControllerClassName;
            var serviceName = options.ServiceClassName;
            var contractName = options.ContractClassName;
            var caption = options.BatchJobCaption ?? options.BaseClassName;

            // File header
            sb.Append(CodeFormatter.GenerateFileHeader(className,
                $"Controller class for {options.BaseClassName} SysOperation."));

            // Class declaration
            sb.AppendLine($"class {className} extends SysOperationServiceController");
            sb.AppendLine("{");

            // Constructor: new()
            sb.AppendLine();
            sb.Append(CodeFormatter.GenerateMethodComment(
                "Constructs a new instance of the controller."));
            sb.AppendLine($"{CodeFormatter.Indent}public void new()");
            sb.AppendLine($"{CodeFormatter.Indent}{{");
            sb.AppendLine($"{CodeFormatter.Indent2}super();");
            sb.AppendLine();
            sb.AppendLine($"{CodeFormatter.Indent2}this.parmClassName(classStr({serviceName}));");
            sb.AppendLine($"{CodeFormatter.Indent2}this.parmMethodName(methodStr({serviceName}, process));");
            sb.AppendLine($"{CodeFormatter.Indent2}this.parmDialogCaption(\"{caption}\");");
            sb.AppendLine($"{CodeFormatter.Indent}}}");

            // Static construct method
            sb.AppendLine();
            sb.Append(CodeFormatter.GenerateMethodComment(
                $"Creates a new instance of {className}.",
                $"A new {className} instance."));
            sb.AppendLine($"{CodeFormatter.Indent}public static {className} construct()");
            sb.AppendLine($"{CodeFormatter.Indent}{{");
            sb.AppendLine($"{CodeFormatter.Indent2}return new {className}();");
            sb.AppendLine($"{CodeFormatter.Indent}}}");

            // Main entry point
            sb.AppendLine();
            sb.Append(CodeFormatter.GenerateMethodComment(
                "Main entry point for the batch job.",
                returns: null,
                ("_args", "The Args object passed from the menu item")));
            sb.AppendLine($"{CodeFormatter.Indent}public static void main(Args _args)");
            sb.AppendLine($"{CodeFormatter.Indent}{{");
            sb.AppendLine($"{CodeFormatter.Indent2}{className} controller = {className}::construct();");
            sb.AppendLine();

            if (options.EnableBatch)
            {
                sb.AppendLine($"{CodeFormatter.Indent2}controller.parmShowBatchTab(true);");
            }

            sb.AppendLine($"{CodeFormatter.Indent2}controller.startOperation();");
            sb.AppendLine($"{CodeFormatter.Indent}}}");

            // Caption method
            sb.AppendLine();
            sb.Append(CodeFormatter.GenerateMethodComment(
                "Returns the caption for the batch dialog.",
                "The batch job caption string."));
            sb.AppendLine($"{CodeFormatter.Indent}public ClassDescription caption()");
            sb.AppendLine($"{CodeFormatter.Indent}{{");
            sb.AppendLine($"{CodeFormatter.Indent2}return \"{caption}\";");
            sb.AppendLine($"{CodeFormatter.Indent}}}");

            // templateForm method (for SysOperation Builder pattern)
            sb.AppendLine();
            sb.Append(CodeFormatter.GenerateMethodComment(
                "Specifies the template form for the SysOperation dialog.",
                "The form name used as dialog template."));
            sb.AppendLine($"{CodeFormatter.Indent}public FormName templateForm()");
            sb.AppendLine($"{CodeFormatter.Indent}{{");
            sb.AppendLine($"{CodeFormatter.Indent2}// SysOperation Builder uses SysOperationTemplateForm by default");
            sb.AppendLine($"{CodeFormatter.Indent2}return formStr(SysOperationTemplateForm);");
            sb.AppendLine($"{CodeFormatter.Indent}}}");

            sb.AppendLine("}");

            return sb.ToString();
        }

        public string GetFileName(GeneratorOptions options) => options.ControllerClassName;
    }
}