cd C:\Speakr.TalksApi\src\Speakr.TalksApi

# Get secrets from S3
C:\Windows\sysnative\WindowsPowerShell\v1.0\powershell.exe -Command 
{
	Set-ExecutionPolicy RemoteSigned; 
	Import-Module AWSPowerShell; 
	Read-S3Object -BucketName speakr-ec2secrets -Key auth0.json -File auth0.json
}

# Restore the nuget references
& "C:\Program Files\dotnet\dotnet.exe" restore

# Publish application with all of its dependencies and runtime for IIS to use
& "C:\Program Files\dotnet\dotnet.exe" publish --framework netcoreapp1.1 --configuration Release -o c:\Speakr.TalksApi\publish


# Point IIS wwwroot of the published folder. CodeDeploy uses 32 bit version of PowerShell.
# To make use the IIS PowerShell CmdLets we need call the 64 bit version of PowerShell.
C:\Windows\sysnative\WindowsPowerShell\v1.0\powershell.exe -Command {Import-Module WebAdministration; Set-ItemProperty 'IIS:\AppPools\DefaultAppPool' -Name managedRuntimeVersion -Value ""}
C:\Windows\sysnative\WindowsPowerShell\v1.0\powershell.exe -Command {Import-Module WebAdministration; Set-ItemProperty 'IIS:\sites\Default Web Site' -Name physicalPath -Value c:\Speakr.TalksApi\publish}

# The AppPool connections will not be set properly if IIS is not reset:
iisreset
