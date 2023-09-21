echo off
set projectsPath=%cd%
set entryProject="%projectsPath%\WebAPITest\WebAPITest.csproj"
set contextProject="%projectsPath%\WebAPITest\WebAPITest.csproj"
echo 1. Applying migration...
dotnet ef database update --project %contextProject%
pause