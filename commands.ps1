# Build container 
## Dev
docker build -t wcf -f Dockerfile.combined.dev .

# Run container
docker run --name testwcf -p 82:80 -d wcf:latest

# TRoublehsoting

## inside docker container
docker exec -it testwcf powershell

# somehow the below command is not working when run from Dockerfile. So need to import during debugging.
import-module WebAdministration
get-item "IIS:/sites/Default web site"
Get-ItemProperty "IIS:/sites/Default web site/InternalService" -name enabledprotocols