using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace PITSysCodeGenerator
{
    /// <summary>
    /// PITSysCodeGenerator Visual Studio Package.
    /// Registers the package with Visual Studio shell and initializes commands.
    /// </summary>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids80.NoSolution, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids80.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class PITSysCodeGeneratorPackage : AsyncPackage
    {
        /// <summary>
        /// Package GUID string.
        /// </summary>
        public const string PackageGuidString = "C3D4E5F6-A7B8-9012-CDEF-123456789ABC";

        /// <summary>
        /// Package constructor.
        /// </summary>
        public PITSysCodeGeneratorPackage()
        {
        }

        /// <summary>
        /// Async initialization of the package. Called when VS needs the package.
        /// </summary>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            // Switch to the main/UI thread to initialize UI-dependent services
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            // Initialize the command
            await Commands.GenerateCodeCommand.InitializeAsync(this);
        }
    }
}