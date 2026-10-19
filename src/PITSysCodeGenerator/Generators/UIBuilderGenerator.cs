using PITSysCodeGenerator.Helpers;
using PITSysCodeGenerator.Models;
using System.Text;

namespace PITSysCodeGenerator.Generators
{
    public class UIBuilderGenerator : ICodeGenerator
    {
        public string Generate(GeneratorOptions options)
        {
            var sb = new StringBuilder();
            var ub = options.UIBuilderClassName;
            var cn = options.ContractClassName;
            sb.AppendLine("class " + ub + " extends SysOperationAutomaticUIBuilder");
            sb.AppendLine("{");
            if (options.Fields != null)
            {
                foreach (var f in options.Fields)
                    sb.AppendLine("    DialogField dlg" + D365NamingHelper.ToPascalCase(f.FieldName) + ";");
                sb.AppendLine();
            }
            sb.AppendLine("    public void postBuild()");
            sb.AppendLine("    {");
            sb.AppendLine("        super();");
            sb.AppendLine("        " + cn + " contract = this.dataContractObject() as " + cn + ";");
            if (options.Fields != null)
            {
                foreach (var f in options.Fields)
                    sb.AppendLine("        dlg" + D365NamingHelper.ToPascalCase(f.FieldName) + " = this.bindInfo().getDialogField(contract, methodStr(" + cn + ", " + f.ParmMethodName + "));");
            }
            sb.AppendLine("    }");
            sb.AppendLine();
            sb.AppendLine("    public void postRun()");
            sb.AppendLine("    {");
            sb.AppendLine("        super();");
            sb.AppendLine("    }");
            sb.AppendLine("}");
            return sb.ToString();
        }

        public string GetFileName(GeneratorOptions options)
        {
            return options.UIBuilderClassName;
        }
    }
}
