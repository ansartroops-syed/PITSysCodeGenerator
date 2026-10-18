using PITSysCodeGenerator.Helpers;
using PITSysCodeGenerator.Models;
using System.Text;

namespace PITSysCodeGenerator.Generators
{
    /// <summary>
    /// Generates X++ Report Data Provider class extending SysOperationRDPBase.
    /// </summary>
    public class DataProviderGenerator : ICodeGenerator
    {
        public string Generate(GeneratorOptions options)
        {
            var sb = new StringBuilder();
            var className = options.DataProviderClassName;
            var contractName = options.ContractClassName;
            var tempTableName = options.TempTableName ?? $"{options.Prefix}{options.BaseClassName}Tmp";

            // File header
            sb.Append(CodeFormatter.GenerateFileHeader(className,
                $"Report Data Provider for {options.BaseClassName}. Populates the {tempTableName} temporary table."));

            // Class attributes
            sb.AppendLine("[");
            sb.AppendLine($"{CodeFormatter.Indent}SRSReportParameterAttribute(classStr({contractName}))");
            sb.AppendLine("]");

            // Class declaration
            sb.AppendLine($"class {className} extends SRSReportDataProviderBase");
            sb.AppendLine("{");

            // Temp table field
            sb.AppendLine($"{CodeFormatter.Indent}{tempTableName} {D365NamingHelper.ToCamelCase(tempTableName)};");
            sb.AppendLine();

            // getTable method
            sb.Append(CodeFormatter.GenerateMethodComment(
                $"Returns the {tempTableName} table buffer.",
                $"The {tempTableName} table buffer with report data."));
            sb.AppendLine($"{CodeFormatter.Indent}[SRSReportDataSetAttribute(tableStr({tempTableName}))]");
            sb.AppendLine($"{CodeFormatter.Indent}public {tempTableName} get{tempTableName}()");
            sb.AppendLine($"{CodeFormatter.Indent}{{");
            sb.AppendLine($"{CodeFormatter.Indent2}select {D365NamingHelper.ToCamelCase(tempTableName)};");
            sb.AppendLine($"{CodeFormatter.Indent2}return {D365NamingHelper.ToCamelCase(tempTableName)};");
            sb.AppendLine($"{CodeFormatter.Indent}}}");

            // processReport method
            sb.AppendLine();
            sb.Append(CodeFormatter.GenerateMethodComment(
                "Main report processing method. Retrieves contract parameters and populates the temp table."));
            sb.AppendLine($"{CodeFormatter.Indent}public void processReport()");
            sb.AppendLine($"{CodeFormatter.Indent}{{");
            sb.AppendLine($"{CodeFormatter.Indent2}{contractName} contract = this.parmDataContract() as {contractName};");
            sb.AppendLine();

            // Retrieve parameters
            if (options.Fields != null && options.Fields.Count > 0)
            {
                sb.AppendLine($"{CodeFormatter.Indent2}// Retrieve parameters from contract");
                foreach (var field in options.Fields)
                {
                    sb.AppendLine($"{CodeFormatter.Indent2}{field.EdtType} {field.FieldName} = contract.{field.ParmMethodName}();");
                }
                sb.AppendLine();
            }

            sb.AppendLine($"{CodeFormatter.Indent2}// TODO: Query data and populate the temp table");
            sb.AppendLine($"{CodeFormatter.Indent2}// {D365NamingHelper.ToCamelCase(tempTableName)}.clear();");
            sb.AppendLine($"{CodeFormatter.Indent2}// {D365NamingHelper.ToCamelCase(tempTableName)}.insert();");

            sb.AppendLine($"{CodeFormatter.Indent}}}");
            sb.AppendLine("}");

            return sb.ToString();
        }

        public string GetFileName(GeneratorOptions options) => options.DataProviderClassName;
    }
}