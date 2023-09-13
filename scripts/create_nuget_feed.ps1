
docker run --rm --name nuget-server -d -p 5555:80 --env-file ../baget-data/baget.env -v "$(pwd)/baget-data:/var/baget" loicsharma/baget:latest

Write-Host "Local NuGet feed running."

Write-Host "Building latest Storage.Client package..."
dotnet build ../src/Storage.Client/
dotnet pack ../src/Storage.Client/bin/debug/Storage.Client~.nupkg

Write-Host "Publishing latest Storage.Client package..."
dotnet nuget push -s http://localhost:5555/v3/index.json -k testapikey ../src/Storage.Client/bin/debug/Storage.Client.1.0.0.nupkg
