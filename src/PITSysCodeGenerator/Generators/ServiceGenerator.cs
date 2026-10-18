using PITSysCodeGenerator.Helpers;
using PITSysCodeGenerator.Models;
using System.Text;

namespace PITSysCodeGenerator.Generators
{
    /// <summary>
    /// Generates X++ Service class with the process() method.
    /// </summary>
    public class ServiceGenerator : ICodeGenerator
    {
        public string Generate(GeneratorOptions options)
        {
            var sb = new StringBuilder();
            var className = options.ServiceClassName;
            var contractName = options.ContractClassName;

            // File header
            sb.Append(CodeFormatter.GenerateFileHeader(className,
                $"Service class for {options.BaseClassName} SysOperation. Contains the business logic."));

            // Class declaration
            sb.AppendLine($"class {className}");
            sb.AppendLine("{");

            // Process method comment
            sb.Append(CodeFormatter.GenerateMethodComment(
                $"Main processing method for {options.BaseClassName}.",
                returns: null,
                ("_contract", $"The {contractName} data contract containing input parameters")));

            // Process method
            sb.AppendLine($"{CodeFormatter.Indent}/// <remarks>");
            sb.AppendLine($"{CodeFormatter.Indent}/// This method is called by the controller/framework. Implement your business logic here.");
            sb.AppendLine($"{CodeFormatter.Indent}/// </remarks>");
            sb.AppendLine($"{CodeFormatter.Indent}public void process({contractName} _contract)");
            sb.AppendLine($"{CodeFormatter.Indent}{{");

            // Generate local variable declarations from contract fields
            if (options.Fields != null && options.Fields.Count > 0)
            {
                sb.AppendLine($"{CodeFormatter.Indent2}// Retrieve parameters from contract");
                foreach (var field in options.Fields)
                {
                    sb.AppendLine($"{CodeFormatter.Indent2}{field.EdtType} {field.FieldName} = _contract.{field.ParmMethodName}();");
                }
                sb.AppendLine();
            }

            sb.AppendLine($"{CodeFormatter.Indent2}// TODO: Implement business logic here");
            sb.AppendLine($"{CodeFormatter.Indent2}try");
            sb.AppendLine($"{CodeFormatter.Indent2}{{");
            sb.AppendLine($"{CodeFormatter.Indent3}ttsbegin;");
            sb.AppendLine();
            sb.AppendLine($"{CodeFormatter.Indent3}// Your processing logic");
            sb.AppendLine();
            sb.AppendLine($"{CodeFormatter.Indent3}ttscommit;");
            sb.AppendLine($"{CodeFormatter.Indent2}}}");
            sb.AppendLine($"{CodeFormatter.Indent2}catch (Exception::Error)");
            sb.AppendLine($"{CodeFormatter.Indent2}{{");
            sb.AppendLine($"{CodeFormatter.Indent3}error(\"An error occurred during {options.BaseClassName} processing.\");");
            sb.AppendLine($"{CodeFormatter.Indent2}}}");

            sb.AppendLine($"{CodeFormatter.Indent}}}");
            sb.AppendLine("}");

            return sb.ToString();
        }

        public string GetFileName(GeneratorOptions options) => options.ServiceClassName;
    }
}