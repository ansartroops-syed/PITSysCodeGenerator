\# Usage Guide



\## Quick Start



1\. Open Visual Studio 2022

2\. Go to \*\*Tools → PIT SysOperation Code Generator\*\*

3\. The Code Generator dialog will open



\## Dialog Configuration



\### Step 1: Base Configuration



\- \*\*Prefix\*\*: Your organization's code prefix (default: `PIT`)

\- \*\*Base Class Name\*\*: The core name without prefix or suffix (e.g., `SalesInvoicePost`)

\- \*\*Description\*\*: Short description of what the batch job does

\- \*\*Batch Job Caption\*\*: The title displayed in the batch job dialog



\### Step 2: Select Pattern



Choose the SysOperation pattern that fits your needs:



| Pattern | Generated Classes |

|---|---|

| \*\*Standard\*\* | Contract + Service + Controller |

| \*\*Report Data Provider\*\* | Contract + DataProvider + Controller |

| \*\*Custom UI Builder\*\* | Contract + Service + Controller + UIBuilder |

| \*\*Full\*\* | All classes + Menu Item |



\### Step 3: Define Contract Fields



Add the data member fields for your Contract class:



1\. Enter \*\*Field Name\*\* in camelCase (e.g., `custAccount`)

2\. Enter the \*\*EDT/Type\*\* (e.g., `CustAccount`, `TransDate`, `str`)

3\. Enter a \*\*Label\*\* (auto-generated if left empty)

4\. Check \*\*Mandatory\*\* if the field is required

5\. Click \*\*Add Field\*\*



You can also click \*\*Add Sample Fields\*\* to see examples.



\### Step 4: Generate



\- Click \*\*Preview\*\* to see the generated code before saving

\- Click \*\*Generate All\*\* to save all files to the output directory

\- Click \*\*Copy to Clipboard\*\* in the preview area for individual classes



\## Output



Generated files are saved as `.xpp` files (X++ source) that can be:

\- Copied into your D365FO project in Visual Studio

\- Used as a reference for creating classes in the AOT



\## Tips



\- Use PascalCase for class names and camelCase for field names

\- The generator automatically creates `parm` methods following D365 conventions

\- UIBuilder generates `postBuild()` and `postRun()` methods with field binding

\- DataProvider generates the `processReport()` pattern with temp table support

