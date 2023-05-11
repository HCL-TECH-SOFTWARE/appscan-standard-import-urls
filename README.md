# AppScan Standard - Import URLs extension

This library is an extension to HCL AppScan Standard.

It allows the user to import a text file containing a list of URLs into AppScan. AppScan will explore these URLs as if they were explored manually.

## Installation

To install, the extension must be imported into AppScan Standard using the AppScan extension manager.

1. Open the extension manager: Tools > Extensions > Extension Manager
1. Import the extension ImportUrls.zip using Add Extensions From > This Computer 
1. Restart AppScan
1. Open the extension manager.
1. Click "trust" to trust the extension.

## Usage

Once installed, you can access the extension using Tools > Extensions > Import URLs from file

Configuration:

- **Input File**: The actual file to import. Must be a text file with a URL in each line.
- **Base URL**: If configured, this will be used as the base URL for any relative URLs in the file.
- **Use session cookies**: If set, the extension will add session cookies from the scan configuration to each request. If there are no session cookies configured, this checkbox is disabled.

After configuring the extension and clicking *Import*, it will convert the list of URLs into an EXD (EXplore Data) file and import it into AppScan.

## Building

In order to build this extension, you must have AppScan Standard and 7-zip installed. If they are not installed in the default location, you must edit their locations in *ImportUrls.csproj* or *pack.ps1*.

Building the solution should automatically generate the file *ImportUrls.zip*.