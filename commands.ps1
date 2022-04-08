docker run --name testwc -p 82:80 -d mcr.microsoft.com/dotnet/framework/wcf:4.8-windowsservercore-ltsc2019
docker run --name testwcf -p 82:80 -d wcf:latest

# TRuublehsoting
 get-item "IIS:/sites/Default web site"
 Get-ItemProperty "IIS:/sites/Default web site/InternalService" -name enabledprotocols