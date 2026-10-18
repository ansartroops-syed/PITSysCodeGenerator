using PITSysCodeGenerator.Helpers; using PITSysCodeGenerator.Models; using System.Text;
namespace PITSysCodeGenerator.Generators {
public class ControllerGenerator : ICodeGenerator {
public string Generate(GeneratorOptions o) {
var s = new StringBuilder(); var c = o.ControllerClassName; var svc = o.ServiceClassName; var cap = o.BatchJobCaption ?? o.BaseClassName;
s.AppendLine("class " + c + " extends SysOperationServiceController"); s.AppendLine("{");
s.AppendLine("    public void new() { super(); this.parmClassName(classStr(" + svc + ")); this.parmMethodName(methodStr(" + svc + ", process)); this.parmDialogCaption(\"" + cap + "\"); }");
s.AppendLine("    public static " + c + " construct() { return new " + c + "(); }");
s.AppendLine("    public static void main(Args _args) { " + c + " ctrl = " + c + "::construct(); ctrl.parmShowBatchTab(true); ctrl.startOperation(); }");
s.AppendLine("    public ClassDescription caption() { return \"" + cap + "\"; }");
s.AppendLine("    public FormName templateForm() { return formStr(SysOperationTemplateForm); }");
s.AppendLine("}");
return s.ToString();
} public string GetFileName(GeneratorOptions o) => o.ControllerClassName; }}
