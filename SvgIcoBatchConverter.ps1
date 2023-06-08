$inputFolderPath = "\\K2dwp07fis004\topas\libra\vbut\svg_icons"  
$outputFolderPath = "\\K2dwp07fis004\topas\libra\vbut\svg_icons\out"
$input16pxFile = "C:\Users\dominykas.gudavicius\Desktop\New_folder\LC_logo_16.png"

$executablePath = "C:\Users\dominykas.gudavicius\source\repos\SvgIcoConverter\SvgIcoConverter\bin\Release\net6.0\SvgIcoConverter.exe"  

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
