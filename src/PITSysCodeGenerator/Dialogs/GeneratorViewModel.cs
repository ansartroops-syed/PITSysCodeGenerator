using PITSysCodeGenerator.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PITSysCodeGenerator.Dialogs
{
    /// <summary>
    /// ViewModel for the Code Generator dialog.
    /// Manages all user input and field collection.
    /// </summary>
    public class GeneratorViewModel : INotifyPropertyChanged
    {
        private string _baseClassName = "SampleBatchJob";
        private string _prefix = "PIT";
        private string _description = "Sample SysOperation batch job";
        private string _batchJobCaption = "Sample Batch Job";
        private string _outputDirectory;
        private string _tempTableName;
        private SysOperationType _operationType = SysOperationType.Standard;
        private bool _generateContract = true;
        private bool _generateService = true;
        private bool _generateController = true;
        private bool _generateDataProvider;
        private bool _generateUIBuilder;
        private bool _generateMenuItem;
        private bool _enableBatch = true;

        public GeneratorViewModel()
        {
            Fields = new ObservableCollection<ContractField>();
            _outputDirectory = System.IO.Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments),
                "PITSysCodeGenerator", "Output");
        }

        public string BaseClassName
        {
            get => _baseClassName;
            set { _baseClassName = value; OnPropertyChanged(); OnPropertyChanged(nameof(PreviewContractName)); OnPropertyChanged(nameof(PreviewServiceName)); OnPropertyChanged(nameof(PreviewControllerName)); }
        }

        public string Prefix
        {
            get => _prefix;
            set { _prefix = value; OnPropertyChanged(); OnPropertyChanged(nameof(PreviewContractName)); OnPropertyChanged(nameof(PreviewServiceName)); OnPropertyChanged(nameof(PreviewControllerName)); }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        public string BatchJobCaption
        {
            get => _batchJobCaption;
            set { _batchJobCaption = value; OnPropertyChanged(); }
        }

        public string OutputDirectory
        {
            get => _outputDirectory;
            set { _outputDirectory = value; OnPropertyChanged(); }
        }

        public string TempTableName
        {
            get => _tempTableName;
            set { _tempTableName = value; OnPropertyChanged(); }
        }

        public SysOperationType OperationType
        {
            get => _operationType;
            set
            {
                _operationType = value;
                OnPropertyChanged();
                ApplyOperationTypeDefaults();
            }
        }

        public bool GenerateContract
        {
            get => _generateContract;
            set { _generateContract = value; OnPropertyChanged(); }
        }

        public bool GenerateService
        {
            get => _generateService;
            set { _generateService = value; OnPropertyChanged(); }
        }

        public bool GenerateController
        {
            get => _generateController;
            set { _generateController = value; OnPropertyChanged(); }
        }

        public bool GenerateDataProvider
        {
            get => _generateDataProvider;
            set { _generateDataProvider = value; OnPropertyChanged(); }
        }

        public bool GenerateUIBuilder
        {
            get => _generateUIBuilder;
            set { _generateUIBuilder = value; OnPropertyChanged(); }
        }

        public bool GenerateMenuItem
        {
            get => _generateMenuItem;
            set { _generateMenuItem = value; OnPropertyChanged(); }
        }

        public bool EnableBatch
        {
            get => _enableBatch;
            set { _enableBatch = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ContractField> Fields { get; set; }

        // Preview names
        public string PreviewContractName => $"{Prefix}{BaseClassName}Contract";
        public string PreviewServiceName => $"{Prefix}{BaseClassName}Service";
        public string PreviewControllerName => $"{Prefix}{BaseClassName}Controller";

        /// <summary>
        /// Converts the ViewModel to a GeneratorOptions model.
        /// </summary>
        public GeneratorOptions ToGeneratorOptions()
        {
            return new GeneratorOptions
            {
                BaseClassName = BaseClassName,
                Prefix = Prefix,
                Description = Description,
                BatchJobCaption = BatchJobCaption,
                OutputDirectory = OutputDirectory,
                TempTableName = TempTableName,
                OperationType = OperationType,
                GenerateContract = GenerateContract,
                GenerateService = GenerateService,
                GenerateController = GenerateController,
                GenerateDataProvider = GenerateDataProvider,
                GenerateUIBuilder = GenerateUIBuilder,
                GenerateMenuItem = GenerateMenuItem,
                EnableBatch = EnableBatch,
                Fields = new System.Collections.Generic.List<ContractField>(Fields)
            };
        }

        private void ApplyOperationTypeDefaults()
        {
            switch (_operationType)
            {
                case SysOperationType.Standard:
                    GenerateContract = true;
                    GenerateService = true;
                    GenerateController = true;
                    GenerateDataProvider = false;
                    GenerateUIBuilder = false;
                    GenerateMenuItem = false;
                    break;
                case SysOperationType.ReportDataProvider:
                    GenerateContract = true;
                    GenerateService = false;
                    GenerateController = true;
                    GenerateDataProvider = true;
                    GenerateUIBuilder = false;
                    GenerateMenuItem = false;
                    break;
                case SysOperationType.CustomUIBuilder:
                    GenerateContract = true;
                    GenerateService = true;
                    GenerateController = true;
                    GenerateDataProvider = false;
                    GenerateUIBuilder = true;
                    GenerateMenuItem = false;
                    break;
                case SysOperationType.Full:
                    GenerateContract = true;
                    GenerateService = true;
                    GenerateController = true;
                    GenerateDataProvider = true;
                    GenerateUIBuilder = true;
                    GenerateMenuItem = true;
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}