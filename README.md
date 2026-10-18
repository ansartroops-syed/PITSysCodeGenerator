# PITSysCodeGenerator

[![Visual Studio Marketplace](https://img.shields.io/visual-studio-marketplace/v/YourPublisher.PITSysCodeGenerator?label=VS%20Marketplace&style=flat-square)](https://marketplace.visualstudio.com/items?itemName=YourPublisher.PITSysCodeGenerator)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=flat-square)](LICENSE)

A **Visual Studio 2022** extension that generates **SysOperation Framework** classes for **Microsoft Dynamics 365 Finance & Operations (D365FO)**.

Generate production-ready **Contract**, **Service**, **Controller**, **DataProvider**, **UIBuilder**, and **MenuItem** classes with a single click — following best practices and the SysOperation Builder pattern.

---

## ✨ Features

| Feature | Description |
|---|---|
| 🔧 Contract Generator | Generates `DataContractAttribute` classes with `DataMemberAttribute` parm methods |
| ⚙️ Service Generator | Generates service classes with the `process()` method |
| 🎮 Controller Generator | Generates `SysOperationServiceController` with `caption()` and `templateForm()` |
| 📊 DataProvider Generator | Generates `SysOperationRDPBase`-derived report data providers |
| 🏗️ UIBuilder Generator | Generates `SysOperationAutomaticUIBuilder` with custom `postBuild()` logic |
| 📋 MenuItem Generator | Generates Action MenuItem metadata XML |
| 🎨 WPF Dialog | User-friendly WPF dialog with dynamic field management |
| 📐 Naming Conventions | Automatic D365FO naming convention enforcement (PIT prefix) |
| 📦 Bulk Generation | Generate all related classes at once |

---

## 📋 Prerequisites

- **Visual Studio 2022** (17.0 or later)
- **.NET Framework 4.8**
- **Dynamics 365 Finance & Operations** development environment (optional, for deployment)

---

## 🚀 Installation

### From VSIX File
1. Download the latest `.vsix` from [Releases](https://github.com/YourUsername/PITSysCodeGenerator/releases)
2. Double-click the `.vsix` file
3. Follow the installation wizard
4. Restart Visual Studio

### From Visual Studio Marketplace
1. Open Visual Studio 2022
2. Go to **Extensions → Manage Extensions**
3. Search for **PITSysCodeGenerator**
4. Click **Download** and restart Visual Studio

---

## 📖 Usage

1. Open Visual Studio 2022
2. Go to **Tools → PIT SysOperation Code Generator**
3. Fill in the dialog:
   - Enter the **Base Class Name** (e.g., `SalesInvoicePost`)
   - Add **Contract Fields** (name, type, label, mandatory, group)
   - Select which classes to generate
   - Choose output directory
4. Click **Generate**
5. Copy generated files to your D365FO project

---

## 📂 Generated Code Example

### Contract Class
```xpp
[DataContractAttribute]
class PITSalesInvoicePostContract
{
    CustAccount custAccount;
    TransDate   fromDate;

    [DataMemberAttribute('CustAccount'),
     SysOperationLabelAttribute(literalStr("Customer account")),
     SysOperationDisplayOrderAttribute('1')]
    public CustAccount parmCustAccount(CustAccount _custAccount = custAccount)
    {
        custAccount = _custAccount;
        return custAccount;
    }
}