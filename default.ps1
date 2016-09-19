$here = Split-Path $MyInvocation.MyCommand.Definition

properties {
    $NuGet = "$here\Tools\NuGet.exe"
    $MsBuild = "C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe"
    $FrontendhNuspec = "$here\frontend\FoosballFrontend.nuspec"
}

Task Default -Depends Clean,CompileBackend, PackFrontend, CreateRelease


Task Clean {

    Write-Host $releasenotes
    remove-item $here\*.nupkg -force
}

Task CompileBackend {
    Exec { & $nuget restore foosball9000.sln } "Unable to restore nuget packages for foosball9000.sln"
    #/logger:"C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll
    Exec { & $msbuild "foosball9000.sln" /verbosity:normal  /p:RunOctoPack=true /p:OctoPackPackageVersion=$Version}
}

Task PackFrontend {
    Exec {& $NuGet pack $FrontendhNuspec -NoPackageAnalysis -version $version} "Failed to pack $FrontendhNuspec"
}

Task CreateRelease {
    if($OctopusApiKey) {
        Exec {& $Nuget  install octopustools -OutputDirectory tools -Version 2.6.1.52 } "Failed to install octopus tools"
        $Octo = $here+'\tools\OctopusTools.2.6.1.52\octo.exe'
        Exec { & $Octo create-release --server http://octopus.sovs.net:5602 --apikey $OctopusApiKey --project Foosball9000 --enableservicemessages --version $Version --deployto Staging --package=Foosball9000api:$Version --package=FoosballFrontend:$Version } "Failed to create Octopus release"
    } else {
        Write-Host "Skipping creating release"
    }
}
