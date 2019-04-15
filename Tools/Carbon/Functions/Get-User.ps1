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

function Get-CUser
{
    <#
    .SYNOPSIS
    Gets *local* users.

    .DESCRIPTION
    `Get-CUser` gets all *local* users. Use the `UserName` parameter to get  a specific user by its username.

    The objects returned by `Get-CUser` are instances of `System.DirectoryServices.AccountManagement.UserPrincipal`. These objects use external resources, which, if they are disposed of correctly, will cause memory leaks. When you're done using the objects returne by `Get-CUser`, call `Dispose()` on each one to clean up its external resources.

    `Get-CUser` is new in Carbon 2.0.

    .OUTPUTS
    System.DirectoryServices.AccountManagement.UserPrincipal.

    .LINK
    Install-CUser

    .LINK
    Test-CUser

    .LINK
    Uninstall-CUser

    .EXAMPLE
    Get-CUser

    Demonstrates how to get all local users.

    .EXAMPLE
    Get-CUser -Username LSkywalker 

    Demonstrates how to get a specific user.
    #>
    [CmdletBinding()]
    [OutputType([System.DirectoryServices.AccountManagement.UserPrincipal])]
    param(
        [ValidateLength(1,20)]
        [string]
        # The username for the user.
        $UserName 
    )

    Set-StrictMode -Version 'Latest'
    Use-CallerPreference -Cmdlet $PSCmdlet -Session $ExecutionContext.SessionState
    
    $ctx = New-Object 'DirectoryServices.AccountManagement.PrincipalContext' ([DirectoryServices.AccountManagement.ContextType]::Machine)
    if( $Username )
    {
        $userToFind = New-Object 'DirectoryServices.AccountManagement.UserPrincipal' $ctx
        $userToFind.SamAccountName = $UserName
        $searcher = New-Object 'DirectoryServices.AccountManagement.PrincipalSearcher' $userToFind
        $user = $searcher.FindOne()
        if( -not $user )
        {
            # Fallback. PrincipalSearch can't find some users.
            $ctx.Dispose()
            $ctx = New-Object 'DirectoryServices.AccountManagement.PrincipalContext' ([DirectoryServices.AccountManagement.ContextType]::Machine)
            $user = [DirectoryServices.AccountManagement.UserPrincipal]::FindByIdentity( $ctx, $Username )
            if( -not $user )
            {
                Write-Error ('Local user "{0}" not found.' -f $Username) -ErrorAction:$ErrorActionPreference
                return
            }
        }
        return $user
    }
    else
    {
        $query = New-Object 'DirectoryServices.AccountManagement.UserPrincipal' $ctx
        $searcher = New-Object 'DirectoryServices.AccountManagement.PrincipalSearcher' $query
        try
        {
            $searcher.FindAll() 
        }
        finally
        {
            $searcher.Dispose()
            $query.Dispose()
        }
    }
}