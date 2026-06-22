using IntegrityInMicrosoftGraph.Core;
using IntegrityInMicrosoftGraph.Enums;
using IntegrityInMicrosoftGraph.Interfaces;
using IntegrityInMicrosoftGraph.Model;
using IntegrityInMicrosoftGraph.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrityInMicrosoftGraph.ConsoleLogic
{
    public class ConsoleBar
    {
        private readonly IFileService _fileService;
        private readonly IHashService _hash;
        private readonly IGraphService _graph;
        private readonly ICalculator _calculator;
        private readonly IFileComparer _comparer;

        public ConsoleBar(
            IFileService fileService,
            IHashService hash,
            IGraphService graph,
            ICalculator calculator,
            IFileComparer comparer)
        {
            _fileService = fileService;
            _hash = hash;
            _graph = graph;
            _calculator = calculator;
            _comparer = comparer;
        }

        public async Task StartAsync()
        {
            Console.WriteLine("Integrity Microsoft Graph Application");

            Console.WriteLine("Choose mode:");
            Console.WriteLine("1 - Use existing file on your PC");
            Console.WriteLine("2 - Create test data by inserting file type and size");

            var mode = Console.ReadLine();

            IFileSourceService source;

            if (mode == "1")
            {
                var path = ReadFilePath();
                source = new FileProviderService(path);
            }
            else
            {
                var sizeKb = ReadFileSize();
                var fileType = ReadFileType();
                source = new FileGeneratorService(sizeKb, fileType, _fileService);
            }

            var runner = new Runner(source, _hash, _graph, _calculator, _comparer);

            var result = await runner.RunAsync();
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

        private string ReadFilePath()
        {
            while (true)
            {
                Console.Write("Enter file path: ");
                var path = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
                    return path;

                Console.WriteLine("Invalid file path.");

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

                if (Enum.TryParse<FileType>(Console.ReadLine(), true, out var fileType))
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
