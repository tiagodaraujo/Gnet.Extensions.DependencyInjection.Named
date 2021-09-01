.PHONY: build test pack

build:
	dotnet build NamedDependencyInjection.sln

test:
	dotnet test NamedDependencyInjection.sln

coverage:
	dotnet test NamedDependencyInjection.sln --collect:"XPlat Code Coverage"

pack:
	dotnet pack src/NamedDependencyInjection/NamedDependencyInjection.csproj -c Release --include-source --include-symbols -o nupkgs
