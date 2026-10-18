using PITSysCodeGenerator.Models;

namespace PITSysCodeGenerator.Generators
{
    /// <summary>
    /// Interface for all X++ code generators.
    /// </summary>
    public interface ICodeGenerator
    {
        /// <summary>
        /// Generates X++ code based on the provided options.
        /// </summary>
        /// <param name="options">Generator configuration options</param>
        /// <returns>Generated X++ source code as a string</returns>
        string Generate(GeneratorOptions options);

        /// <summary>
        /// Gets the file name for the generated output (without extension).
        /// </summary>
        string GetFileName(GeneratorOptions options);
    }
}