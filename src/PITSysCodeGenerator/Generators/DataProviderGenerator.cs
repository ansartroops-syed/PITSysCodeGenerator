using PITSysCodeGenerator.Helpers;
using PITSysCodeGenerator.Models;
using System.Text;

namespace PITSysCodeGenerator.Generators
{
    public class DataProviderGenerator : ICodeGenerator
    {
        public string Generate(GeneratorOptions options)
        {
            var sb = new StringBuilder();
            var dp = options.DataProviderClassName;
            var cn = options.ContractClassName;
            var tmp = options.TempTableName ?? (options.Prefix + options.BaseClassName + "Tmp");
            var v = D365NamingHelper.ToCamelCase(tmp);
            sb.AppendLine("[SRSReportParameterAttribute(classStr(" + cn + "))]");
            sb.AppendLine("class " + dp + " extends SRSReportDataProviderBase");
            sb.AppendLine("{");
            sb.AppendLine("    " + tmp + " " + v + ";");
            sb.AppendLine();
            sb.AppendLine("    [SRSReportDataSetAttribute(tableStr(" + tmp + "))]");
            sb.AppendLine("    public " + tmp + " get" + tmp + "()");
            sb.AppendLine("    {");
            sb.AppendLine("        select " + v + ";");
            sb.AppendLine("        return " + v + ";");
            sb.AppendLine("    }");
            sb.AppendLine();
            sb.AppendLine("    public void processReport()");
            sb.AppendLine("    {");
            sb.AppendLine("        " + cn + " contract = this.parmDataContract() as " + cn + ";");
            sb.AppendLine("        // TODO: Populate temp table");
            sb.AppendLine("    }");
            sb.AppendLine("}");
            return sb.ToString();
        }

        public string GetFileName(GeneratorOptions options)
        {
            return options.DataProviderClassName;
        }
    }
}
