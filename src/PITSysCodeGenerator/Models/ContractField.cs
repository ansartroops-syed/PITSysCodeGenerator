using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PITSysCodeGenerator.Models
{
    /// <summary>
    /// Represents a single data member field in a SysOperation Contract class.
    /// Used by the dialog to collect field definitions from the user.
    /// </summary>
    public class ContractField : INotifyPropertyChanged
    {
        private string _fieldName;
        private string _edtType;
        private string _label;
        private bool _isMandatory;
        private string _groupName;
        private int _displayOrder;
        private string _helpText;

        /// <summary>
        /// Field variable name (e.g., "custAccount")
        /// </summary>
        public string FieldName
        {
            get => _fieldName;
            set { _fieldName = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// EDT or primitive type (e.g., "CustAccount", "TransDate", "str", "int")
        /// </summary>
        public string EdtType
        {
            get => _edtType;
            set { _edtType = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Label for the SysOperationLabelAttribute
        /// </summary>
        public string Label
        {
            get => _label;
            set { _label = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Whether the field is mandatory (adds SysOperationMandatoryAttribute)
        /// </summary>
        public bool IsMandatory
        {
            get => _isMandatory;
            set { _isMandatory = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Group name for SysOperationGroupAttribute
        /// </summary>
        public string GroupName
        {
            get => _groupName;
            set { _groupName = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Display order for SysOperationDisplayOrderAttribute
        /// </summary>
        public int DisplayOrder
        {
            get => _displayOrder;
            set { _displayOrder = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Help text for SysOperationHelpTextAttribute
        /// </summary>
        public string HelpText
        {
            get => _helpText;
            set { _helpText = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Returns the parm method name (e.g., "parmCustAccount")
        /// </summary>
        public string ParmMethodName
        {
            get
            {
                if (string.IsNullOrEmpty(FieldName)) return string.Empty;
                return "parm" + char.ToUpperInvariant(FieldName[0]) + FieldName.Substring(1);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}