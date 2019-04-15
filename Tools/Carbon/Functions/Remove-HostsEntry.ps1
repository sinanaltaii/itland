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

function Remove-CHostsEntry
{
    <#
    .SYNOPSIS
    Removes entries from the hosts file by hostname.

    .DESCRIPTION
    You can remove multiple entries or pipe entries into this function.

    .EXAMPLE
    Remove-CHostsEntry -HostName 'adadvisor.net'

    Demonstrates how to remove hosts entry for `adadvisor.net`, which you probably don't want to do.

    .EXAMPLE
    Remove-CHostsEntry -HostName 'adadvisor.net','www.adchimp.com'

    Demonstrates how to remove multiple hosts entries.

    .EXAMPLE
    ('adadvisor.net','www.adchimp.com') | Remove-CHostsEntry

    Demonstrates how to pipe hostnames into `Remove-CHostsEntry`.

    .EXAMPLE
    Remove-CHostsEntry -HostName 'adadvisor.net' -Path 'C:\Projects\Carbon\adblockhosts'

    Demonstrates how to work with a file other than Windows' default hosts file.
    #>
    [CmdletBinding(SupportsShouldProcess=$true)]
    param(
        [Parameter(Mandatory=$true,ValueFromPipeline=$true,Position=0)]
        [string[]]
        # The hostname of the hosts entry/entries to remove.
        $HostName,

        [string]
        # The hosts file to modify.  Defaults to the Windows hosts file.
        $Path = (Get-CPathToHostsFile)
    )

    begin
    {
        Set-StrictMode -Version 'Latest'

        Use-CallerPreference -Cmdlet $PSCmdlet -Session $ExecutionContext.SessionState

        $allHostNames = New-Object 'Collections.ArrayList'
    }

    process
    {
        $HostName | 
            ForEach-Object { [Text.RegularExpressions.Regex]::Escape( $_ ) } |
            ForEach-Object { [void] $allHostNames.Add( $_ ) }
    }

    end
    {
        $regex = $allHostNames -join '|'
        $regex = '^[0-9a-f.:]+\s+\b({0})\b.*$' -f $regex 

        $cmdErrors = @()
        $newHostsFile = Read-CFile -Path $Path -ErrorVariable 'cmdErrors' |
                            Where-Object { $_ -notmatch $regex }
        if( $cmdErrors )
        {
            return
        }

        $entryNoun = 'entry'
        if( $HostName.Count -gt 1 )
        {
            $entryNoun = 'entries'
        }

        if( $PSCmdlet.ShouldProcess( $Path, ('removing hosts {0} {1}' -f $entryNoun,($HostName -join ', ')) ) )
        {
            $newHostsFile | Write-CFile -Path $Path
        }
    }
}
