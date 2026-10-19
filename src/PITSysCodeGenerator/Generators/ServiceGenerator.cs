using PITSysCodeGenerator.Models;
using System.Text;

namespace PITSysCodeGenerator.Generators
{
    public class ServiceGenerator : ICodeGenerator
    {
        public string Generate(GeneratorOptions options)
        {
            var sb = new StringBuilder();
            var sn = options.ServiceClassName;
            var cn = options.ContractClassName;
            sb.AppendLine("/// <summary>");
            sb.AppendLine("/// Service class for " + options.BaseClassName + ".");
            sb.AppendLine("/// </summary>");
            sb.AppendLine("class " + sn);
            sb.AppendLine("{");
            sb.AppendLine("    public void process(" + cn + " _contract)");
            sb.AppendLine("    {");
            if (options.Fields != null)
            {
                foreach (var f in options.Fields)
                    sb.AppendLine("        " + f.EdtType + " " + f.FieldName + " = _contract." + f.ParmMethodName + "();");
                sb.AppendLine();
            }
            sb.AppendLine("        // TODO: Implement business logic here");
            sb.AppendLine("        try");
            sb.AppendLine("        {");
            sb.AppendLine("            ttsbegin;");
            sb.AppendLine("            ttscommit;");
            sb.AppendLine("        }");
            sb.AppendLine("        catch (Exception::Error)");
            sb.AppendLine("        {");
            sb.AppendLine("            error(\"An error occurred during processing.\");");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("}");
            return sb.ToString();
        }

        public string GetFileName(GeneratorOptions options)
        {
            return options.ServiceClassName;
        }
    }
}
