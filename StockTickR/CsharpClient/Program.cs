using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5050/stocks")
    .ConfigureLogging(logging => { logging.AddConsole(); })
    .AddMessagePackProtocol()
    .Build();

await connection.StartAsync();

Console.WriteLine("Starting connection. Press Ctrl-C to close.");

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (sender, a) =>
{
    a.Cancel = true;
    cts.Cancel();
};

connection.Closed += e =>
{
    Console.WriteLine("Connection closed with error: {0}", e);

    cts.Cancel();
    return Task.CompletedTask;
};


connection.On("marketOpened", () => { Console.WriteLine("Market opened"); });

connection.On("marketClosed", () => { Console.WriteLine("Market closed"); });

connection.On("marketReset", () =>
{
    // We don't care if the market rest
});

var channel = await connection.StreamAsChannelAsync<Dictionary<string, object>>("StreamStocks", CancellationToken.None);
while (await channel.WaitToReadAsync() && !cts.IsCancellationRequested)
{
    while (channel.TryRead(out var stock))
    {
        Console.WriteLine(string.Join(", ", stock.Select(pair => $"{pair.Key}: {pair.Value}")));
    }
}

