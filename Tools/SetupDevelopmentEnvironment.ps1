[CmdletBinding()]
param()
$ScriptDir = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent
$ConfigFilePath = [System.IO.Path]::Combine($PSScriptRoot, "config.json")
$AppPoolCredentials = $null
$ExitScriptNo = 3

Write-Host "Preparing dependencies needed to run script"
if (-not(Get-Module -Name "Carbon")) {
    Write-Verbose "Carbon module not found, importing now"
    & (Join-Path -Path $ScriptDir -ChildPath Carbon\Import-Carbon.ps1 -Resolve)
}

try {
    Write-Host "Reading configuration file..."
    $Config = Get-Content -Raw -Path $ConfigFilePath | ConvertFrom-Json
}
catch {
}

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
        try {
            foreach ($site in $Config.sites) {
                if ($null -eq $AppPoolCredentials) {
                    Write-Verbose "Getting AppPool user credentials"
                    $User = $site.appPoolUsername
                    $Password = ConvertTo-SecureString -String $site.appPoolPassword -AsPlainText -Force
                    $AppPoolCredentials = New-Object -TypeName System.Management.Automation.PSCredential -ArgumentList $User, $Password
                    Write-Verbose "Installing App pool"
                    Install-IisAppPool -Name $site.name -Credential $AppPoolCredentials
                }
                
                $BindingList = @()
                foreach ($binding in $site.bindings) {
                    Write-Host "Adding $binding..."
                    $BindingList += [string]::Format("http/*:80:{0}", $binding)
                }
                try {
                    Write-Host "Installing IIS site $site.rootPath"
                    Install-IisWebsite -Name $site.name -PhysicalPath $site.rootPath -Bindings $BindingList -AppPoolName $site.name
                }
                catch {
                }
                
                Write-Verbose "Recycling App Pool"
                $AppPool = Get-IisAppPool -Name $site.name
                $AppPool.Recycle()
                
                foreach ($Address in $site.bindings) {
                    Write-Verbose "Adding host entries to hosts file"
                    Write-Verbose "Adding $ $site.binding to hosts file"
                    Set-HostsEntry -IPAddress 127.0.0.1 -HostName $Address
                }
        
                Write-Verbose "Starting web site $site.name"
                $WebSite = Get-IisWebsite -Name $site.name
                $WebSite.Start()
            }
        }
        catch {
        }
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

    if ([string]::IsNullOrWhiteSpace($Step) -or ($Step -lt 1) -or -not ($Step -lt ($ExitScriptNo + 1))) {
        Write-Host "`r`nPlease enter a valid option" -ForegroundColor "Red"
    }
}