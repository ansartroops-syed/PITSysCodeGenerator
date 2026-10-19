using PITSysCodeGenerator.Models;

namespace PITSysCodeGenerator.Generators
{
    public interface ICodeGenerator
    {
        string Generate(GeneratorOptions options);
        string GetFileName(GeneratorOptions options);
    }
}
