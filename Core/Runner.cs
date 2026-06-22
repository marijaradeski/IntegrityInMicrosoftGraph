using IntegrityInMicrosoftGraph.Enums;
using IntegrityInMicrosoftGraph.Interfaces;
using IntegrityInMicrosoftGraph.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace IntegrityInMicrosoftGraph.Core
{
    public class Runner
    {
        private readonly IFileService _files;
        private readonly IHashService _hash;
        private readonly IGraphService _graph;
        private readonly ICalculator _calculator;
        private readonly IFileComparer _comparer;

        public Runner(IFileService files, IHashService hash, IGraphService graph, ICalculator calculator, IFileComparer comparer)
        {
            _files = files;
            _hash = hash;
            _graph = graph;
            _calculator = calculator;
            _comparer = comparer;
        }

        public async Task<FileTransferResult> Run(string filePath, int size)
        {
            string original = "original.bin";
            string downloaded = "downloaded.bin";
            string fileName = Path.GetFileName(filePath);
            string remotePath = $"test/{fileName}";

            //// 1. CREATE FILE
            _files.CreateFile(original, sizeKb, fileType);

            long fileSizeBytes = new FileInfo(filePath).Length;

            // 2. HASH BEFORE UPLOAD
            var hash1 = _hash.ComputeHash(filePath);

            // 3. UPLOAD
            var upload = await MeasureAsync(() =>
                _graph.UploadAsync(filePath, remotePath));

            // 4. DOWNLOAD
            var download = await MeasureAsync(() =>
                _graph.DownloadAsync(remotePath, downloaded));

            // 5. HASH AFTER DOWNLOAD
            var hash2 = _hash.ComputeHash(downloaded);

            // 6. RESULT OBJECT
            var result = new FileTransferResult
            {
                FileSizeBytes = fileSizeBytes,
                SizeKb = (int)(fileSizeBytes / 1024.0),
                FileType = Path.GetExtension(filePath),

                UploadTime = upload.ms,
                DownloadTime = download.ms,

                UploadBytesPerSecond = _calculator.CalculateBytesPerSecond(fileSizeBytes, upload.ms),
                DownloadBytesPerSecond = _calculator.CalculateBytesPerSecond(fileSizeBytes, download.ms),

                HashMatch = hash1 == hash2,
                FilesMatch = _comparer.Compare(filePath, downloaded),

                RemotePath = remotePath,
                TimestampUtc = DateTime.UtcNow
            };

            return result;
        }

        #region private methods
        private async Task<(long ms, double bytesPerSec)> MeasureAsync(Func<Task> action)
        {
            try
            {
                var sw = Stopwatch.StartNew();
                await action();
                sw.Stop();

                return (sw.ElapsedMilliseconds, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR during transfer:");
                Console.WriteLine(ex.Message);
                throw; // IMPORTANT so you see real failure
            }
        }
        #endregion
    }
}
