using System;
using System.Linq;

namespace NonSolutionFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            GOTO_RETRY:
            Console.WriteLine("Usage:");
            Console.WriteLine("NonSolutionFiles.exe [absolute path to your c# solution file] [excludeFileContaingString1] [excludeFileContainingString2] [...]");

            Console.WriteLine("Input absolute path to your c# solution file");
            var solutionPath = Console.ReadLine();
            Console.WriteLine("[excludeFileContaingString1] [excludeFileContainingString2] [...]");
            var exludeFilesContaining = Console.ReadLine().Split(' ');
            var fileReader = new FileReader();
            var findNonSolutionFiles = new FindNonSolutionFiles(new FilesOnDisk(), new FilesInProject(fileReader), new ProjectsInSolution(fileReader));
            var deadFiles = findNonSolutionFiles.Find(solutionPath, exludeFilesContaining).ToArray();
            foreach (var deadFile in deadFiles)
            {
                Console.WriteLine(deadFile);
            }

            Console.WriteLine();

            Console.WriteLine($"Found {deadFiles.Count()} dead files!");

            Console.WriteLine($"Input EXIT to exit or ENTER to retry!");

            if (Console.ReadLine() != "EXIT")
            {
                goto GOTO_RETRY;
            }
        }
    }
}