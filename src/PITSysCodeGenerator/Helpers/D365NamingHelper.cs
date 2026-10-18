using System;
using System.Text.RegularExpressions;

namespace PITSysCodeGenerator.Helpers
{
    /// <summary>
    /// Helper class that enforces D365FO naming conventions.
    /// </summary>
    public static class D365NamingHelper
    {
        /// <summary>
        /// Validates that a class name follows D365 conventions.
        /// </summary>
        public static bool IsValidClassName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            return Regex.IsMatch(name, @"^[A-Z][a-zA-Z0-9]*$");
        }

        /// <summary>
        /// Validates that a field name follows camelCase convention.
        /// </summary>
        public static bool IsValidFieldName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            return Regex.IsMatch(name, @"^[a-z][a-zA-Z0-9]*$");
        }

        /// <summary>
        /// Converts a string to a valid camelCase field name.
        /// </summary>
        public static string ToCamelCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            // Remove non-alphanumeric characters
            var cleaned = Regex.Replace(input, @"[^a-zA-Z0-9]", "");
            if (cleaned.Length == 0) return "field";

            return char.ToLowerInvariant(cleaned[0]) + cleaned.Substring(1);
        }

        /// <summary>
        /// Converts a string to PascalCase.
        /// </summary>
        public static string ToPascalCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var cleaned = Regex.Replace(input, @"[^a-zA-Z0-9]", "");
            if (cleaned.Length == 0) return "Class";

            return char.ToUpperInvariant(cleaned[0]) + cleaned.Substring(1);
        }

        /// <summary>
        /// Creates a parm method name from a field name.
        /// e.g., "custAccount" → "parmCustAccount"
        /// </summary>
        public static string ToParmMethodName(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName)) return "parmField";
            return "parm" + char.ToUpperInvariant(fieldName[0]) + fieldName.Substring(1);
        }

        /// <summary>
        /// Creates a label from a field name using word-break on PascalCase.
        /// e.g., "custAccount" → "Cust account"
        /// </summary>
        public static string ToLabel(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName)) return string.Empty;

            var pascalName = ToPascalCase(fieldName);
            var words = Regex.Replace(pascalName, @"([A-Z])", " $1").Trim();
            return words.Substring(0, 1).ToUpper() + words.Substring(1).ToLower();
        }

        /// <summary>
        /// Returns common D365 EDT types for autocomplete/suggestion.
        /// </summary>
        public static string[] GetCommonEdtTypes()
        {
            return new[]
            {
                "CustAccount", "VendAccount", "ItemId", "InventDimId",
                "TransDate", "FromDate", "ToDate",
                "Name", "Description", "Notes",
                "AmountCur", "AmountMST", "Qty", "Price",
                "CustGroupId", "VendGroupId", "ItemGroupId",
                "SalesId", "PurchId", "InventTransId",
                "LedgerAccount", "DimensionDefault",
                "NoYesId", "boolean", "str", "int", "real", "int64",
                "utcdatetime", "date", "guid",
                "LegalEntityDataAreaId", "CompanyId"
            };
        }
    }
}