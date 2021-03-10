# EARIN EX1
Optimization program, used to minimize a function. 
Created in C# by Oskar Hącel & Marcin Lisowski 
Politechnika Warszawska, 03.2021

## Preparation
1. Download release package or build project yourself
1. Release package supports Windows machines
1. Be sure, that decimal separator is set to "." (dot) in Windows Control Panel
## Settings
#### appsettings.json
```json
{
  "Settings": {
    "MaxExecutionTimeInMs": 5000,
    "DesiredPrecision": 0.001
  }
}
```
* `MaxExecutionTimeInMs` is used to set calculations timeout (in miliseconds),
* `DesiredPrecision` is used to specify desired precision that algorithm will try to reach.
## Build
Build using newest version of Visual Studio 2019 (16.9.0) and .Net Core 3.0
## Usage
### Interactive mode
Run application (using command line) to run it in interactive mode, like:
```cmd
FunctionMinimization.exe
```
Follow instructions to proceed
### Single-run mode
Run application with following arguments:
```cmd
FunctionMinimization.exe <MethodType> [beta (if grad. descent)] <C> <B> <A> <X0> <desired J(X)> <n>
```
or
```cmd
FunctionMinimization.exe<MethodType> [beta (if grad. descent)] <C> <B> <A> <l> <u> <desired J(X)> <n>
```
Examples:
```cmd
FunctionMinimization Newtons 1.0 1.0,0.0 1.0,0.0;0.0,1.0 -50,50 0.75 1
FunctionMinimization NewtonsNum 1.0 1.0,0.0 1.0,0.0;0.0,1.0 -50,50 0.75 1
FunctionMinimization SimpleGradient 0.01 1.0 1.0,0.0 1.0,0.0;0.0,1.0 -100 100 0.75 10
FunctionMinimization SimpleGradientNum 0.01 1.0 1.0,0.0 1.0,0.0;0.0,1.0 -100 100 0.75 10
```

## Batch mode
Both - interactive and signle-run modes support `batch mode`. 
To activate it, follow the instructions is interactive mode, or set `<n>` greater than `1` in single-run mode 

## Tests
The project contains tests assuring nominal work of the algorithms. Run them using `test explorer` in Visual Studio 2019 
