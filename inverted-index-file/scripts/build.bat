@ECHO OFF

SET current_dir=%~dp0
SET src_dir=%current_dir%..\src\
SET build_dir=%current_dir%..\build\

SET executable_name=Program.exe

IF "%VARSINITIALIZED%"=="" CALL "%current_dir%vcvarsinit.bat"

IF NOT EXIST "%build_dir%" MKDIR "%build_dir%"
DEL /Q "%build_dir%*"

csc -out:"%build_dir%%executable_name%" -recurse:"%src_dir%*.cs"