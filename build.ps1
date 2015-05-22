param (
    [Parameter(Mandatory=$True,Position=1)]
    [string]$Version = "0.0.0.0",
    [string]$Configuration = "Release",
    [string]$OctopusApiKey = $null,
    [string]$ReleaseNotes
)

Write-Host $Version
$here = Split-Path $MyInvocation.MyCommand.Definition
$env:EnableNuGetPackageRestore = 'false'
$NuGetExe = $here+'\tools\nuget.exe'

New-Item -type Directory "$here\tools" -force

if ((Test-Path $NuGetExe) -eq $false) {(New-Object System.Net.WebClient).DownloadFile('http://nuget.org/nuget.exe', $NuGetExe)}
& $NuGetExe install psake -OutputDirectory tools -Version 4.2.0.1
& $NuGetExe install CiPsLib -OutputDirectory tools -Version 0.4.0.26

if((Get-Module psake) -eq $null)
{
    Import-Module $here\tools\psake.4.2.0.1\tools\psake.psm1 -Force
    Import-Module $here\tools\cipslib.0.4.0.26\tools\CiPsLib.Common.psm1 -Force
}

#$TmpPath = $MyDir+'\tmp'
#[IO.Directory]::CreateDirectory($TmpPath)

$psake.use_exit_on_error = $true
Invoke-psake -buildFile $here'.\Default.ps1' -parameters @{"Version"=$Version;"Configuration"=$Configuration;"NuGetPack"="true";"OctopusApiKey"=$OctopusApiKey;"ReleaseNotes"=$releasenotes}
if ($psake.build_success -eq $false) { exit 1 } else { exit 0 }