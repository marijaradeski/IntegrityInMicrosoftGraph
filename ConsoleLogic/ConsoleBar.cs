using IntegrityInMicrosoftGraph.Core;
using IntegrityInMicrosoftGraph.Enums;
using IntegrityInMicrosoftGraph.Interfaces;
using IntegrityInMicrosoftGraph.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrityInMicrosoftGraph.ConsoleLogic
{
    public class ConsoleBar
    {
        private readonly Runner _runner;
        private readonly ICalculator _calculator;

        public ConsoleBar(Runner runner, ICalculator calculator)
        {
            _runner = runner;
            _calculator = calculator;
        }

        public async Task StartAsync()
        {
            Console.WriteLine("Integrity Microsoft Graph Application");

            int sizeKb = ReadFileSize();
            FileType fileType = ReadFileType();

            var result = await _runner.Run(sizeKb, fileType);
            PrintResults(result);
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

        private void PrintResults(FileTransferResult result)
        {
            Console.WriteLine("\n================ RESULTS ================\n");

            Console.WriteLine($"File type: {result.FileType}");
            Console.WriteLine($"Size: {result.SizeKb} KB");
            Console.WriteLine($"Remote path: {result.RemotePath}\n");

            Console.WriteLine($"Upload time: {result.UploadTime} ms");
            Console.WriteLine($"Download time: {result.DownloadTime} ms\n");

            double uploadMb = _calculator.CalculateMbPerSecond(result.FileSizeBytes, result.UploadTime);
            double downloadMb = _calculator.CalculateMbPerSecond(result.FileSizeBytes, result.DownloadTime);

            Console.WriteLine($"Upload speed: {uploadMb:F2} MB/s");
            Console.WriteLine($"Download speed: {downloadMb:F2} MB/s\n");

            Console.WriteLine(result.HashMatch
                ? "Hash check: MATCH"
                : "Hash check: MISMATCH");

            Console.WriteLine(result.FilesMatch
                ? "File compare: MATCH"
                : "File compare: MISMATCH");

            Console.WriteLine("\n=========================================\n");
        }
    }
}
