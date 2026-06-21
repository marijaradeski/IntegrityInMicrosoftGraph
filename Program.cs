using IntegrityInMicrosoftGraph.Authentication;
using IntegrityInMicrosoftGraph.Core;
using IntegrityInMicrosoftGraph.Enums;
using IntegrityInMicrosoftGraph.Interfaces;
using IntegrityInMicrosoftGraph.Security;
using IntegrityInMicrosoftGraph.Services;
using Microsoft.Graph;
using Microsoft.VisualBasic.FileIO;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Integrity In Microsoft Graph App");

        // AUTH
        var authProvider = new GraphAuthenticator();

        // GRAPH CLIENT
        var client = new GraphServiceClient(authProvider);

        // SERVICES
        IFileService fileService = new FileService();
        IHashService hashService = new HashService();
        IGraphService graphService = new GraphService(client);

        // RUN EXPERIMENT
        var runner = new Runner(fileService, hashService, graphService);

        await runner.Run(10, FileType.Txt);
        await runner.Run(1024, FileType.Txt);
        await runner.Run(100, FileType.Png);
        await runner.Run(1024, FileType.Jpg);
        await runner.Run(1024, FileType.Zip);
    }
}