# Introduction 
MAZED Team Good Driver Rewards Program for CPSC4910

# Technologies
Frontend: AngularJS, Bootstrap, Angular Material
Backend: ASP.NET Web API's, C#
Database: AWS RDS

# Committing

1. I am big believer of committing early and often, that way if something goes wrong in your code, it is easy to revert to a stable commit and pinpoint the error. 
2. To commit your changes in Visual Studio Code:
   1. First, make sure your repo is connected the Azure branches and that you are in the correct branch
      1. DO NOT COMMIT TO MAIN!!!!!!
   2. Second, select the Source Control tab on the left-hand menu 
   3. In this view, you should be able to see all of your changes since your last commit
       ![](images/Staged_changes.png)
      1. Hover over the desired file to stage and a `+` should appear
      2. Click the plus button to stage the change
         1. This means you intentionally select what changes to apply (like me staging this readme)
      3. Once you have staged all your files, hit commit!
         1. This only actually updates your branch locally, not online, so to get the changes into your Azure branch we have to push your changes.....
### NOTE: Make sure your commits have meaningful commit messages. This is so that when other teammates look at your branches/commits, they will be able to quickly pick up what exactly each commit achieved.

# Pushing and Pulling
### Pushing your changes 
   ![](images/Push_pull.png)
   1. Once you have made a commit, you will see a down arrow and an up arrow. These represent the number of incoming commits (from other teammates) and outgoing commits (made by you), respectively. They also allow other to incrementally see the changes you have made.
   2. As seen in the picture, I have one outgoing commit that I need to push onto the online branch so that is visible to everyone else. 
   3. In VSCode, there are two easy ways to do this:
      1. Click on the three dots in the Source Control pane (See previous image) where dropdown menu will appear for you to select push, OR
      2. Click on the up/down arrows themselves. This action will also pull changes from your branch as well.
    4. It is up to you when push your changes, as long as you commit regularly.

### Pulling changes
   1. Pulling is when you bring a teammate's changes/commits into your own branch, updating the code. 
   2. When to pull: 
      1. When you are working with another teammate in the same branch. 
         1. Pull often to avoid stepping on each other's code
      2. When you are starting to work a new feature/branch
         1. Always pull from main if you are doing this (will be explained again in branch protocols)
   

