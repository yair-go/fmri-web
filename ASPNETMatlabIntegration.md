# Required setting under IIS machine #
  * `libeng.dll` directory should be in system path (usually `c:\Program Files\MatLab\bin\win32`)
  * ASPNET user should have enough privileges:
    * Add to Administrators group
    * Remove its restrictions under `secpol.msc > Local Policies > User Rights Assignment`
      * `Deny Logon Locally`
      * `Deny log on through Terminal Services`

# ASP.NET application #
  * .NET Framework 2.0