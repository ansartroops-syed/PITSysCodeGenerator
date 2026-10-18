using PITSysCodeGenerator.Helpers;
using PITSysCodeGenerator.Models;
using System.Text;
namespace PITSysCodeGenerator.Generators {
public class ContractGenerator : ICodeGenerator {
public string Generate(GeneratorOptions o) {
var s = new StringBuilder();
var c = o.ContractClassName;
s.AppendLine("[DataContractAttribute]");
s.AppendLine("class " + c);
s.AppendLine("{");
if (o.Fields != null && o.Fields.Count > 0) {
foreach (var f in o.Fields) s.AppendLine("    " + f.EdtType + " " + f.FieldName + ";");
s.AppendLine();
}
if (o.Fields != null) {
for (int i = 0; i < o.Fields.Count; i++) {
var f = o.Fields[i];
if (i > 0) s.AppendLine();
s.AppendLine("    [DataMemberAttribute('" + f.ParmMethodName + "'),");
if (!string.IsNullOrEmpty(f.Label)) s.AppendLine("     SysOperationLabelAttribute(literalStr(\"" + f.Label + "\")),");
if (f.IsMandatory) s.AppendLine("     SysOperationMandatoryAttribute,");
s.AppendLine("     SysOperationDisplayOrderAttribute('" + f.DisplayOrder + "')]");
s.AppendLine("    public " + f.EdtType + " " + f.ParmMethodName + "(" + f.EdtType + " _" + f.FieldName + " = " + f.FieldName + ")");
s.AppendLine("    {");
s.AppendLine("        " + f.FieldName + " = _" + f.FieldName + "; return " + f.FieldName + ";");
s.AppendLine("    }");
}} s.AppendLine("}"); return s.ToString(); }
public string GetFileName(GeneratorOptions o) => o.ContractClassName;
}}
