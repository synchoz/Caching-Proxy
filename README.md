
# Caching Proxy Server

A simple command-line interface (CLI) tool and HTTP proxy server built in C#. This project provides functionality to forward HTTP requests to an origin server, cache responses for subsequent requests, and handle requests efficiently. It was inspired by the [roadmap.sh caching server project](https://roadmap.sh/projects/caching-server).

## Features
- CLI interface for starting and managing the proxy server.
- Forwarding requests to an origin server and caching responses.
- Support for custom ports and origin URLs.
- Cache control with `X-Cache` headers:
  - `X-Cache: MISS` for non-cached responses.
  - `X-Cache: HIT` for cached responses.
- Graceful server shutdown.

## How to Use
### Steps to Run
1. Clone the repository:
   ```bash
   git clone https://github.com/synchoz/Caching-Proxy.git
   cd caching-proxy
   ```

2. Build the project:
   ```bash
   dotnet build
   ```

3. Start the server:
   ```bash
   dotnet run --project CachingProxyCLI -- --start --port <PORT> --origin <ORIGIN_URL>
   ```
   Example:
   ```bash
   dotnet run --project CachingProxyCLI -- --start --port 3000 --origin https://dummyjson.com
   ```

4. Stop the server:
   - Enter `--exit` in the CLI when the server is running to shut it down gracefully.

5. Clear the cache:
   - Enter `--clear-cache` in the CLI when the server is running to clear the cache.

## Project Structure
```
CachingProxyServer/        # Contains the core HTTP server logic
CachingProxyCLI/           # CLI interface for managing the proxy server
    Helpers/               # Utility functions for argument validation and CLI support
```

### Key Files:
- `CachingProxyServer/ProxyServer.cs`: Handles HTTP requests, caching, and forwarding.
- `CachingProxyCLI/Program.cs`: CLI entry point and command handling logic.
- `CachingProxyCLI/Helpers/`: Contains helper methods for validation.


## What I Learned
Through this project, I gained:
1. A deeper understanding of how proxy servers work, including request forwarding and response caching.
2. Practical experience with C# features like:
   - Asynchronous programming (`async`/`await`) for non-blocking operations.
   - Regex and validation for user input.
3. Insights into designing and structuring a multi-project solution in C# using best practices.
4. Hands-on experience managing graceful shutdowns and debugging issues in a multi-threaded application.


## Getting Started with Development
1. Clone the repository and open it in Visual Studio Code or your favorite IDE.
2. Follow the project structure to add or extend features.
3. Contribute by submitting pull requests for any enhancements or bug fixes!

---

Feel free to contribute or suggest improvements!
