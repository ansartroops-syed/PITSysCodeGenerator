using PITSysCodeGenerator.Helpers; using PITSysCodeGenerator.Models; using System.Text;
namespace PITSysCodeGenerator.Generators {
public class ServiceGenerator : ICodeGenerator {
public string Generate(GeneratorOptions o) {
var s = new StringBuilder(); var c = o.ServiceClassName; var n = o.ContractClassName;
s.AppendLine("class " + c); s.AppendLine("{");
s.AppendLine("    public void process(" + n + " _contract) {");
if (o.Fields != null && o.Fields.Count > 0) {
s.AppendLine("        // Retrieve parameters");
foreach (var f in o.Fields) s.AppendLine("        " + f.EdtType + " " + f.FieldName + " = _contract." + f.ParmMethodName + "();");
}
s.AppendLine("        try { ttsbegin; // TODO: logic ttscommit; } catch { error(\"Error\"); }");
s.AppendLine("    }"); s.AppendLine("}"); return s.ToString(); }
public string GetFileName(GeneratorOptions o) => o.ServiceClassName;
}}
