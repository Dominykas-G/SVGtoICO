# This script recursively scans a selected folder and converts all .svg files to an .ico
# input folder and executable path is compulsory

$inputFolderPath = "C:\Users\user\input_folder"  
$outputFolderPath = "C:\Users\user\output_folder"
$input16pxFile = "C:\Users\user\file.png"

$executablePath = "C:\Users\user\path\SvgIcoConverter.exe"  

Get-ChildItem -Path $inputFolderPath -Filter "*.svg" -Recurse | ForEach-Object {
    $fileName = $_.Name
    $fullName = $_.FullName 

    try {
        Write-Host "Starting with $fileName"
        & $executablePath "-f $fullName" "-d $outputFolderPath" "-i $input16pxFile"        
    }
    catch {
        Write-Host -ForegroundColor Red $_.Exception.Message
    }
}

Write-Host -ForegroundColor Green "Done"
