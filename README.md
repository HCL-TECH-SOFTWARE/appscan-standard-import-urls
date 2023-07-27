# AppScan Standard - Import URLs extension

This library is an extension for HCL AppScan Standard.

It enables you to import into AppScan a text file containing a list of URLs. AppScan will explore these URLs as if you explored them manually yourself.

## Installation

To install, the extension must be imported into AppScan Standard using the AppScan Extension Manager.

1. In AppScan Standard, go to Tools > Extensions > Extension Manager
1. Import the extension ImportUrls.zip using Add Extensions From: > This Computer 
1. Restart AppScan Standard for the change to take effect.
1. Open Extension Manager.
1. Click "Trust" to trust the extension.

## Usage

Once installed, you can access the extension using Tools > Extensions > Import URLs from file

Configuration:

- **Input File**: The file you want to import. Must be a text file with one URL or relative path per line.
- **Base URL**: If the list includes relative paths, add here the base URL for these paths.
- **Use session cookies**: If set, the extension will add session cookies from the scan configuration to each request. If there are no session cookies configured, this checkbox is disabled.

After configuring the extension, click *Import* to convert the list of URLs into a temporary EXD (EXplore Data) file and import it into AppScan. AppScan will then explore these URLs automatically. You can then decide whether to continue with a Test stage or more manual exploring.

## Building
In order to build this extension, you must have AppScan Standard and 7-zip installed. If they are not installed in their default locations, you must edit their locations in `ImportUrls.csproj`.

The file `ImportUrls.zip` is automatically generated in the sub folder: \appscan-standard-import-urls\AppScanImportUrls\Output
