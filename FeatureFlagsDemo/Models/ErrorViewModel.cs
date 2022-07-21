namespace FeatureFlagsDemo.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class HomeIndexModel
    {
        public bool FeatureA { get; internal set; }
        public string BackgroundValue { get; internal set; }
        public bool NoTracking { get; internal set; }
    }

    public static class MyFeatureFlags
    {
        public const string FeatureA = "MyFeatureFlags.FeatureA";
        public const string FeatureB = "FeatureB";
        public const string FeatureC = "FeatureC";
        public const string NoTracking = "online.homepage.notracking";
    }

    public class ThirdPartyActionFilter
    {

    }
}