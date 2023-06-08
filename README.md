# SVGtoICO

SVGtoICO converts .svg files to .ico files by creating intermediate 16x16, 32x32, 48x48, 128x128 and 256x256 px .bmp files which are later deleted.

## Building
Execute ```dotnet build --configuration Release``` which will build the project and generate .\SvgIcoConverter\bin\Release\net6.0\SvgIcoConverter.exe file

## Flags

| Flag | Comment                            | Example                    |
|------|------------------------------------|----------------------------|
| -f   | path to the .svg file (compulsory) | "-f C:\\path\to\file.svg"  |
| -d   | output directory                   | "-d C:\\optional\output"   |
| -i   | optional 16 px file                | "-i C:\\optional\file.png" |

## SvgIcoBatchConverter.ps1
This script recursively scans a selected folder and executes .exe file. It can be used with SvgIcoConverter.exe
