Param
(
	[Parameter(Mandatory, Position=0, HelpMessage="The connection string to the DB to create the encryption keys")]
    [string] $DbConnectionString,
    [Parameter(Mandatory, Position=1, HelpMessage="The app servers certificate thumbprint that will be used to create the encryption master key")]
    [string] $CertificateThumbprint
)


Function Create-InpatientEncryptionKeys() {
	param( [string]$connectionString, [string]$certificateThumbprint)
	Set-ExecutionPolicy Unrestricted -Force

	try {
		# Import the SQL Server Module.  
		Import-Module SqlServer

		# Connect to your database
		$database = Get-SqlDatabase -ConnectionString $connectionString

		#List the keys
		$cmks = Get-SqlColumnMasterKey -InputObject $database | Select-Object -ExpandProperty "Name"
		Write-Host "Current SqlColumnMasterKeys: $cmks"
		$ceks = Get-SqlColumnEncryptionKey -InputObject $database | Select-Object -ExpandProperty "Name"
		Write-Host "Current SqlColumnEncryptionKeys: $ceks"

		$cmkName = "EStoreColumnMasterKey"

		if($cmks -and $cmks.Contains($cmkName)) {
			Write-Host "$cmkName already exists. Exiting... "
			Return;
		}
		
		Write-Host "Using certificate with thumbprint $certificateThumbprint to generate keys"
		
		# Create a SqlColumnMasterKeySettings object for your column master key. 
		$cmkSettings = New-SqlCertificateStoreColumnMasterKeySettings -CertificateStoreLocation "LocalMachine" -Thumbprint $certificateThumbprint

		$cmk = New-SqlColumnMasterKey -Name $cmkName -InputObject $database -ColumnMasterKeySettings $cmkSettings

		$cekName = "EStoreColumnEncryptedKey"
		$cek = New-SqlColumnEncryptionKey -Name $cekName  -InputObject $database -ColumnMasterKey $cmkName

		#List the keys
		$cmks = $cmks = Get-SqlColumnMasterKey -InputObject $database | Select-Object -ExpandProperty "Name"
		Write-Host "Updated SqlColumnMasterKeys: $cmks"
		$ceks = Get-SqlColumnEncryptionKey -InputObject $database | Select-Object -ExpandProperty "Name"
		Write-Host "Updated SqlColumnEncryptionKeys: $ceks"
	}
	catch {
		Write-Host "An error occurred. Make sure you have updated InPatient Gen3 Prerequisites DSC Modules to version 2020.9.24.1 or higher. Also at least SQL Server 2016 SP1 is required"
		
		Write-Error $_
	}
	
}


Create-InpatientEncryptionKeys $DbConnectionString $CertificateThumbprint