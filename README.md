# f14.AutoVersion
This is small .Net Core CLI tool for auto-update build version of your project.

##CLI
Command name: `dotnet autover`.
Params:
- `-backup` or short name `-b`: set this if you need to create backup of current project.json. File created each tool run.
- `-template` or short name `-t` and *MUST BE SET AFTER PARAM* value `1.0.0-alpha-{00000}`. The variable part is enclosed in a brace.
  - Sample template-1. Template: `1.0.0-alpha-{00000}` Result: n1(`1.0.0-alpha-00001`), n2(`1.0.0-alpha-00002`),n3(`1.0.0-alpha-00003`),n100(`1.0.0-alpha-00100`)
  - Sample template-2. Template: `1.0.0-beta{0}` Result: n1(`1.0.0-beta1`), n2(`1.0.0-beta2`),n3(`1.0.0-beta3`),n100(`1.0.0-beta100`)

## NuGet
[NuGet](https://www.nuget.org/packages/f14.AutoVersion)

## How To Use
### project.json
Add to the tools section:
```json
"tools": {
    "f14.AutoVersion": "1.0.0"
  }
```
If your project is not *netcoreapp* targeted, add imports.

Sample for *netstandard1.6*:
```json
"dependencies": {
    "NETStandard.Library": "1.6.0"
  },
  "tools": {
    "f14.AutoVersion": "1.0.0"      <-- Add tool
  },
  "frameworks": {
    "netstandard1.6": {
      "imports": [
        "dnxcore50",
        "netcoreapp1.0"             <-- Add netcoreapp1 to import of project frameworks
      ]
    }
  }
```
#### Scripts
To change version each time when you compile project add next (setup you format first ;)):
```json
  "scripts": {
    "precompile": "dotnet autover -b -t 1.0.0-alpha-{00000}"
  }
```
