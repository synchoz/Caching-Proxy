using System.Net;

namespace CachingProxyServer;

public class ProxyServer
{
    private readonly HttpListener _listener;
    private readonly HttpClient _client;
    private readonly string _forwardURL;
    private readonly CancellationTokenSource  _cancellationToken;
    private readonly string baseURL = "http://localhost";
    private readonly Dictionary<string,string> _cachedResponses;
    public ProxyServer(int port, string originURL)
    {
        _cachedResponses = new Dictionary<string, string>();
        _listener = new HttpListener();
        _client = new HttpClient();
        _listener.Prefixes.Add($"{baseURL}:{port}/");
        _forwardURL = originURL;
        _cancellationToken = new CancellationTokenSource();
    }
    private async Task InitRequestAsync()
    {
        _listener.Start();

        while (!_cancellationToken.Token.IsCancellationRequested)
        {
            try
            {
                var context = await _listener.GetContextAsync();
                _ = Task.Run(() => HandleRequestAsync(context));
            }
            catch (HttpListenerException) when (_cancellationToken.Token.IsCancellationRequested)
            {
                Console.WriteLine("The HTTP server is now Stopping...");
                _cancellationToken.Dispose();
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
    private async Task HandleRequestAsync(HttpListenerContext context)
    {
        string cached = "MISS";
        string responseBody;
        try
        {
            string fullUrl = $"{_forwardURL}{context.Request.RawUrl}";
            Console.WriteLine(fullUrl);
            if(_cachedResponses.ContainsKey(fullUrl))
            {
                cached = "HIT";
                responseBody = _cachedResponses[fullUrl];
            }
            else 
            {
                responseBody = await _client.GetStringAsync(fullUrl);
                _cachedResponses.Add(fullUrl, responseBody);
            }
            byte[] responseBytes = System.Text.Encoding.UTF8.GetBytes(responseBody);
            context.Response.Headers.Add($"X-Cache: {cached}");
            Stream output = context.Response.OutputStream;
            await output.WriteAsync(responseBytes, 0, responseBytes.Length);
            output.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public async Task Start()
    {
        await InitRequestAsync();
    }
    public void Stop()
    {
        _cancellationToken.Cancel();
        _listener.Stop();
    }

    public void ClearCache()
    {
        _cachedResponses.Clear();
        Console.WriteLine("Cache has been cleared.");
    }

}

