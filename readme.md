# Implementation of features

## Packages used

```xml
  <PackageReference Include="Microsoft.FeatureManagement" Version="2.5.1" />
  <PackageReference Include="Microsoft.FeatureManagement.AspNetCore" Version="2.5.1" />
```
## Installation

###  program.cs

```c#
using Microsoft.FeatureManagement;
...
// Register feature management from a specific configuration.
builder.Services.AddFeatureManagement(builder.Configuration.GetSection("FeatureManagement"));
```


### appsettings.json

```c#
 "FeatureManagement": {
    "FeatureA": true, // Feature flag set to on
    "FeatureB": false, // Feature flag set to off
    "FeatureC": {
      "EnabledFor": [
        {
          "Name": "Percentage",
          "Parameters": {
            "Value": 50
          }
        }
      ]
    }
    
    //Samples for fronend backend feature splits
    ,
    "fe": {
      "LoginPage": {
        "Label": true // Remove from Live after 2 weeks #JiraTicket-123
      }
    },
    "be": {

    }
  }
```

### Usage in controller
this sample shows the different possible implementations

```c#
public class HomeController : Controller
{
    private readonly IFeatureManager _featureManager;

    public HomeController(IFeatureManager featureManager)
    {
        _featureManager = featureManager ?? throw new ArgumentNullException(nameof(featureManager));
    }

    public async Task<IActionResult> Index()
    {
        var homeIndexModel = new HomeIndexModel
        {
            FeatureA = false,
            UserName = string.Empty
            
        };

        if (await _featureManager.IsEnabledAsync(MyFeatureFlags.FeatureA))
        {
            // some custom logic
            homeIndexModel.UserName = this.User.Identity.Name ?? string.Empty;
        }

        homeIndexModel.FeatureA = await _featureManager.IsEnabledAsync(MyFeatureFlags.FeatureA);

        return View(homeIndexModel);
    }

    /// <summary>
    /// Accessible only when featureB is enabled
    /// </summary>        
    [FeatureGate(MyFeatureFlags.FeatureB)]
    public IActionResult Privacy()
    {
        return View();
    }
}
  
```


### Usage in views

requires using in _ViewImports.cshtml or in view.cshtml file

```
@addTagHelper *, Microsoft.FeatureManagement.AspNetCore

```

```xml
<feature name="FeatureA">
    <p>This can only be seen if 'FeatureA' is enabled.</p>
</feature>

<feature name="FeatureA" negate="true">
    <p>This will be shown if 'FeatureA' is disabled.</p>
</feature>

<feature name="FeatureA, FeatureB" requirement="All">
    <p>This can only be seen if 'FeatureA' and 'FeatureB' are enabled.</p>
</feature>

<feature name="FeatureA, FeatureB" requirement="Any">
    <p>This can be seen if 'FeatureA', 'FeatureB', or both are enabled.</p>
</feature>
```