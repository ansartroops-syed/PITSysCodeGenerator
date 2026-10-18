using System.Collections.Generic;

namespace PITSysCodeGenerator.Models
{
    /// <summary>
    /// Encapsulates all options/settings collected from the user dialog
    /// that drive code generation.
    /// </summary>
    public class GeneratorOptions
    {
        /// <summary>
        /// Base class name without prefix (e.g., "SalesInvoicePost")
        /// </summary>
        public string BaseClassName { get; set; }

        /// <summary>
        /// Prefix for all generated classes (default: "PIT")
        /// </summary>
        public string Prefix { get; set; } = "PIT";

        /// <summary>
        /// Description/summary for the generated classes
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The SysOperation pattern type to generate
        /// </summary>
        public SysOperationType OperationType { get; set; } = SysOperationType.Standard;

        /// <summary>
        /// Contract data member fields
        /// </summary>
        public List<ContractField> Fields { get; set; } = new List<ContractField>();

        /// <summary>
        /// Whether to generate the Contract class
        /// </summary>
        public bool GenerateContract { get; set; } = true;

        /// <summary>
        /// Whether to generate the Service class
        /// </summary>
        public bool GenerateService { get; set; } = true;

        /// <summary>
        /// Whether to generate the Controller class
        /// </summary>
        public bool GenerateController { get; set; } = true;

        /// <summary>
        /// Whether to generate the DataProvider class (for RDP reports)
        /// </summary>
        public bool GenerateDataProvider { get; set; }

        /// <summary>
        /// Whether to generate the UIBuilder class
        /// </summary>
        public bool GenerateUIBuilder { get; set; }

        /// <summary>
        /// Whether to generate the MenuItem XML
        /// </summary>
        public bool GenerateMenuItem { get; set; }

        /// <summary>
        /// Output directory for generated files
        /// </summary>
        public string OutputDirectory { get; set; }

        /// <summary>
        /// The batch job caption/title
        /// </summary>
        public string BatchJobCaption { get; set; }

        /// <summary>
        /// Whether the service should run in batch
        /// </summary>
        public bool EnableBatch { get; set; } = true;

        /// <summary>
        /// Temp table name for RDP pattern
        /// </summary>
        public string TempTableName { get; set; }

        #region Computed Names

        /// <summary>
        /// Full contract class name (e.g., "PITSalesInvoicePostContract")
        /// </summary>
        public string ContractClassName => $"{Prefix}{BaseClassName}Contract";

        /// <summary>
        /// Full service class name (e.g., "PITSalesInvoicePostService")
        /// </summary>
        public string ServiceClassName => $"{Prefix}{BaseClassName}Service";

        /// <summary>
        /// Full controller class name (e.g., "PITSalesInvoicePostController")
        /// </summary>
        public string ControllerClassName => $"{Prefix}{BaseClassName}Controller";

        /// <summary>
        /// Full data provider class name (e.g., "PITSalesInvoicePostDP")
        /// </summary>
        public string DataProviderClassName => $"{Prefix}{BaseClassName}DP";

        /// <summary>
        /// Full UI builder class name (e.g., "PITSalesInvoicePostUIBuilder")
        /// </summary>
        public string UIBuilderClassName => $"{Prefix}{BaseClassName}UIBuilder";

        /// <summary>
        /// Menu item name (e.g., "PITSalesInvoicePost")
        /// </summary>
        public string MenuItemName => $"{Prefix}{BaseClassName}";

        #endregion
    }
}