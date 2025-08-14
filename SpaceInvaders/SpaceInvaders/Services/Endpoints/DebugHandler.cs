namespace SpaceInvaders.Services.Endpoints;

/// <summary>
/// Intercepts HTTP requests and logs details, especially for unsuccessful calls, in debug mode.
/// </summary>
internal class DebugHttpHandler : DelegatingHandler
{
  private readonly ILogger _logger;

  /// <summary>
  /// Initializes a new instance of the <see cref="DebugHttpHandler"/> class.
  /// </summary>
  /// <param name="logger">The logger instance.</param>
  /// <param name="innerHandler">The inner HTTP message handler.</param>
  public DebugHttpHandler(ILogger<DebugHttpHandler> logger, HttpMessageHandler? innerHandler = null)
    : base(innerHandler ?? new HttpClientHandler())
  {
    _logger = logger;
  }

  /// <summary>
  /// Sends an HTTP request and logs details of unsuccessful responses in debug mode.
  /// </summary>
  /// <param name="request">The HTTP request message.</param>
  /// <param name="cancellationToken">A token to cancel the operation.</param>
  /// <returns>The HTTP response message.</returns>
  protected async override Task<HttpResponseMessage> SendAsync(
    HttpRequestMessage request,
    CancellationToken cancellationToken)
  {
    var response = await base.SendAsync(request, cancellationToken);
#if DEBUG
    if (!response.IsSuccessStatusCode)
    {
      _logger.LogDebugMessage("Unsuccessful API Call");
      if (request.RequestUri is not null)
      {
        _logger.LogDebugMessage($"{request.RequestUri} ({request.Method})");
      }

      foreach ((var key, var values) in request.Headers.ToDictionary(x => x.Key, x => string.Join(", ", x.Value)))
      {
        _logger.LogDebugMessage($"{key}: {values}");
      }

      var content = request.Content is not null ? await request.Content.ReadAsStringAsync() : null;
      if (!string.IsNullOrEmpty(content))
      {
        _logger.LogDebugMessage(content);
      }

      // Uncomment to automatically break when an API call fails while debugging
      // System.Diagnostics.Debugger.Break();
    }
#endif
    return response;
  }
}