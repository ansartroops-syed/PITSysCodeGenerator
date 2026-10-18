using PITSysCodeGenerator.Generators;
using PITSysCodeGenerator.Helpers;
using PITSysCodeGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PITSysCodeGenerator.Dialogs
{
    /// <summary>
    /// Code-behind for the Code Generator WPF dialog.
    /// </summary>
    public partial class CodeGeneratorDialog : Window
    {
        private readonly GeneratorViewModel _viewModel;
        private readonly Dictionary<string, ICodeGenerator> _generators;
        private string _currentPreviewTab = "Contract";

        public CodeGeneratorDialog()
        {
            InitializeComponent();

            _viewModel = new GeneratorViewModel();
            DataContext = _viewModel;

            _generators = new Dictionary<string, ICodeGenerator>
            {
                { "Contract", new ContractGenerator() },
                { "Service", new ServiceGenerator() },
                { "Controller", new ControllerGenerator() },
                { "DataProvider", new DataProviderGenerator() },
                { "UIBuilder", new UIBuilderGenerator() },
                { "MenuItem", new MenuItemGenerator() }
            };
        }

        #region Pattern Radio Buttons

        private void PatternStandard_Checked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
                _viewModel.OperationType = SysOperationType.Standard;
        }

        private void PatternRDP_Checked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
                _viewModel.OperationType = SysOperationType.ReportDataProvider;
        }

        private void PatternUIBuilder_Checked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
                _viewModel.OperationType = SysOperationType.CustomUIBuilder;
        }

        private void PatternFull_Checked(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
                _viewModel.OperationType = SysOperationType.Full;
        }

        #endregion

        #region Field Management

        private void AddField_Click(object sender, RoutedEventArgs e)
        {
            var fieldName = txtNewFieldName.Text?.Trim();
            var fieldType = txtNewFieldType.Text?.Trim();
            var fieldLabel = txtNewFieldLabel.Text?.Trim();
            var isMandatory = chkNewFieldMandatory.IsChecked == true;

            if (string.IsNullOrEmpty(fieldName) || string.IsNullOrEmpty(fieldType))
            {
                SetStatus("⚠️ Field Name and EDT/Type are required.", true);
                return;
            }

            // Auto-generate label if empty
            if (string.IsNullOrEmpty(fieldLabel))
            {
                fieldLabel = D365NamingHelper.ToLabel(fieldName);
            }

            var field = new ContractField
            {
                FieldName = D365NamingHelper.ToCamelCase(fieldName),
                EdtType = fieldType,
                Label = fieldLabel,
                IsMandatory = isMandatory,
                DisplayOrder = _viewModel.Fields.Count + 1
            };

            _viewModel.Fields.Add(field);

            // Clear input fields
            txtNewFieldName.Text = string.Empty;
            txtNewFieldType.Text = string.Empty;
            txtNewFieldLabel.Text = string.Empty;
            chkNewFieldMandatory.IsChecked = false;

            SetStatus($"✅ Field '{field.FieldName}' added.");
            txtNewFieldName.Focus();
        }

        private void RemoveField_Click(object sender, RoutedEventArgs e)
        {
            if (dgFields.SelectedItem is ContractField selectedField)
            {
                _viewModel.Fields.Remove(selectedField);
                // Re-order
                for (int i = 0; i < _viewModel.Fields.Count; i++)
                {
                    _viewModel.Fields[i].DisplayOrder = i + 1;
                }
                SetStatus($"🗑️ Field removed.");
            }
            else
            {
                SetStatus("⚠️ Select a field to remove.", true);
            }
        }

        private void ClearFields_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Fields.Clear();
            SetStatus("🧹 All fields cleared.");
        }

        private void AddSampleFields_Click(object sender, RoutedEventArgs e)
        {
            var sampleFields = new[]
            {
                new ContractField { FieldName = "custAccount", EdtType = "CustAccount", Label = "Customer account", IsMandatory = true, DisplayOrder = 1 },
                new ContractField { FieldName = "fromDate", EdtType = "TransDate", Label = "From date", IsMandatory = false, DisplayOrder = 2 },
                new ContractField { FieldName = "toDate", EdtType = "TransDate", Label = "To date", IsMandatory = false, DisplayOrder = 3 },
                new ContractField { FieldName = "includeOnHold", EdtType = "NoYesId", Label = "Include on hold", IsMandatory = false, DisplayOrder = 4, GroupName = "Options" }
            };

            foreach (var field in sampleFields)
            {
                _viewModel.Fields.Add(field);
            }

            SetStatus("📋 Sample fields added.");
        }

        #endregion

        #region Output

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog
            {
                Description = "Select output directory for generated X++ files",
                ShowNewFolderButton = true,
                SelectedPath = _viewModel.OutputDirectory
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _viewModel.OutputDirectory = dialog.SelectedPath;
            }
        }

        #endregion

        #region Preview

        private void Preview_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput()) return;

            grpPreview.Visibility = Visibility.Visible;
            UpdatePreview(_currentPreviewTab);
            SetStatus("👁️ Preview generated.");
        }

        private void PreviewTab_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string tabName)
            {
                _currentPreviewTab = tabName;
                UpdatePreview(tabName);
            }
        }

        private void UpdatePreview(string tabName)
        {
            if (!ValidateInput()) return;

            var options = _viewModel.ToGeneratorOptions();

            if (_generators.TryGetValue(tabName, out var generator))
            {
                try
                {
                    txtPreview.Text = generator.Generate(options);
                }
                catch (Exception ex)
                {
                    txtPreview.Text = $"// Error generating {tabName}:\n// {ex.Message}";
                }
            }
        }

        private void CopyPreview_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPreview.Text))
            {
                FileOutputHelper.CopyToClipboard(txtPreview.Text);
                SetStatus("📋 Code copied to clipboard!");
            }
        }

        #endregion

        #region Generate

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput()) return;

            var options = _viewModel.ToGeneratorOptions();
            var generatedFiles = new List<string>();

            try
            {
                if (options.GenerateContract)
                {
                    var gen = new ContractGenerator();
                    var path = FileOutputHelper.SaveToFile(options.OutputDirectory, gen.GetFileName(options), gen.Generate(options));
                    generatedFiles.Add(path);
                }

                if (options.GenerateService)
                {
                    var gen = new ServiceGenerator();
                    var path = FileOutputHelper.SaveToFile(options.OutputDirectory, gen.GetFileName(options), gen.Generate(options));
                    generatedFiles.Add(path);
                }

                if (options.GenerateController)
                {
                    var gen = new ControllerGenerator();
                    var path = FileOutputHelper.SaveToFile(options.OutputDirectory, gen.GetFileName(options), gen.Generate(options));
                    generatedFiles.Add(path);
                }

                if (options.GenerateDataProvider)
                {
                    var gen = new DataProviderGenerator();
                    var path = FileOutputHelper.SaveToFile(options.OutputDirectory, gen.GetFileName(options), gen.Generate(options));
                    generatedFiles.Add(path);
                }

                if (options.GenerateUIBuilder)
                {
                    var gen = new UIBuilderGenerator();
                    var path = FileOutputHelper.SaveToFile(options.OutputDirectory, gen.GetFileName(options), gen.Generate(options));
                    generatedFiles.Add(path);
                }

                if (options.GenerateMenuItem)
                {
                    var gen = new MenuItemGenerator();
                    var path = FileOutputHelper.SaveXmlToFile(options.OutputDirectory, gen.GetFileName(options), gen.Generate(options));
                    generatedFiles.Add(path);
                }

                var fileNames = string.Join("\n", generatedFiles.Select(f => $"  • {System.IO.Path.GetFileName(f)}"));
                var result = MessageBox.Show(
                    $"✅ Successfully generated {generatedFiles.Count} file(s):\n\n{fileNames}\n\nOutput directory:\n{options.OutputDirectory}\n\nOpen output folder?",
                    "PITSysCodeGenerator - Success",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                {
                    FileOutputHelper.OpenInExplorer(options.OutputDirectory);
                }

                SetStatus($"✅ Generated {generatedFiles.Count} file(s) successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Error during code generation:\n\n{ex.Message}",
                    "PITSysCodeGenerator - Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                SetStatus("❌ Generation failed. See error message.", true);
            }
        }

        #endregion

        #region Validation & Helpers

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(_viewModel.BaseClassName))
            {
                SetStatus("⚠️ Base Class Name is required.", true);
                txtBaseClassName.Focus();
                return false;
            }

            if (!D365NamingHelper.IsValidClassName(_viewModel.BaseClassName))
            {
                SetStatus("⚠️ Base Class Name must start with uppercase letter (PascalCase).", true);
                txtBaseClassName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(_viewModel.Prefix))
            {
                SetStatus("⚠️ Prefix is required.", true);
                txtPrefix.Focus();
                return false;
            }

            return true;
        }

        private void SetStatus(string message, bool isError = false)
        {
            txtStatus.Text = message;
            txtStatus.Foreground = isError
                ? FindResource("ErrorColor") as System.Windows.Media.SolidColorBrush
                : FindResource("SuccessColor") as System.Windows.Media.SolidColorBrush;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion
    }
}