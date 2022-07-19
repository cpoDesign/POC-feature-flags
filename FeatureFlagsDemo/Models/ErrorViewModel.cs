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
    }

    public static class MyFeatureFlags
    {
        public const string FeatureA = "FeatureA";
        public const string FeatureB = "FeatureB";
        public const string FeatureC = "FeatureC";
    }

    public class ThirdPartyActionFilter
    {

    }
}