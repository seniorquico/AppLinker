using System.Diagnostics;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AppLinker
{
    /// <summary>
    ///     Provides the application entry point. This starts a Kestrel web server and runs a web application.
    /// </summary>
    [DebuggerStepThrough]
    public sealed class Program
    {
        /// <summary>
        ///     Creates a web host builder pre-configured to start a Kestrel web server and run a web application with
        ///     the AppLinker services.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns>The web host builder.</returns>
        /// <remarks>
        ///     This method signature must not be changed. The integration testing infrastructure provided by the
        ///     <c>Microsoft.AspNetCore.Mvc.Testing</c> package builds and runs the web host using this exact method
        ///     signature.
        /// </remarks>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        /// <summary>Starts the application.</summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args) =>
            CreateWebHostBuilder(args).Build().Run();
    }
}
