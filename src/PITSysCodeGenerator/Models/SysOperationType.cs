namespace PITSysCodeGenerator.Models
{
    /// <summary>
    /// Defines the type of SysOperation batch pattern being generated.
    /// </summary>
    public enum SysOperationType
    {
        /// <summary>
        /// Standard Service pattern: Contract + Service + Controller
        /// </summary>
        Standard = 0,

        /// <summary>
        /// Report Data Provider pattern: Contract + DataProvider + Controller
        /// </summary>
        ReportDataProvider = 1,

        /// <summary>
        /// Custom UI Builder pattern: Contract + Service + Controller + UIBuilder
        /// </summary>
        CustomUIBuilder = 2,

        /// <summary>
        /// Full pattern: All classes including MenuItem
        /// </summary>
        Full = 3
    }
}