cd src
dotnet publish ./Action.Api -c Release -o ./bin/Docker
dotnet publish ./Action.Services.Activities -c Release -o ./bin/Docker
dotnet publish ./Action.Services.Identity -c Release -o ./bin/Docker