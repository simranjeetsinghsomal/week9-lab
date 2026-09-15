$ErrorActionPreference = 'Stop'
$compiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
foreach ($name in @('NonMVC_Calculator', 'MVC_Calculator')) {
    $folder = Join-Path $PSScriptRoot $name
    New-Item -ItemType Directory -Force -Path "$folder\bin\Debug" | Out-Null
    $sources = Get-ChildItem -LiteralPath $folder -Filter *.cs | Select-Object -ExpandProperty FullName
    & $compiler /nologo /target:winexe "/out:$folder\bin\Debug\$name.exe" /reference:System.dll /reference:System.Drawing.dll /reference:System.Windows.Forms.dll $sources
    if ($LASTEXITCODE -ne 0) { throw "Build failed: $name" }
    Write-Output "Built $name"
}
