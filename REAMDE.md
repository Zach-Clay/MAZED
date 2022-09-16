# Introduction 
MAZED Team Good Driver Rewards Program for CPSC4910

# Technologies
Frontend: AngularJS, Bootstrap, Angular Material
Backend: ASP.NET Web API's, C#
Database: AWS RDS

# Git protocols

## Committing

1. I am big believer of committing early and often, that way if something goes wrong in your code, it is easy to revert to a stable commit and pinpoint the error. 
2. To commit your changes in Visual Studio Code:
   1. First, make sure your repo is connected the Azure branches and that you are in the correct branch
      1. DO NOT COMMIT TO MAIN!!!!!!
   2. Second, select the Source Control tab on the left-hand menu 
      1. Icon looks like this ![](images/source_control.png)
   3. In this view, you should be able to see all of your changes since your last commit
       ![](images/Staged_changes.png)
      1. Hover over the desired file to stage and a `+` should appear
      2. Click the plus button to stage the change
         1. This means you intentionally select what changes to apply (like me staging this readme)
      3. Once you have staged all your files, hit commit!
         1. This only actually updates your branch locally, not online, so to get the changes into your Azure branch we have to push your changes.....

