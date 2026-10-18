using PITSysCodeGenerator.Helpers; using PITSysCodeGenerator.Models; using System.Text;
namespace PITSysCodeGenerator.Generators {
public class DataProviderGenerator : ICodeGenerator {
public string Generate(GeneratorOptions o) {
var s = new StringBuilder(); var c = o.DataProviderClassName; var n = o.ContractClassName; var t = o.TempTableName ?? o.Prefix + o.BaseClassName + "Tmp";
s.AppendLine("[SRSReportParameterAttribute(classStr(" + n + "))]");
s.AppendLine("class " + c + " extends SRSReportDataProviderBase");
s.AppendLine("{");
s.AppendLine("    [SRSReportDataSetAttribute(tableStr(" + t + "))]");
s.AppendLine("    public " + t + " get" + t + "() { select " + D365NamingHelper.ToCamelCase(t) + "; return " + D365NamingHelper.ToCamelCase(t) + "; }");
s.AppendLine("    public void processReport() { " + n + " contract = this.parmDataContract() as " + n + "; // TODO }");
s.AppendLine("}");
return s.ToString();
} public string GetFileName(GeneratorOptions o) => o.DataProviderClassName; }}
