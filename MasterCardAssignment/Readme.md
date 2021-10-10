## _Project Infrastucture_
- Please install .Net 5.0 or above in order to build and run the project 
- The input files are embedded within the project pipe.txt, comma.csv and space.dat which are delimited by pipe(|), comma(,) and whitespace(' ') respectively
- The input files are located on the root location of the project in order to avoid errors while getting full path in Windows vs Mac/*Nix machines (Home\Proj vs home/proj) for example. 
- [XUnit] is used for unit testing the components
- External libraries used for unit testing are 
    - [Moq], 
    - [Shouldly] and 
    - [Autofixture]

[XUnit]: <https://xunit.net/>
[Moq]: <https://github.com/moq/moq4> 
[Shouldly]: <https://github.com/shouldly/shouldly>
[AutoFixture]: <https://github.com/AutoFixture/AutoFixture>

## _Build and Run_
- To build the project, open a shell or terminal window, navigate to the root project where MasterCardAssignment.csproj file is located and execute

```sh
>MasterCardAssignment: dotnet build
```

To run the project execute
 ```sh
>MasterCardAssignment: dotnet run
```
Upon successful execution of the program, you should see the following message in the console/terminal window
> Successfully written the output to output.txt

Any exceptions will also be logged in the console/terminal window
## Thank You