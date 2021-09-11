.PHONY: build test pack

build:
	dotnet build DependencyInjection.Named.sln

test:
	dotnet test DependencyInjection.Named.sln

coverage:
	dotnet test DependencyInjection.Named.sln --collect:"XPlat Code Coverage"

pack:
	dotnet pack src/DependencyInjection.Named/DependencyInjection.Named.csproj -c Release --include-source --include-symbols -o nupkgs
