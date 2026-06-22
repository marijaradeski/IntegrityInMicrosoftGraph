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
        private readonly IFileSourceService _source;
        private readonly IHashService _hash;
        private readonly IGraphService _graph;
        private readonly ICalculator _calculator;
        private readonly IFileComparer _comparer;

        public Runner(
            IFileSourceService source,
            IHashService hash,
            IGraphService graph,
            ICalculator calculator,
            IFileComparer comparer)
        {
            _source = source;
            _hash = hash;
            _graph = graph;
            _calculator = calculator;
            _comparer = comparer;
        }

        public async Task<FileTransferResult> RunAsync()
        {
            string localPath = _source.FilePath();
            string fileName = Path.GetFileName(localPath);
            string remotePath = $"test/{fileName}";
            string downloaded = "downloaded.bin";

            long sizeBytes = _source.GetSizeBytes(localPath);

            var hash1 = _hash.ComputeHash(localPath);

            var upload = await MeasureUploadDownloadAsync(() =>
                _graph.UploadAsync(localPath, remotePath));

            var download = await MeasureUploadDownloadAsync(() =>
                _graph.DownloadAsync(remotePath, downloaded));

            var hash2 = _hash.ComputeHash(downloaded);

            return new FileTransferResult
            {
                FileSizeBytes = sizeBytes,
                SizeKb = (int)(sizeBytes / 1024.0),
                FileType = _source.GetFileType(localPath),

                UploadTime = upload,
                DownloadTime = download,

                UploadBytesPerSecond = _calculator.CalculateBytesPerSecond(sizeBytes, upload),
                DownloadBytesPerSecond = _calculator.CalculateBytesPerSecond(sizeBytes, download),

                HashMatch = hash1 == hash2,
                FilesMatch = _comparer.Compare(localPath, downloaded),

                RemotePath = remotePath,
                TimestampUtc = DateTime.UtcNow
            };
        }

        private async Task<long> MeasureUploadDownloadAsync(Func<Task> action)
        {
            var sw = Stopwatch.StartNew();
            await action();
            sw.Stop();
            return (sw.ElapsedMilliseconds);
        }
    }
}
