using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

await ProcessRepositoriesAsync(client);

static async Task ProcessRepositoriesAsync(HttpClient client)
{
    //var json = await client.GetStringAsync(
    //    "https://api.github.com/orgs/dotnet/repos");
    await using Stream stream =
    await client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
    var repositories =
        await JsonSerializer.DeserializeAsync<List<Repository>>(stream);

    //foreach (var repo in repositories ?? Enumerable.Empty<Repository>())
    //    Console.WriteLine(repo.name);

    foreach (var repo in repositories)
    {
        Console.WriteLine($"Name: {repo.Name}");
        Console.WriteLine($"Homepage: {repo.Homepage}");
        Console.WriteLine($"GitHub: {repo.GitHubHomeUrl}");
        Console.WriteLine($"Description: {repo.Description}");
        Console.WriteLine($"Watchers: {repo.Watchers:#,0}");
        Console.WriteLine();
    }
}