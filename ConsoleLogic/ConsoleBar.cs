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

            //var sizeKb = ReadFileSize();
            var path = ReadFileType();

            var result = await _runner.Run(path);
            PrintResults(result);
        }

        private string ReadFileType()
        {
            while (true)
             {
                Console.Write("Enter full file path: ");
                var path = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
                    return path;

                Console.WriteLine("File not found. Try again.");
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
