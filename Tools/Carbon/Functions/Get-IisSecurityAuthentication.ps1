# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# 
#     http://www.apache.org/licenses/LICENSE-2.0
# 
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.

function Get-CIisSecurityAuthentication
{
    <#
    .SYNOPSIS
    Gets a site's (and optional sub-directory's) security authentication configuration section.
    
    .DESCRIPTION
    You can get the anonymous, basic, digest, and Windows authentication sections by using the `Anonymous`, `Basic`, `Digest`, or `Windows` switches, respectively.
    
    Beginning with Carbon 2.0.1, this function is available only if IIS is installed.

    .OUTPUTS
    Microsoft.Web.Administration.ConfigurationSection.
    
    .EXAMPLE
    Get-CIisSecurityAuthentication -SiteName Peanuts -Anonymous
    
    Gets the `Peanuts` site's anonymous authentication configuration section.
    
    .EXAMPLE
    Get-CIisSecurityAuthentication -SiteName Peanuts -VirtualPath Doghouse -Basic
    
    Gets the `Peanuts` site's `Doghouse` sub-directory's basic authentication configuration section.
    #>
    [CmdletBinding()]
    [OutputType([Microsoft.Web.Administration.ConfigurationSection])]
    param(
        [Parameter(Mandatory=$true)]
        [string]
        # The site where anonymous authentication should be set.
        $SiteName,
        
        [Alias('Path')]
        [string]
        # The optional path where anonymous authentication should be set.
        $VirtualPath = '',

        [Parameter(Mandatory=$true,ParameterSetName='anonymousAuthentication')]        
        [Switch]
        # Gets a site's (and optional sub-directory's) anonymous authentication configuration section.
        $Anonymous,
        
        [Parameter(Mandatory=$true,ParameterSetName='basicAuthentication')]        
        [Switch]
        # Gets a site's (and optional sub-directory's) basic authentication configuration section.
        $Basic,
        
        [Parameter(Mandatory=$true,ParameterSetName='digestAuthentication')]        
        [Switch]
        # Gets a site's (and optional sub-directory's) digest authentication configuration section.
        $Digest,
        
        [Parameter(Mandatory=$true,ParameterSetName='windowsAuthentication')]        
        [Switch]
        # Gets a site's (and optional sub-directory's) Windows authentication configuration section.
        $Windows
    )
    
    Set-StrictMode -Version 'Latest'

    Use-CallerPreference -Cmdlet $PSCmdlet -Session $ExecutionContext.SessionState

    $sectionPath = 'system.webServer/security/authentication/{0}' -f $pscmdlet.ParameterSetName
    Get-CIisConfigurationSection -SiteName $SiteName -VirtualPath $VirtualPath -SectionPath $sectionPath
}

