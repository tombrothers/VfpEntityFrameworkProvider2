
setlocal
cd..
cd..

copy /y "%CD%\Source\Resources\net40\VfpClient*.dll" %CD%\Source\VfpEntityFrameworkProvider.Setup\Resources\net40
copy /y "%CD%\Source\Resources\net45\VfpClient*.dll" %CD%\Source\VfpEntityFrameworkProvider.Setup\Resources\net45
endlocal