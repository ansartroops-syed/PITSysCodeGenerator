# Installation Guide

## Prerequisites

1. **Visual Studio 2022** (Community, Professional, or Enterprise) version 17.0 or later
2. **.NET Framework 4.8** (included with Windows 10 1903+ and Windows 11)
3. **Visual Studio Extensibility Workload** (for building from source)

## Installation Methods

### Method 1: Install from VSIX (Recommended)

1. Download the latest 'PITSysCodeGenerator.vsix' from the [Releases page](https://github.com/YourUsername/PITSysCodeGenerator/releases)
2. Close all instances of Visual Studio 2022
3. Double-click the '.vsix' file
4. The VSIX Installer will launch — click **Install**
5. Wait for installation to complete
6. Open Visual Studio 2022
7. Go to **Tools** menu — you should see **PIT SysOperation Code Generator**

### Method 2: Build from Source

1. Clone the repository:
git clone https://github.com/ansartroops-syed/PITSysCodeGenerator.git
2. Open 'PITSysCodeGenerator.sln' in Visual Studio 2022
3. Ensure the **Visual Studio extension development** workload is installed
4. Build the solution (Ctrl+Shift+B)
5. The VSIX file will be in 'src\PITSysCodeGenerator\bin\Release\PITSysCodeGenerator.vsix'
6. Install the VSIX as described in Method 1

## Verification

1. Open Visual Studio 2022
2. Go to **Extensions → Manage Extensions**
3. Search for "PITSysCodeGenerator" in the Installed tab
4. Verify it shows as installed and enabled