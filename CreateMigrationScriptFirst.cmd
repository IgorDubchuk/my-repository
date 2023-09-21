echo off
rem Название создаваемой EF Миграции
set migrationName=addDriverTable
rem Название создаваемого файла со скриптом SQL
set scriptFileName=V1_addDriverTable.sql
rem Название последней EF Миграции, от которой берутся изменения для генерации SQL скрипта
set lastMigrationName=addDriverTable

set projectsPath=%cd%
set entryProject="%projectsPath%\WebAPITest\WebAPITest.csproj"
set contextProject="%projectsPath%\WebAPITest\WebAPITest.csproj"
set migrationOutputPath="Infrastructure\Migrations"
set scriptOutputPath="%projectsPath%\WebAPITest\Infrastructure\Migrations\Scripts"
echo 1. Build EF Migration... Name: %migrationName%
dotnet ef migrations add --project %contextProject% --startup-project %entryProject% %migrationName% --output-dir %migrationOutputPath% --verbose
echo 2. Create SQL script file... Name: %scriptFileName%
dotnet ef migrations script --project %contextProject% --startup-project %entryProject% --idempotent -o %scriptOutputPath%/%scriptFileName%
pause