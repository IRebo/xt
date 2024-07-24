$aa=New-SelfSignedCertificate -Type Custom -Subject "CN=hoe" -KeyUsage DigitalSignature -FriendlyName "hoe" -CertStoreLocation "Cert:\CurrentUser\My" -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.3", "2.5.29.19={text}") -NotAfter (Get-Date).AddYears(20) -KeyAlgorithm 'RSA' -KeyLength 2048
echo $aa
$fp=$aa.Thumbprint
$password = ConvertTo-SecureString -String xtrancetest -Force -AsPlainText 
Export-PfxCertificate -cert "Cert:\CurrentUser\My\$fp" -FilePath SigningCertificate.pfx -Password $password
$pfx_cert = Get-Content '.\SigningCertificate.pfx' -Encoding Byte
[System.Convert]::ToBase64String($pfx_cert) | Out-File 'SigningCertificate_Encoded.txt'
