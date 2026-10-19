using PITSysCodeGenerator.Generators;
using PITSysCodeGenerator.Helpers;
using PITSysCodeGenerator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace PITSysCodeGenerator.Dialogs
{
    public partial class CodeGeneratorDialog : Window
    {
        private readonly ObservableCollection<ContractField> _fields;

        public CodeGeneratorDialog()
        {
            InitializeComponent();
            _fields = new ObservableCollection<ContractField>();
            lstFields.ItemsSource = _fields;
            txtOutput.Text = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "PITSysCodeGenerator", "Output");
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.SelectedPath = txtOutput.Text;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtOutput.Text = dlg.SelectedPath;
            }
        }

        private void AddField_Click(object sender, RoutedEventArgs e)
        {
            string name = txtFieldName.Text.Trim();
            string type = txtFieldType.Text.Trim();
            string label = txtFieldLabel.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type))
            {
                txtStatus.Text = "Field Name and Type are required.";
                txtStatus.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            if (string.IsNullOrEmpty(label))
            {
                label = D365NamingHelper.ToLabel(name);
            }

            _fields.Add(new ContractField
            {
                FieldName = D365NamingHelper.ToCamelCase(name),
                EdtType = type,
                Label = label,
                DisplayOrder = _fields.Count + 1
            });

            txtFieldName.Text = "";
            txtFieldType.Text = "";
            txtFieldLabel.Text = "";
            txtStatus.Text = "Field added.";
            txtStatus.Foreground = System.Windows.Media.Brushes.Green;
        }

        private void RemoveField_Click(object sender, RoutedEventArgs e)
        {
            if (lstFields.SelectedItem is ContractField selected)
            {
                _fields.Remove(selected);
                txtStatus.Text = "Field removed.";
            }
        }

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            string prefix = txtPrefix.Text.Trim();
            string className = txtClassName.Text.Trim();

            if (string.IsNullOrEmpty(className))
            {
                txtStatus.Text = "Class Name is required.";
                txtStatus.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            var options = new GeneratorOptions
            {
                Prefix = prefix,
                BaseClassName = className,
                BatchJobCaption = txtCaption.Text.Trim(),
                OutputDirectory = txtOutput.Text,
                Fields = new List<ContractField>(_fields),
                GenerateContract = chkContract.IsChecked == true,
                GenerateService = chkService.IsChecked == true,
                GenerateController = chkController.IsChecked == true,
                GenerateDataProvider = chkDP.IsChecked == true,
                GenerateUIBuilder = chkUI.IsChecked == true,
                GenerateMenuItem = chkMenu.IsChecked == true
            };

            var generated = new List<string>();

            try
            {
                if (options.GenerateContract)
                {
                    var g = new ContractGenerator();
                    FileOutputHelper.SaveToFile(options.OutputDirectory, g.GetFileName(options), g.Generate(options));
                    generated.Add(g.GetFileName(options) + ".xpp");
                }
                if (options.GenerateService)
                {
                    var g = new ServiceGenerator();
                    FileOutputHelper.SaveToFile(options.OutputDirectory, g.GetFileName(options), g.Generate(options));
                    generated.Add(g.GetFileName(options) + ".xpp");
                }
                if (options.GenerateController)
                {
                    var g = new ControllerGenerator();
                    FileOutputHelper.SaveToFile(options.OutputDirectory, g.GetFileName(options), g.Generate(options));
                    generated.Add(g.GetFileName(options) + ".xpp");
                }
                if (options.GenerateDataProvider)
                {
                    var g = new DataProviderGenerator();
                    FileOutputHelper.SaveToFile(options.OutputDirectory, g.GetFileName(options), g.Generate(options));
                    generated.Add(g.GetFileName(options) + ".xpp");
                }
                if (options.GenerateUIBuilder)
                {
                    var g = new UIBuilderGenerator();
                    FileOutputHelper.SaveToFile(options.OutputDirectory, g.GetFileName(options), g.Generate(options));
                    generated.Add(g.GetFileName(options) + ".xpp");
                }
                if (options.GenerateMenuItem)
                {
                    var g = new MenuItemGenerator();
                    FileOutputHelper.SaveXmlToFile(options.OutputDirectory, g.GetFileName(options), g.Generate(options));
                    generated.Add(g.GetFileName(options) + ".xml");
                }

                string files = string.Join("\n", generated);
                MessageBox.Show(
                    "Generated " + generated.Count + " file(s):\n\n" + files + "\n\nLocation:\n" + options.OutputDirectory,
                    "PITSysCodeGenerator",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                txtStatus.Text = "Generated " + generated.Count + " files successfully.";
                txtStatus.Foreground = System.Windows.Media.Brushes.Green;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "PITSysCodeGenerator", MessageBoxButton.OK, MessageBoxImage.Error);
                txtStatus.Text = "Error: " + ex.Message;
                txtStatus.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
