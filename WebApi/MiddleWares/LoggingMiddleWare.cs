namespace WebApi.MiddleWares
{
    public class LoggingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleWare> _logger;

        public LoggingMiddleWare(RequestDelegate next,ILogger<LoggingMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var iPAddress = context.Connection?.RemoteIpAddress?.ToString();
            var url= context.Request.Path;
            var method=context.Request.Method;

            _logger.LogInformation($"[RequestIp] {iPAddress} [RequestUrl] {url} [Method] {method}");

            //if (context.Request.Method != HttpMethod.Get.Method)
            //{
            //    var remoteIp = context.Connection.RemoteIpAddress;
            //    _logger.LogDebug("Request from Remote IP address: {RemoteIp}", remoteIp);

            //    var bytes = remoteIp.GetAddressBytes();
            //    var badIp = true;
            //    foreach (var address in _safelist)
            //    {
            //        if (address.SequenceEqual(bytes))
            //        {
            //            badIp = false;
            //            break;
            //        }
            //    }

            //    if (badIp)
            //    {
            //        _logger.LogWarning(
            //            "Forbidden Request from Remote IP address: {RemoteIp}", remoteIp);
            //        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            //        return;
            //    }
            //}

            await _next.Invoke(context);
        }
    }
}
