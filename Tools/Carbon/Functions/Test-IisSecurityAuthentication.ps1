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

function Test-CIisSecurityAuthentication
{
    <#
    .SYNOPSIS
    Tests if IIS authentication types are enabled or disabled on a site and/or virtual directory under that site.
    
    .DESCRIPTION
    You can check if anonymous, basic, or Windows authentication are enabled.  There are switches for each authentication type.
    
    Beginning with Carbon 2.0.1, this function is available only if IIS is installed.

    .OUTPUTS
    System.Boolean.
    
    .EXAMPLE
    Test-CIisSecurityAuthentication -SiteName Peanuts -Anonymous
    
    Returns `true` if anonymous authentication is enabled for the `Peanuts` site.  `False` if it isn't.
    
    .EXAMPLE
    Test-CIisSecurityAuthentication -SiteName Peanuts -VirtualPath Doghouse -Basic
    
    Returns `true` if basic authentication is enabled for`Doghouse` directory under  the `Peanuts` site.  `False` if it isn't.
    #>
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true)]
        [string]
        # The site where anonymous authentication should be set.
        $SiteName,
        
        [Alias('Path')]
        [string]
        # The optional path where anonymous authentication should be set.
        $VirtualPath = '',
        
        [Parameter(Mandatory=$true,ParameterSetName='Anonymous')]
        [Switch]
        # Tests if anonymous authentication is enabled.
        $Anonymous,
        
        [Parameter(Mandatory=$true,ParameterSetName='Basic')]
        [Switch]
        # Tests if basic authentication is enabled.
        $Basic,
        
        [Parameter(Mandatory=$true,ParameterSetName='Digest')]
        [Switch]
        # Tests if digest authentication is enabled.
        $Digest,
        
        [Parameter(Mandatory=$true,ParameterSetName='Windows')]
        [Switch]
        # Tests if Windows authentication is enabled.
        $Windows
    )

    Set-StrictMode -Version 'Latest'

    Use-CallerPreference -Cmdlet $PSCmdlet -Session $ExecutionContext.SessionState
    
    $getConfigArgs = @{ $pscmdlet.ParameterSetName = $true }
    $authSettings = Get-CIisSecurityAuthentication -SiteName $SiteName -VirtualPath $VirtualPath @getConfigArgs
    return ($authSettings.GetAttributeValue('enabled') -eq 'true')
}

