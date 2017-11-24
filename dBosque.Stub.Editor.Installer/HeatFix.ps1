 param (
    [Parameter(Mandatory=$true)][string]$publishdir,
    [Parameter(Mandatory=$true)][string]$file
 )
 
 Write-Output $publishdir
 Write-Output $file
 (Get-Content $file) -replace "SourceDir\\",$publishdir | out-file $file -Encoding "UTF8"