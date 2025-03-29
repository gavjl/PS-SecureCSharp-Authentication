

# ## ensure you:
# #   1 - Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
# #   2 - run as administrator
# #

$rootCaCrtPath = "C:\rootCA.WiredBrainCoffee.crt"
$rootCaPfxPath = "C:\rootCA.WiredBrainCoffee.pfx"
$clientCrtPath = "C:\client1.WiredBrainCoffee.crt"
$clientPfxPath = "C:\client1.WiredBrainCoffee.pfx"
$rootCaDnsName = "localhost"
$clientDnsName = "client.localhost"

# create the root CA - note -DnsName must be the domain you're making calls to, for dev it's "localhost"
$rootCert = New-SelfSignedCertificate -CertStoreLocation "cert:\CurrentUser\My" -DnsName $rootCaDnsName -TextExtension @("2.5.29.19={text}CA=true") -KeyUsage CertSign,CrlSign,DigitalSignature

# Set a password and export as a public and private key
[System.Security.SecureString]$rootCertPassword = ConvertTo-SecureString -String "AStrongPassword!" -Force -AsPlainText
[String]$rootCertPath = Join-Path -Path "cert:\CurrentUser\My\" -ChildPath "$($rootCert.Thumbprint)"
Export-PfxCertificate -Cert $rootCertPath -FilePath $rootCaPfxPath -Password $rootCertPassword
Export-Certificate -Cert $rootCertPath -FilePath $rootCaCrtPath

# import the root CA
Import-Certificate -FilePath $rootCaCrtPath -CertStoreLocation "cert:\LocalMachine\Root"

# Create a client certificate, signed by the root CA
# Ensure the -DnsName does NOT match the -DnsName for the root CA. If it does it'll be seen as self signed and won't work
$clientCert = New-SelfSignedCertificate -CertStoreLocation "cert:\CurrentUser\My\" -DnsName $clientDnsName -KeyExportPolicy Exportable -KeyLength 2048 -KeyUsage DigitalSignature,KeyEncipherment -Signer $rootCert

# Export the public and private version of this key
[System.Security.SecureString]$clientCertPassword = ConvertTo-SecureString -String "ADifferentStrongPassword!" -Force -AsPlainText
[String]$clientCertPath = Join-Path -Path "cert:\CurrentUser\My\" -ChildPath "$($clientCert.Thumbprint)"
Export-PfxCertificate -Cert $clientCertPath -FilePath $clientPfxPath -Password $clientCertPassword
Export-Certificate -Cert $clientCertPath -FilePath $clientCrtPath

# Remove the certs from the cert:\CurrentUser\My\ location now they've been exported
Remove-Item -Path $rootCertPath -Force
Remove-Item -Path $clientCertPath -Force
