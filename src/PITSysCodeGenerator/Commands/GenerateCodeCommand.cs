using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using Task = System.Threading.Tasks.Task;

namespace PITSysCodeGenerator.Commands
{
    /// <summary>
    /// Command handler for the PIT SysOperation Code Generator.
    /// Registered under Tools menu in Visual Studio.
    /// </summary>
    internal sealed class GenerateCodeCommand
    {
        /// <summary>
        /// VS Package that provides this command.
        /// </summary>
        private readonly AsyncPackage _package;

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider => _package;

        /// <summary>
        /// Private constructor. Use <see cref="InitializeAsync"/> to create.
        /// </summary>
        private GenerateCodeCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            _package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandId = new CommandID(
                new Guid(CommandIds.CommandSetGuidString),
                CommandIds.GenerateCodeCommandId);

            var menuItem = new MenuCommand(Execute, menuCommandId);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static GenerateCodeCommand Instance { get; private set; }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Ensure we're on the UI thread
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new GenerateCodeCommand(package, commandService);
        }

        /// <summary>
        /// Callback when the command is invoked from the Tools menu.
        /// Opens the WPF Code Generator dialog.
        /// </summary>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            try
            {
                // Create and show the WPF dialog
                var dialog = new Dialogs.CodeGeneratorDialog();
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                VsShellUtilities.ShowMessageBox(
                    _package,
                    $"Error launching PIT SysOperation Code Generator:\n\n{ex.Message}",
                    "PITSysCodeGenerator - Error",
                    OLEMSGICON.OLEMSGICON_CRITICAL,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }
        }
    }
}