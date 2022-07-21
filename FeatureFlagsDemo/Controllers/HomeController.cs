using FeatureFlagsDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;
using System.Diagnostics;

namespace FeatureFlagsDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFeatureManager _featureManager;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IFeatureManager featureManager, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _featureManager = featureManager ?? throw new ArgumentNullException(nameof(featureManager));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<IActionResult> Index()
        {
            var homeIndexModel = new HomeIndexModel
            {
                FeatureA = false
            };

            // how to list all feature flags in required
            var listOfItems = new List<string>();
            await foreach (var item in _featureManager.GetFeatureNamesAsync())
            {
                listOfItems.Add(item);
            }

            homeIndexModel.FeatureA = await _featureManager.IsEnabledAsync(MyFeatureFlags.FeatureA);
            homeIndexModel.NoTracking = await _featureManager.IsEnabledAsync(MyFeatureFlags.NoTracking);
            
            // How to get configuration from app configration
            homeIndexModel.BackgroundValue = _configuration.GetValue<string>("TestApp:Settings:BackgroundColor");

            return View(homeIndexModel);
        }

        [FeatureGate(MyFeatureFlags.FeatureB)]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}