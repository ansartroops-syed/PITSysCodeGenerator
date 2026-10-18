using PITSysCodeGenerator.Helpers;
using PITSysCodeGenerator.Models;
using System.Text;

namespace PITSysCodeGenerator.Generators
{
    /// <summary>
    /// Generates X++ UIBuilder class extending SysOperationAutomaticUIBuilder.
    /// </summary>
    public class UIBuilderGenerator : ICodeGenerator
    {
        public string Generate(GeneratorOptions options)
        {
            var sb = new StringBuilder();
            var className = options.UIBuilderClassName;
            var contractName = options.ContractClassName;

            // File header
            sb.Append(CodeFormatter.GenerateFileHeader(className,
                $"UI Builder for {options.BaseClassName} SysOperation dialog customization."));

            // Class declaration
            sb.AppendLine($"class {className} extends SysOperationAutomaticUIBuilder");
            sb.AppendLine("{");

            // Dialog field declarations
            if (options.Fields != null && options.Fields.Count > 0)
            {
                sb.AppendLine($"{CodeFormatter.Indent}// Dialog field controls");
                foreach (var field in options.Fields)
                {
                    sb.AppendLine($"{CodeFormatter.Indent}DialogField dlg{D365NamingHelper.ToPascalCase(field.FieldName)};");
                }
                sb.AppendLine();
            }

            // postBuild method
            sb.Append(CodeFormatter.GenerateMethodComment(
                "Called after the dialog is built. Use to customize dialog controls."));
            sb.AppendLine($"{CodeFormatter.Indent}public void postBuild()");
            sb.AppendLine($"{CodeFormatter.Indent}{{");
            sb.AppendLine($"{CodeFormatter.Indent2}super();");
            sb.AppendLine();
            sb.AppendLine($"{CodeFormatter.Indent2}{contractName} contract = this.dataContractObject() as {contractName};");
            sb.AppendLine();

            if (options.Fields != null && options.Fields.Count > 0)
            {
                sb.AppendLine($"{CodeFormatter.Indent2}// Bind dialog fields to contract parm methods");
                foreach (var field in options.Fields)
                {
                    var pascalName = D365NamingHelper.ToPascalCase(field.FieldName);
                    sb.AppendLine($"{CodeFormatter.Indent2}dlg{pascalName} = this.bindInfo().getDialogField(contract, methodStr({contractName}, {field.ParmMethodName}));");
                }
                sb.AppendLine();

                sb.AppendLine($"{CodeFormatter.Indent2}// TODO: Customize dialog fields here");
                sb.AppendLine($"{CodeFormatter.Indent2}// Example: dlg{D365NamingHelper.ToPascalCase(options.Fields[0].FieldName)}.registerOverrideMethod(methodStr(FormStringControl, modified), methodStr({className}, {options.Fields[0].FieldName}_modified), this);");
            }

            sb.AppendLine($"{CodeFormatter.Indent}}}");

            // postRun method
            sb.AppendLine();
            sb.Append(CodeFormatter.GenerateMethodComment(
                "Called after the dialog has been fully initialized. Use for registering event handlers."));
            sb.AppendLine($"{CodeFormatter.Indent}public void postRun()");
            sb.AppendLine($"{CodeFormatter.Indent}{{");
            sb.AppendLine($"{CodeFormatter.Indent2}super();");
            sb.AppendLine();
            sb.AppendLine($"{CodeFormatter.Indent2}// TODO: Register additional event handlers or modify control properties");
            sb.AppendLine($"{CodeFormatter.Indent}}}");

            // Sample modified event handler (if fields exist)
            if (options.Fields != null && options.Fields.Count > 0)
            {
                var firstField = options.Fields[0];
                sb.AppendLine();
                sb.Append(CodeFormatter.GenerateMethodComment(
                    $"Event handler for {firstField.FieldName} field modification.",
                    "true if the value change is accepted.",
                    ("_control", "The form control that triggered the event")));
                sb.AppendLine($"{CodeFormatter.Indent}public boolean {firstField.FieldName}_modified(FormControl _control)");
                sb.AppendLine($"{CodeFormatter.Indent}{{");
                sb.AppendLine($"{CodeFormatter.Indent2}boolean ret = _control.modified();");
                sb.AppendLine();
                sb.AppendLine($"{CodeFormatter.Indent2}if (ret)");
                sb.AppendLine($"{CodeFormatter.Indent2}{{");
                sb.AppendLine($"{CodeFormatter.Indent3}// TODO: Handle {firstField.FieldName} value change");
                sb.AppendLine($"{CodeFormatter.Indent2}}}");
                sb.AppendLine();
                sb.AppendLine($"{CodeFormatter.Indent2}return ret;");
                sb.AppendLine($"{CodeFormatter.Indent}}}");
            }

            sb.AppendLine("}");

            return sb.ToString();
        }

        public string GetFileName(GeneratorOptions options) => options.UIBuilderClassName;
    }
}