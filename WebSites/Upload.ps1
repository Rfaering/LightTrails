#Login-AzureRmAccount

$appdirectory="."
$webappname="LightTrails"
$resourceGroupName="LightTrails"
$zipFile = Get-ChildItem ".\Web.config"

# Get publishing profile for the web app
[xml]$xml = (Get-AzureRmWebAppPublishingProfile -Name $webappname `
-ResourceGroupName LightTrails `
-OutputFile null)

# Extract connection information from publishing profile
$username = $xml.SelectNodes("//publishProfile[@publishMethod=`"FTP`"]/@userName").value
$password = $xml.SelectNodes("//publishProfile[@publishMethod=`"FTP`"]/@userPWD").value
$url = $xml.SelectNodes("//publishProfile[@publishMethod=`"FTP`"]/@publishUrl").value

# Upload files recursively 
Set-Location $appdirectory
$webclient = New-Object -TypeName System.Net.WebClient
$webclient.Credentials = New-Object System.Net.NetworkCredential($username,$password)


$relativepath = (Resolve-Path -Path $zipFile.FullName -Relative).Replace(".\", "").Replace('\', '/')
$uri = New-Object System.Uri("$url/$relativepath")
"Uploading to " + $uri.AbsoluteUri
$webclient.UploadFile($uri, $zipFile.FullName)


$webclient.Dispose()