using PITSysCodeGenerator.Helpers; using PITSysCodeGenerator.Models; using System.Text;
namespace PITSysCodeGenerator.Generators {
public class UIBuilderGenerator : ICodeGenerator {
public string Generate(GeneratorOptions o) {
var s = new StringBuilder(); var c = o.UIBuilderClassName; var n = o.ContractClassName;
s.AppendLine("class " + c + " extends SysOperationAutomaticUIBuilder");
s.AppendLine("{");
s.AppendLine("    public void postBuild() { super(); " + n + " contract = this.dataContractObject() as " + n + "; // Bind fields }");
s.AppendLine("    public void postRun() { super(); }");
s.AppendLine("}");
return s.ToString();
} public string GetFileName(GeneratorOptions o) => o.UIBuilderClassName; }}
