<p align="center">
  <a href="https://seniorquico.github.io/AppLinker"><img src="/docs/link.png" width="160" height="160" alt="AppLinker's logo, two linked chains" /></a>
</p>
<h3 align="center"><a href="https://seniorquico.github.io/AppLinker">AppLinker</a></h3>
<p align="center">A reusable ASP.NET Core library and a standalone application to power your Android App Links and iOS Universal Links</p>

---

AppLinker may be used to power your Android App Links and iOS Universal Links.

TODO: Who is this for?
TODO: What are App Links & Universal Links?
TODO: Why do I want App Links & Universal Links?
TODO: Quick list of features.

---

Add AppLinker to an existing website deployed as an ASP.NET Core application. With this deployment model, AppLinker will simply serve the platform-specific configuration files (to verify the mobile app and website association) and a measurement API. Optionally, the AppLinker UI (banner, splash, etc.) may be embedded on the pages of your website to prompt users to install the mobile app.

We recommend this deployment model if some of the pages on your website directly translate to screens in your mobile app. Using this deployment model, users that open a link to your website will directly launch the mobile app if it is already installed. If the mobile app is not already installed, users will launch their mobile web browser and visit the page on your website.

---

Use AppLinker as a standalone ASP.NET Core application. With this deployment model, AppLinker will serve the platform-specific configuration files, a measurement API, and a custom landing page with links to download the mobile app separate from other websites you operate. Optionally, add the AppLinker UI (banner, splash, etc.)

---

## Programmatic Configuration

```csharp
public void ConfigureServices(IServiceCollection services)
{
    ...
    services.AddMvc()
        ...
        .AddAppLinker(options =>
        {
            options.Android = new GooglePlayApp
            {
                CertificateFingerprint = "",
                PackageName = "com.cheesypixel.slippy",
            };
            options.iOS = new AppleAppStoreApp
            {
                BundleId = "com.cheesypixel.slippy",
                TeamId = "ABCDEFGHIJ",
            };
        })
        ...;
}
```

## `IConfiguration` Configuration

```csharp
public void ConfigureServices(IConfiguration configuration, IServiceCollection services)
{
    ...
    services.AddMvc()
        ...
        .AddAppLinker();
    services.Configure<AppLinkerOptions>(configuration.GetSection("AppLinker"));
}
```

```json
{
  "AppLinker": {
    "Android": {
      "CertificateFingerprint": "",
      "PackageName": "com.cheesypixel.slippy"
    },
    "iOS": {
      "BundleId": "com.cheesypixel.slippy",
      "TeamId": "ABCDEFGHIJ"
    }
  }
}
```

## Credits

The link icon was created by [Milinda Courey](https://thenounproject.com/milindacourey10/) from the Noun Project.
