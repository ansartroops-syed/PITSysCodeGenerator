using System;
using System.IO;
using System.Text;
using System.Windows;

namespace PITSysCodeGenerator.Helpers
{
    /// <summary>
    /// Helper for writing generated code to files and clipboard.
    /// </summary>
    public static class FileOutputHelper
    {
        /// <summary>
        /// Saves generated code to a file.
        /// </summary>
        /// <param name="directory">Output directory path</param>
        /// <param name="fileName">File name (without extension)</param>
        /// <param name="content">X++ code content</param>
        /// <param name="extension">File extension (default: .xpp)</param>
        /// <returns>Full path of the saved file</returns>
        public static string SaveToFile(string directory, string fileName, string content, string extension = ".xpp")
        {
            if (string.IsNullOrWhiteSpace(directory))
            {
                directory = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "PITSysCodeGenerator",
                    "Output");
            }

            Directory.CreateDirectory(directory);

            var fullPath = Path.Combine(directory, $"{fileName}{extension}");
            File.WriteAllText(fullPath, content, Encoding.UTF8);
            return fullPath;
        }

        /// <summary>
        /// Saves XML content (e.g., MenuItem definitions).
        /// </summary>
        public static string SaveXmlToFile(string directory, string fileName, string content)
        {
            return SaveToFile(directory, fileName, content, ".xml");
        }

        /// <summary>
        /// Copies content to the system clipboard.
        /// </summary>
        public static void CopyToClipboard(string content)
        {
            try
            {
                Clipboard.SetText(content);
            }
            catch (Exception)
            {
                // Clipboard may fail in certain contexts; silently ignore
            }
        }

        /// <summary>
        /// Opens the output directory in Windows Explorer.
        /// </summary>
        public static void OpenInExplorer(string directory)
        {
            if (Directory.Exists(directory))
            {
                System.Diagnostics.Process.Start("explorer.exe", directory);
            }
        }
    }
}