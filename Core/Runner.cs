using IntegrityInMicrosoftGraph.Interfaces;
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

        public Runner(IFileService files, IHashService hash, IGraphService graph)
        {
            _files = files;
            _hash = hash;
            _graph = graph;
        }

        public async Task Run(int sizeKb, string fileType)
        {
            string original = "original.bin";
            string downloaded = "downloaded.bin";

            _files.CreateFile(original, sizeKb, fileType);

            var hash1 = _hash.ComputeHash(original);

            //Calculate size
            double sizeMB = GetSizeMB(original);

            //UPLOAD
            var upload = await MeasureAsync(
                () => _graph.UploadAsync(original, "test/original.bin"),
                sizeMB
            );

            Console.WriteLine($"Upload time: {upload.ms} ms");
            Console.WriteLine($"Upload speed: {upload.mbps:F2} MB/s");


            //DOWNLOAD
            var download = await MeasureAsync(
                () => _graph.DownloadAsync("test/original.bin", downloaded),
                sizeMB
            );

            Console.WriteLine($"Download time: {download.ms} ms");
            Console.WriteLine($"Download speed: {download.mbps:F2} MB/s");

            //HASH CHECK
            var hash2 = _hash.ComputeHash(downloaded);

            Console.WriteLine(hash1 == hash2 ? "MATCH" : "MISMATCH");
            Console.WriteLine(CompareFiles(original, downloaded) ? "FILES MATCH" : "FILES MISMATCH");
        }
        private async Task<(long ms, double mbps)> MeasureAsync(Func<Task> action, double sizeMB)
        {
            var sw = Stopwatch.StartNew();
            await action();
            sw.Stop();

            double seconds = sw.Elapsed.TotalSeconds;
            double mbps = sizeMB / seconds;

            return (sw.ElapsedMilliseconds, mbps);
        }

        private double GetSizeMB(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            return fileInfo.Length / (1024.0 * 1024.0);
        }

        private bool CompareFiles(string file1, string file2)
        {
            using var fs1 = new FileStream(file1, FileMode.Open);
            using var fs2 = new FileStream(file2, FileMode.Open);
            if (fs1.Length != fs2.Length)
                return false;
            int byte1, byte2;
            do
            {
                byte1 = fs1.ReadByte();
                byte2 = fs2.ReadByte();
                if (byte1 != byte2)
                    return false;
            } while (byte1 != -1);
            return true;

        }
    }
}
