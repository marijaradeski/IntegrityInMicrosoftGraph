using IntegrityInMicrosoftGraph.Authentication;
using IntegrityInMicrosoftGraph.ConsoleLogic;
using IntegrityInMicrosoftGraph.Core;
using IntegrityInMicrosoftGraph.Interfaces;
using IntegrityInMicrosoftGraph.Services;
using Microsoft.Graph;

class Program
{
    static async Task Main(string[] args)
    {
        // AUTH
        var authProvider = new GraphAuthenticator();

        // GRAPH CLIENT
        var client = new GraphServiceClient(authProvider);

        IFileService fileService = new FileService();
        IHashService hashService = new HashService();
        IGraphService graphService = new GraphService(client);
        ICalculator calculator = new Calculator();
        IFileComparer comparer = new FileComparer();

        var menu = new ConsoleBar(
            fileService,
            hashService,
            graphService,
            calculator,
            comparer);

        await menu.StartAsync();
    }
}