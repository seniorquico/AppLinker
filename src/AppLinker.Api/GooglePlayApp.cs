using System.Diagnostics;

namespace AppLinker.Api
{
    /// <summary>Identifies options for a Google Play app.</summary>
    [DebuggerStepThrough]
    public sealed class GooglePlayApp
    {
        /// <summary>Gets or sets the certificate fingerprint.</summary>
        public string CertificateFingerprint { get; set; }

        /// <summary>Gets or sets the package name.</summary>
        public string PackageName { get; set; }
    }
}
