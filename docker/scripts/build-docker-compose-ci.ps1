#This PowerShell script runs on the Docker-Container-CI build agent, 
# built by Dockerfile.build-server

$BUILD_BUILDID = (Get-ChildItem Env:BUILD_BUILDID).Value
$BUILD_ARTIFACTSTAGINGDIRECTORY = (Get-ChildItem Env:BUILD_ARTIFACTSTAGINGDIRECTORY).Value
Write-Host "BuildId is $BUILD_BUILDID"

$od = Get-DockerComposeFile ./docker-compose.yml

# The Yaml reader thinks the version is a decimal - this fixes that
$od["version"] = $od.version.ToString()

$od.ReplaceImage("initiatives-vue", "coeoctava.azurecr.io/initiatives-vue:dev-1.0.$BUILD_BUILDID")
$od.ReplaceImage("initiatives-webapi", "coeoctava.azurecr.io/initiatives-webapi:v1.0.$BUILD_BUILDID")
$od.ReplaceImage("nginx", "coeoctava.azurecr.io/nginx:v1.0.$BUILD_BUILDID")

foreach ($svcName in $od.services.Keys)
{
  $svc = $od.services[$svcName]

  #Remove all exposed ports
  $svc.Remove("ports")

  #remove all volume, as they could interfere with multiple running instances
  $svc.Remove("volumes")  

  #remove the "restart" section if it exists, since if there are errors
  #we don't need to waste resources constantly restarting
  $svc.Remove("restart")
}

#Add ports back somes images to allow us to map to a host port
$od.services.nginx["ports"] = @('${PORT}:80')
$od.services.'wordpress-db'["ports"] = @('${MYSQL_PORT}:3306')
$od.services.'initiatives-db'["ports"] = @('${MSSQL_PORT}:1433')

# Declare the OCTAVA_URL environment variable so we can set it on startup
$od.services.'wordpress-db'.environment["OCTAVA_URL"] = '${OCTAVA_URL}'
#NOTE: environment won't exist on the NGINX image!
$nginxEnv = New-Object System.Collections.Specialized.OrderedDictionary
$nginxEnv["OCTAVA_URL"] = '${OCTAVA_URL}'
$od.services.nginx["environment"]  = $nginxEnv

# Finally, we can remove the root "volumes" section since we won't have any left here
$od.Remove("volumes")

Write-Host "docker-compose:"
$od.ToString()

Write-Host "Saving docker-compose to $BUILD_ARTIFACTSTAGINGDIRECTORY"

$od.ToString() > $BUILD_ARTIFACTSTAGINGDIRECTORY/docker-compose.yml