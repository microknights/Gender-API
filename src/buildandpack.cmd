@echo off
set version=1.4.0
set project=MicroKnights.Gender-API.csproj

dotnet clean -c Release %project%
dotnet build -c Release %project%
dotnet pack -c Release -o r:\nuget --version-suffix %version% %project%
