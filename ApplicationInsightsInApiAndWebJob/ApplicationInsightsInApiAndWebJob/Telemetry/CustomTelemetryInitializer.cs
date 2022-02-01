using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace ApplicationInsightsInApiAndWebJob.Telemetry
{
    public class CustomTelemetryInitializer : ITelemetryInitializer
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly IAuthHelper _authHelper;

        //public CustomTelemetryInitializer(IHttpContextAccessor httpContextAccessor, IAuthHelper authHelper)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //    _authHelper = authHelper;
        //}

        public void Initialize(ITelemetry telemetry)
        {
            //var httpContext = _httpContextAccessor?.HttpContext;

            //SetUserProperties(telemetry, httpContext);

            //if (telemetry is RequestTelemetry requestTelemetry)
            //{
            //    OverrideHttpStatusCodes(requestTelemetry);
            //}
        }

        //private void SetUserProperties(ITelemetry telemetry, HttpContext httpContext)
        //{
        //    if (httpContext == null)
        //    {
        //        return;
        //    }

        //    var subjectId = _authHelper.GetUserId(httpContext.User);
        //    var expiration = _authHelper.GetExpired(httpContext.User);

        //    if (!string.IsNullOrWhiteSpace(subjectId))
        //    {
        //        telemetry.Context.User.Id = subjectId;
        //        telemetry.Context.Session.Id = StringHelper.GetSHA256Hash(subjectId + expiration.UtcTicks);
        //    }
        //}

        //private void OverrideHttpStatusCodes(RequestTelemetry requestTelemetry)
        //{
        //    // Clients refresh their tokens when they receive 401. To see a clearer picture on Azure Portal, do not track these as errors.
        //    var parsed = Int32.TryParse(requestTelemetry.ResponseCode, out var code);
        //    if (parsed && code == 401)
        //    {
        //        // If we set the Success property, the SDK won't change it:
        //        requestTelemetry.Success = true;
        //        // Allow us to filter these requests in the portal:
        //        requestTelemetry.Properties["Overridden401s"] = "true";
        //    }
        //}
    }
}
