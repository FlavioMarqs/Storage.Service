$shouldStartDockerContainer = 0;

$containers = docker ps --filter "name=nuget-server"
if (docker ps -f "name=nuget-server")
{
	Write-host "The nuget-server container is running!"
}
else
{
	Write-host "Resetting nuget-feed docker image, deploying it and starting it."
	docker run --rm --name nuget-server -d -p 5555:80 --env-file ../baget-data/baget.env -v "$(pwd)/baget-data:/var/baget" loicsharma/baget:latest
}

Write-Host ">>Local NuGet feed running."
