using A_Application;
using ProtoBuf;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:6969");

var app = builder.Build();
app.UseWebSockets();
app.Map("/ws", async context => 
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var currencyPairGenerator = DataGenerator.CreateCurrencyPairGenerator();
        using var ws = await context.WebSockets.AcceptWebSocketAsync();
        while (true)
        {
            if (ws.State == WebSocketState.Open)
            {
                using var memoryStream = new System.IO.MemoryStream();
                Serializer.Serialize(memoryStream, currencyPairGenerator.Generate());
                var outputBuffer = new ArraySegment<byte>(memoryStream.ToArray(), 0, (int)memoryStream.Length);
                await ws.SendAsync(outputBuffer, WebSocketMessageType.Binary, true, CancellationToken.None);
            }
            else if (ws.State == WebSocketState.Closed || ws.State == WebSocketState.Aborted)
            {
                break;
            }
            Thread.Sleep(200);
        }
    }
});

await app.RunAsync();
