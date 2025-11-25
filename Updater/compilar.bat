@echo off
echo ========================================
echo Compilando UpdaterModerno.exe
echo ========================================

REM Buscar MSBuild en ubicaciones comunes
set MSBUILD_PATH=""

if exist "C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe" (
    set MSBUILD_PATH="C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe"
) else if exist "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" (
    set MSBUILD_PATH="C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"
) else if exist "C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe" (
    set MSBUILD_PATH="C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe"
) else if exist "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" (
    set MSBUILD_PATH="C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
) else (
    echo ERROR: No se encontro MSBuild
    echo Instala Visual Studio o usa Developer Command Prompt
    pause
    exit /b 1
)

echo Usando MSBuild: %MSBUILD_PATH%
echo.

REM Compilar el proyecto
%MSBUILD_PATH% UpdaterModerno.csproj /p:Configuration=Release /p:Platform="Any CPU"

if %ERRORLEVEL% == 0 (
    echo.
    echo ========================================
    echo COMPILACION EXITOSA!
    echo ========================================
    echo.
    echo Archivo generado: bin\Release\UpdaterModerno.exe
    echo.
    
    REM Copiar a la raiz de la aplicacion principal
    if exist "bin\Release\UpdaterModerno.exe" (
        copy "bin\Release\UpdaterModerno.exe" "..\UpdaterModerno.exe"
        echo Copiado a: ..\UpdaterModerno.exe
        echo.
        echo Ya puedes probar el sistema de actualizacion!
    )
) else (
    echo.
    echo ========================================
    echo ERROR EN COMPILACION
    echo ========================================
    echo Revisa los errores arriba
)

echo.
pause
