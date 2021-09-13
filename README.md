# Gnet.Extensions.DependencyInjection.Named

<img src="logos/Gnet-banner.png" width="40%" height="40%">

> Named Dependency Injection Extensions for .NET Core.

Gnet DependencyInjection Named is a set of extensions to `Microsoft.Extensions.DependencyInjection` to register on `IServiceCollection` and get from `IServiceProvider` named services without override the vanilla .net core dependency injection engine.

## Getting Started

Open the solution `DependencyInjection.Named.sln` on Visual Studio.

### How to build the library?

 Open the solution file `DependencyInjection.Named.sln` with Visual Studio, and Build the Solution (Build -> Build Solution)

 or

 Execute the following make command.
 ```
 make build
 ```

 ### How to run the unit tests?

 Open the solution file `DependencyInjection.Named.sln` with Visual Studio, and run the unit tests (Test -> Run All Tests)

 or

 Execute the following make command.
 ```
 make test
 ```

 ### How to pack the library?

 Open the solution file `DependencyInjection.Named.sln` with Visual Studio, and pack the HttpContextMoq (Build -> Pack HttpContextMoq)

  ```
 make pack
 ```

 ## Contributing

Please read [contributing](CONTRIBUTING.md) for details of the code of conduct, and the process for submitting pull requests to us.

## Versioning

Uses [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository][tags].

[tags]: https://github.com/tiagodaraujo/Gnet.Extensions.DependencyInjection.Named/tags

## Authors

* **Tiago Araújo** - *Initial work* - [tiagodaraujo](https://github.com/tiagodaraujo)

See also the list of [contributors](https://github.com/tiagodaraujo/Gnet.Extensions.DependencyInjection.Named/contributors) who participated in this project.