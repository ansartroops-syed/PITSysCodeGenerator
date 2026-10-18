using PITSysCodeGenerator.Helpers;
using PITSysCodeGenerator.Models;
using System.Text;

namespace PITSysCodeGenerator.Generators
{
    /// <summary>
    /// Generates X++ Contract class with DataContractAttribute and parm methods.
    /// </summary>
    public class ContractGenerator : ICodeGenerator
    {
        public string Generate(GeneratorOptions options)
        {
            var sb = new StringBuilder();
            var className = options.ContractClassName;

            // File header
            sb.Append(CodeFormatter.GenerateFileHeader(className,
                $"Data contract class for {options.BaseClassName} SysOperation."));

            // Class attribute and declaration
            sb.AppendLine("[DataContractAttribute]");
            sb.AppendLine($"class {className}");
            sb.AppendLine("{");

            // Field declarations
            if (options.Fields != null && options.Fields.Count > 0)
            {
                foreach (var field in options.Fields)
                {
                    sb.AppendLine($"{CodeFormatter.Indent}{field.EdtType} {field.FieldName};");
                }
                sb.AppendLine();
            }

            // Parm methods
            if (options.Fields != null)
            {
                for (int i = 0; i < options.Fields.Count; i++)
                {
                    var field = options.Fields[i];
                    if (i > 0) sb.AppendLine();

                    // Method comment
                    sb.Append(CodeFormatter.GenerateMethodComment(
                        $"Gets or sets the {field.Label ?? field.FieldName} value.",
                        $"The current {field.EdtType} value.",
                        (field.FieldName, $"The {field.EdtType} value to set; optional")));

                    // DataMember attributes
                    sb.AppendLine($"{CodeFormatter.Indent}[DataMemberAttribute('{field.ParmMethodName}'),");

                    if (!string.IsNullOrEmpty(field.Label))
                    {
                        sb.AppendLine($"{CodeFormatter.Indent} SysOperationLabelAttribute(literalStr(\"{field.Label}\")),");
                    }

                    if (!string.IsNullOrEmpty(field.HelpText))
                    {
                        sb.AppendLine($"{CodeFormatter.Indent} SysOperationHelpTextAttribute(literalStr(\"{field.HelpText}\")),");
                    }

                    if (field.IsMandatory)
                    {
                        sb.AppendLine($"{CodeFormatter.Indent} SysOperationMandatoryAttribute,");
                    }

                    if (!string.IsNullOrEmpty(field.GroupName))
                    {
                        sb.AppendLine($"{CodeFormatter.Indent} SysOperationGroupMemberAttribute('{field.GroupName}'),");
                    }

                    sb.AppendLine($"{CodeFormatter.Indent} SysOperationDisplayOrderAttribute('{field.DisplayOrder}')]");

                    // Method signature
                    sb.AppendLine($"{CodeFormatter.Indent}public {field.EdtType} {field.ParmMethodName}({field.EdtType} _{field.FieldName} = {field.FieldName})");
                    sb.AppendLine($"{CodeFormatter.Indent}{{");
                    sb.AppendLine($"{CodeFormatter.Indent2}{field.FieldName} = _{field.FieldName};");
                    sb.AppendLine($"{CodeFormatter.Indent2}return {field.FieldName};");
                    sb.AppendLine($"{CodeFormatter.Indent}}}");
                }
            }

            sb.AppendLine("}");
            return sb.ToString();
        }

        public string GetFileName(GeneratorOptions options) => options.ContractClassName;
    }
}