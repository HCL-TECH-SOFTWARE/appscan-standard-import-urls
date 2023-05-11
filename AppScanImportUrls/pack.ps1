param ([string]$TargetDir)
$DestDir = $TargetDir + "ImportUrls"
$ZipPath = $TargetDir + "ImportUrls.zip"
Remove-Item -Path $DestDir -Force -Recurse
New-Item -Path $TargetDir -Name "ImportUrls" -ItemType "directory"
Copy-Item -Path "${TargetDir}*.dll", "${TargetDir}*.xml" -Destination $DestDir -Verbose
#Compress-Archive -Path $DestDir -DestinationPath $ZipPath -Force -Verbose
echo "Note: to build this project, you need to have 7-zip installed (http://www.7-zip.org/)"
& "C:\Program Files\7-Zip\7z.exe" a -tzip "$ZipPath" -r "$DestDir"
echo "Target created: $ZipPath"