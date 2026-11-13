using System;
using System.IO;
using System.Linq;

namespace Assignment_01.Data
{
    internal static class DataFiles
    {
        // Look next to the executable first (the .csproj copies the data files
        // there), then fall back to the current directory when running from
        // Visual Studio or from a custom build output.
        public static string Resolve(string fileName)
        {
            string outputPath = Path.Combine(AppContext.BaseDirectory, fileName);

            return File.Exists(outputPath) ? outputPath : fileName;
        }

        public static string[] LoadWords(string fileName)
            => File.ReadAllLines(Resolve(fileName))
                   .Where(word => !string.IsNullOrWhiteSpace(word))
                   .ToArray();
    }
}
