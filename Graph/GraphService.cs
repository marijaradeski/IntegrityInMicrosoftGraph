using IntegrityInMicrosoftGraph.Interfaces;
using Microsoft.Graph;

public class GraphService : IGraphService
{
    private readonly GraphServiceClient _client;

    public GraphService(GraphServiceClient client)
    {
        _client = client;
    }

    public async Task UploadAsync(string local, string remote)
    {
        using var stream = File.OpenRead(local);

        var drive = await _client.Me.Drive.GetAsync();

        await _client.Drives[drive.Id]
            .Items["root"]
            .ItemWithPath(remote)
            .Content
            .PutAsync(stream);
    }

    public async Task DownloadAsync(string remote, string local)
    {
        var drive = await _client.Me.Drive.GetAsync();

        var stream = await _client.Drives[drive.Id]
            .Items["root"]
            .ItemWithPath(remote)
            .Content
            .GetAsync();

        using var file = File.Create(local);
        await stream.CopyToAsync(file);
    }
}