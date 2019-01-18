using System.Diagnostics;

namespace AppLinker.Api
{
    /// <summary>Identifies options for the App Linker service.</summary>
    [DebuggerStepThrough]
    public sealed class AppLinkerOptions
    {
        /// <summary>Gets or sets the options for Android App Links.</summary>
        public GooglePlayApp Android { get; set; }

        /// <summary>Gets or sets the options for iOS Universal Links.</summary>
        public AppleAppStoreApp iOS { get; set; }
    }
}
