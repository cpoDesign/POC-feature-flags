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

        public HomeController(ILogger<HomeController> logger, IFeatureManager featureManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _featureManager = featureManager ?? throw new ArgumentNullException(nameof(featureManager));
        }

        public async Task<IActionResult> Index()
        {
            var homeIndexModel = new HomeIndexModel
            {
                FeatureA = false
            };

            if (await _featureManager.IsEnabledAsync(MyFeatureFlags.FeatureA))
            {
                homeIndexModel.FeatureA = true;
            }

            homeIndexModel.FeatureA = await _featureManager.IsEnabledAsync(MyFeatureFlags.FeatureA);

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