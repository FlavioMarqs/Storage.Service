
Write-Host ">>Building latest Storage.Client packages..."
dotnet build -c Release ../Storage.Service.sln
dotnet pack -c Release ../src/Storage.Client/
Write-Host ">>Finding .nupkg files..."
$nupkgs = Get-ChildItem -recurse -Path ../src/ -filter *.nupkg

foreach ($nupkg in $nupkgs)
{
	if($nupkg.FullName.Contains("Release"))
	{
		Write-Host ">>Pushing " $nupkg.FullName
#		dotnet pack $nupkg.FullName
		dotnet nuget push -s http://localhost:5555/v3/index.json -k 417e0b54996540334ac722e16efa88e054ef05cf59ed6c5fd77b8033f4d0be40 $nupkg.FullName
	}
}