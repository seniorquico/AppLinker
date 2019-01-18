using System.Diagnostics;

namespace AppLinker.Api
{
    /// <summary>Identifies options for an Apple App Store app.</summary>
    [DebuggerStepThrough]
    public sealed class AppleAppStoreApp
    {
        /// <summary>Gets or sets the bundle identifier.</summary>
        public string BundleId { get; set; }

        /// <summary>Gets or sets the team identifier.</summary>
        public string TeamId { get; set; }
    }
}
