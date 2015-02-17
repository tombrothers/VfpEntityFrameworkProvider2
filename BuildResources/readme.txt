VfpEntityFrameworkProvider.dll has been added to your project.

-- Requirement
You need to have VfpOleDb installed for VfpEntityFrameworkProvider to work.  

-- In package directory
* VfpOleDbSetup.msi - for you to install if you don't already have it installed.
* VfpEntityFrameworkProvider.msi   
  - Installs VfpOleDb
  - Installs VfpEntityFrameworkProvider.dll in the GAC 
  - Adds registry settings to integrate VfpEntityFrameworkProvider with Visual Studio (need this to work with the Entity Framework Designer)


----------------------------------------------------
Released to the public domain, use at your own risk.

-----------------------------
VFP Entity Framework Provider
https://github.com/tombrothers/VfpEntityFrameworkProvider2
http://randomdevnotes.com/tag/vfp-ef-provider/
Tom Brothers (tombrothers@outlook.com)