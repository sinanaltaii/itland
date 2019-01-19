[CmdletBinding()]
param()
$Bindings = "http:/*:80:{0}"
$AvailableBindings = @("itland.local");
$ScriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent
$IisUserCredential = $null
$IisUser = "dev"
$SiteName = "itland.local"
$WebRootPath = [System.IO.Path]::GetFullPath((Join-Path $ScriptDir "../ITLand.Web"))
$ExitScriptNo = 2

while (-not ($Step -eq $ExitScriptNo) -or ($null -eq $Step)) {
    Write-Host "`r`nAvailable options:`r`n" -ForegroundColor Cyan
    Write-Host "1. Install IIS site, app pool and hosts entry:" -ForegroundColor Cyan
    Write-Host "2. Recycle app pool:" -ForegroundColor Gray
    Write-Host "3. Exit" -ForegroundColor White

    try {
        $Step = [int](Read-Host "Please choose option")
    }
    catch {
        $Step = [int]0
    }

    if ($Step -eq 1) {

        if (-not(Get-Module -Name "Carbon")) {
            Write-Verbose "Carbon module not found, importing now"
            & (Join-Path -Path $ScriptDir -ChildPath Carbon\Import-Carbon.ps1 -Resolve)
        }
    
        if ($null -eq $IisUserCredential) {
            Write-Verbose "Getting IIS user credentials"
            $IisUserCredential = Get-Credential ($IisUser)
        }

        Write-Verbose "Installing App pool"
        Install-IisAppPool -Name $SiteName -Credential $IisUserCredential
        
        $BindingList = @()
        foreach ($Site in $AvailableBindings) {
            $BindingList += $Bindings -f $Site
        }

        Write-Verbose "Installing IIS site $WebRootPath"
        Install-IisWebsite -Name $SiteName -PhysicalPath $WebRootPath -Bindings $BindingList -AppPoolName $SiteName
        
        Write-Verbose "Recycling App Pool"
        $AppPool = Get-IisAppPool -Name $SiteName
        $AppPool.Recycle()

        Write-Verbose "Adding host entries to hosts file"
        foreach ($Address in $AvailableBindings) {
            Write-Verbose "Adding $address to hosts file"
            Set-HostsEntry -IPAddress 127.0.0.1 -HostName $Address
        }

        Write-Verbose "Starting web site $SiteName"
        $WebSite = Get-IisWebsite -Name $SiteName
        $WebSite.Start()
    }
    
    if ($Step -eq 2) {
        if (-not(Get-Module -Name "WebAdministration")) {
            Write-Verbose "WebAdministration not found, importing now"
            Import-Module "WebAdministration"
        }
        Write-Verbose "Recycling App Pool"
        $AppPool = (Get-Item "IIS:\Sites\itland.local" | Select-Object ApplicationPool).ApplicationPool
        Restart-WebAppPool $AppPool
    }

    if ($Step -eq 3) {
        Write-Host "`nBye" -ForegroundColor Cyan
    }

    if([string]::IsNullOrWhiteSpace($Step) -or ($Step -lt 1) -or -not ($Step -lt ($ExitScriptNo + 1))) {
        Write-Host "`r`nPlease enter a valid option" -ForegroundColor "Red"
    }
}