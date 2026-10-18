using PITSysCodeGenerator.Helpers; using PITSysCodeGenerator.Models; using System.Text;
namespace PITSysCodeGenerator.Generators {
public class MenuItemGenerator : ICodeGenerator {
public string Generate(GeneratorOptions o) {
var s = new StringBuilder(); s.AppendLine("<?xml version=\"1.0\"?>");
s.AppendLine("<AxMenuItemAction>");
s.AppendLine("    <Name>" + o.MenuItemName + "</Name>");
s.AppendLine("    <Label>" + (o.BatchJobCaption ?? o.BaseClassName) + "</Label>");
s.AppendLine("    <ObjectType>Class</ObjectType>");
s.AppendLine("    <Object>" + o.ControllerClassName + "</Object>");
s.AppendLine("</AxMenuItemAction>");
return s.ToString();
} public string GetFileName(GeneratorOptions o) => o.MenuItemName + "_MenuItem"; }}
