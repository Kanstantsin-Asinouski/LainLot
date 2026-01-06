# LainLot
That repository has 4 back-end (BE) and 1 front-end (FE) projects:
* DatabaseProvider (BE);
* DatabaseRepository (BE);
* NUnit Tests (BE);
* RestAPI (BE);
* AdminPanel (FE);

What should you do to install and run project:

* Install GIT on Windows (download from official git [webwite](https://git-scm.com/downloads));
* Install IIS on Windows (windows features and restart PC);
* Install Postman (download from official postman [website](https://www.postman.com/downloads/));
* Install latest version of .Net SDK (downlod from official microsoft [website](https://dotnet.microsoft.com/en-us/download/dotnet) and restart PC);
* Install latest version of .Net Hosting Bundle (download from official microsoft [website](https://dotnet.microsoft.com/en-us/download/dotnet) and restart PC);
* Install latest version of PostgreSQL plus PGAdmin4 (download from official postrgres [website](https://www.postgresql.org/download/windows/));
* Expand database using base code from sql file;
* Insert data to the next tables: AccessLevels, UserRoles and Users;
* Prepare folders on the PC to work with project, create three parent folders: Database, LainLot, Publish in the same folder;
* In Publish folder, create subfolder: RestAPI;
* Pull LainLot project from repos to LainLot folder;
* Open VS, rebuild solution, publish RestAPI;
* Copy all files from publish folder and put into Publish->RestAPI folder on your PC;
* Create new website in IIS: port - 8040, name - LainLot, physical path -  path to your Publish->RestAPI folder;
* Test RestAPI using postman;
* Open VS Code and run project;

Congrat! You is run LainLot project!
