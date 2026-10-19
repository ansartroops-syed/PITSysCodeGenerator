using PITSysCodeGenerator.Helpers;
using PITSysCodeGenerator.Models;
using System.Text;

namespace PITSysCodeGenerator.Generators
{
    public class ContractGenerator : ICodeGenerator
    {
        public string Generate(GeneratorOptions options)
        {
            var sb = new StringBuilder();
            var cn = options.ContractClassName;
            sb.AppendLine("/// <summary>");
            sb.AppendLine("/// Data contract for " + options.BaseClassName + ".");
            sb.AppendLine("/// </summary>");
            sb.AppendLine("[DataContractAttribute]");
            sb.AppendLine("class " + cn);
            sb.AppendLine("{");
            if (options.Fields != null)
            {
                foreach (var f in options.Fields)
                    sb.AppendLine("    " + f.EdtType + " " + f.FieldName + ";");
                sb.AppendLine();
                for (int i = 0; i < options.Fields.Count; i++)
                {
                    var f = options.Fields[i];
                    if (i > 0) sb.AppendLine();
                    sb.AppendLine("    [DataMemberAttribute('" + f.ParmMethodName + "'),");
                    if (!string.IsNullOrEmpty(f.Label))
                        sb.AppendLine("     SysOperationLabelAttribute(literalStr(\"" + f.Label + "\")),");
                    if (f.IsMandatory)
                        sb.AppendLine("     SysOperationMandatoryAttribute,");
                    sb.AppendLine("     SysOperationDisplayOrderAttribute('" + f.DisplayOrder + "')]");
                    sb.AppendLine("    public " + f.EdtType + " " + f.ParmMethodName + "(" + f.EdtType + " _" + f.FieldName + " = " + f.FieldName + ")");
                    sb.AppendLine("    {");
                    sb.AppendLine("        " + f.FieldName + " = _" + f.FieldName + ";");
                    sb.AppendLine("        return " + f.FieldName + ";");
                    sb.AppendLine("    }");
                }
            }
            sb.AppendLine("}");
            return sb.ToString();
        }

        public string GetFileName(GeneratorOptions options)
        {
            return options.ContractClassName;
        }
    }
}
