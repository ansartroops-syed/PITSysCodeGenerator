using PITSysCodeGenerator.Models;
using System.Text;

namespace PITSysCodeGenerator.Generators
{
    public class ControllerGenerator : ICodeGenerator
    {
        public string Generate(GeneratorOptions options)
        {
            var sb = new StringBuilder();
            var ctrl = options.ControllerClassName;
            var svc = options.ServiceClassName;
            var cap = options.BatchJobCaption ?? options.BaseClassName;
            sb.AppendLine("/// <summary>");
            sb.AppendLine("/// Controller for " + options.BaseClassName + ".");
            sb.AppendLine("/// </summary>");
            sb.AppendLine("class " + ctrl + " extends SysOperationServiceController");
            sb.AppendLine("{");
            sb.AppendLine("    public void new()");
            sb.AppendLine("    {");
            sb.AppendLine("        super();");
            sb.AppendLine("        this.parmClassName(classStr(" + svc + "));");
            sb.AppendLine("        this.parmMethodName(methodStr(" + svc + ", process));");
            sb.AppendLine("        this.parmDialogCaption(\"" + cap + "\");");
            sb.AppendLine("    }");
            sb.AppendLine();
            sb.AppendLine("    public static " + ctrl + " construct()");
            sb.AppendLine("    {");
            sb.AppendLine("        return new " + ctrl + "();");
            sb.AppendLine("    }");
            sb.AppendLine();
            sb.AppendLine("    public static void main(Args _args)");
            sb.AppendLine("    {");
            sb.AppendLine("        " + ctrl + " controller = " + ctrl + "::construct();");
            if (options.EnableBatch)
                sb.AppendLine("        controller.parmShowBatchTab(true);");
            sb.AppendLine("        controller.startOperation();");
            sb.AppendLine("    }");
            sb.AppendLine();
            sb.AppendLine("    public ClassDescription caption()");
            sb.AppendLine("    {");
            sb.AppendLine("        return \"" + cap + "\";");
            sb.AppendLine("    }");
            sb.AppendLine();
            sb.AppendLine("    public FormName templateForm()");
            sb.AppendLine("    {");
            sb.AppendLine("        return formStr(SysOperationTemplateForm);");
            sb.AppendLine("    }");
            sb.AppendLine("}");
            return sb.ToString();
        }

        public string GetFileName(GeneratorOptions options)
        {
            return options.ControllerClassName;
        }
    }
}
