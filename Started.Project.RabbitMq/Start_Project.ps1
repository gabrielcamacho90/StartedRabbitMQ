$Name = (Get-Item -Path ".\").Name
$Name = $Name -replace "-", "."
ren .git _.git
Remove-Item -LiteralPath "_.git" -Force -Recurse

ren InitialProject.Api "$Name.Api"
ren InitialProject.Domain "$Name.Domain"
ren InitialProject.Infra "$Name.Infra"
ren InitialProject.Shared "$Name.Shared"
# ren InitialProject.Site "$Name.Site"
ren InitialProject.Test "$Name.Test"


cd *.Api
ren InitialProject.Api.csproj "$Name.Api.csproj"
cd ..

cd *.Domain
ren InitialProject.Domain.csproj "$Name.Domain.csproj"
cd ..

cd *.Infra
ren InitialProject.Infra.csproj "$Name.Infra.csproj"
cd ..

cd *.Shared
ren InitialProject.Shared.csproj "$Name.Shared.csproj"
cd ..

cd *.Test
ren InitialProject.Test.csproj "$Name.Test.csproj"
cd ..

ren InitialProject.sln "$Name.sln"

(Get-Content "$Name.sln") | ForEach-Object { $_ -replace "InitialProject", $Name } | Set-Content "$Name.sln"


$Dir = get-childitem "$Name.Api" -recurse
# $Dir |get-member
$List = $Dir | where {$_.extension -eq ".cs"}
$List.FullName | ForEach-Object {[System.IO.File]::WriteAllText("$_", [System.IO.File]::ReadAllText("$_").Replace("InitialProject",$Name)) }

$Dir = get-childitem "$Name.Api" -recurse
# $Dir |get-member
$List = $Dir | where {$_.extension -eq ".csproj"}
$List.FullName | ForEach-Object {[System.IO.File]::WriteAllText("$_", [System.IO.File]::ReadAllText("$_").Replace("InitialProject",$Name)) }

$Dir = get-childitem "$Name.Domain" -recurse
# $Dir |get-member
$List = $Dir | where {$_.extension -eq ".cs"}
$List.FullName | ForEach-Object {[System.IO.File]::WriteAllText("$_", [System.IO.File]::ReadAllText("$_").Replace("InitialProject",$Name)) }

$Dir = get-childitem "$Name.Domain" -recurse
# $Dir |get-member
$List = $Dir | where {$_.extension -eq ".csproj"}
$List.FullName | ForEach-Object {[System.IO.File]::WriteAllText("$_", [System.IO.File]::ReadAllText("$_").Replace("InitialProject",$Name)) }

$Dir = get-childitem "$Name.Infra" -recurse
# $Dir |get-member
$List = $Dir | where {$_.extension -eq ".cs"}
$List.FullName | ForEach-Object {[System.IO.File]::WriteAllText("$_", [System.IO.File]::ReadAllText("$_").Replace("InitialProject",$Name)) }

$Dir = get-childitem "$Name.Infra" -recurse
# $Dir |get-member
$List = $Dir | where {$_.extension -eq ".csproj"}
$List.FullName | ForEach-Object {[System.IO.File]::WriteAllText("$_", [System.IO.File]::ReadAllText("$_").Replace("InitialProject",$Name)) }

$Dir = get-childitem "$Name.Shared" -recurse
# $Dir |get-member
$List = $Dir | where {$_.extension -eq ".cs"}
$List.FullName | ForEach-Object {[System.IO.File]::WriteAllText("$_", [System.IO.File]::ReadAllText("$_").Replace("InitialProject",$Name)) }

$Dir = get-childitem "$Name.Shared" -recurse
# $Dir |get-member
$List = $Dir | where {$_.extension -eq ".csproj"}
$List.FullName | ForEach-Object {[System.IO.File]::WriteAllText("$_", [System.IO.File]::ReadAllText("$_").Replace("InitialProject",$Name)) }

$Dir = get-childitem "$Name.Test"  -recurse
# $Dir |get-member
$List = $Dir | where {$_.extension -eq ".cs"}
$List.FullName | ForEach-Object {[System.IO.File]::WriteAllText("$_", [System.IO.File]::ReadAllText("$_").Replace("InitialProject",$Name)) }

$Dir = get-childitem "$Name.Test" -recurse
# $Dir |get-member
$List = $Dir | where {$_.extension -eq ".csproj"}
$List.FullName | ForEach-Object {[System.IO.File]::WriteAllText("$_", [System.IO.File]::ReadAllText("$_").Replace("InitialProject",$Name)) }