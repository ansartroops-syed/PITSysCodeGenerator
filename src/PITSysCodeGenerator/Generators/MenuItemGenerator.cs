using PITSysCodeGenerator.Helpers;
using PITSysCodeGenerator.Models;
using System.Text;

namespace PITSysCodeGenerator.Generators
{
    public class MenuItemGenerator : ICodeGenerator
    {
        public string Generate(GeneratorOptions options)
        {
            var sb = new StringBuilder();
            var mi = options.MenuItemName;
            var ctrl = options.ControllerClassName;
            var cap = options.BatchJobCaption ?? options.BaseClassName;
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<AxMenuItemAction xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">");
            sb.AppendLine("    <Name>" + mi + "</Name>");
            sb.AppendLine("    <Label>" + cap + "</Label>");
            sb.AppendLine("    <ObjectType>Class</ObjectType>");
            sb.AppendLine("    <Object>" + ctrl + "</Object>");
            sb.AppendLine("    <RunOn>Server</RunOn>");
            sb.AppendLine("</AxMenuItemAction>");
            return sb.ToString();
        }

        public string GetFileName(GeneratorOptions options)
        {
            return options.MenuItemName + "_MenuItem";
        }
    }
}
