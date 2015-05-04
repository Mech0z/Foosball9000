$here = Split-Path $MyInvocation.MyCommand.Definition

properties {
    $NuGet = "$here\Tools\NuGet.exe"
    $MsBuild = "msbuild.exe"
    $FrontendhNuspec = "$here\frontend\FoosballFrontend.nuspec"
    $OctopusApiKey = ""
}

Task Default -Depends Clean,CompileBackend, PackFrontend


Task Clean {
    remove-item $here\*.nupkg -force
}

Task CompileBackend {
    Exec { & $nuget restore foosballold.sln } "Unable to restore nuget packages for foosballold.sln"
    #/logger:"C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll
    Exec { & $msbuild "foosballold.sln" /verbosity:minimal  /p:RunOctoPack=true /p:OctoPackPackageVersion=$Version}
}

Task PackFrontend {
       Exec {& $NuGet pack $FrontendhNuspec -NoPackageAnalysis -version $version} "Failed to pack $FrontendhNuspec"
}

Task Publish {
    get-childitem -path $here -filter "*.nupkg" | foreach {
        Exec {& $NuGet push $_.fullName 'API-YWAJKOJLP4XLKW10WVDQWMQUNG' -Source https://octopus.vfl.dk/nuget/packages} "Failed to push nupkg"

    }
}