using IntegrityInMicrosoftGraph.Core;
using IntegrityInMicrosoftGraph.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrityInMicrosoftGraph.ConsoleLogic
{
    public class ConsoleBar
    {
        private readonly Runner _runner;

        public ConsoleBar(Runner runner)
        {
            _runner = runner;
        }

        public async Task StartAsync()
        {
            Console.WriteLine("Integrity Microsoft Graph Application");

            int sizeKb = ReadFileSize();
            FileType fileType = ReadFileType();

            await _runner.Run(sizeKb, fileType);
        }

        private int ReadFileSize()
        {
            while (true)
            {
                Console.Write("Enter file size (KB): ");

                if (int.TryParse(Console.ReadLine(), out int sizeKb) && sizeKb > 0)
                {
                    return sizeKb;
                }

                Console.WriteLine("Please enter a valid positive number.");
            }
        }

        private FileType ReadFileType()
        {
            while (true)
            {
                Console.WriteLine("File types:");

                foreach (var type in Enum.GetValues<FileType>())
                {
                    Console.WriteLine($"- {type}");
                }

                Console.Write("Select file type: ");

                if (Enum.TryParse<FileType>(Console.ReadLine(),true, out var fileType))
                {
                    return fileType;
                }

                Console.WriteLine("Invalid file type.");
            }
        }
    }
}
