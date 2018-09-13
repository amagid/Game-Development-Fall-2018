# Contributing Guidelines
### Last Updated: 9/13/18 @ 3:21 PM
Hey, everyone! Since we've got a lot of people on this team, we need to work especially hard to make sure our Git repo stays organized. In that spirit, here are some important guidelines for contributing to our Git repository.

**Note: This file may be updated from time to time. If and when that happens, everyone will be notified via discord. If you're ever wondering whether or not the file has been updated, check the "Last Updated" label up at the top.**

----------
## Table Of Contents
#### *It's really more of a list than a table, but whatever*

* [Basics Of Git](#basics-of-git "For newbies, or if you just need a refresher")
* [GitHub For Unity](#github-for-unity "Make Unity do more things for you")
* [When To Commit](#when-to-commit "Early And Often")
* [Commit Message Style](#commit-messages "ConventionalCommits, with our own flare")
* [Before You Work Each Day](#before-you-work "Pull latest code and post to Scrum-Updates channel")
* [Branching](#branching "Always.")
* [Contribution Reviews](#contribution-review "Code can't enter the Master branch without review")

----------
<a name="basics-of-git"></a>
## Basics Of Git
#### *Probably the most helpful thing in this document*

So, you're new to Git (or just need a refresher)! Either way, no worries - Aaron's AI skills aren't quite strong enough to make this document judge you. At the most basic level, Git is a distributed version control system. That's a fancy way of saying it keeps track of your files in a *repository*, who edits them, what edits they make, and allows us to undo any set of changes we like at any time. GitHub, by contrast, is a separate service that hosts remote Git repositories for you - kind of like Google Drive for your code.

In Git, code changes are grouped into *commits*. Each commit stores a set of changes you've maded to the various files in the project as well as a message written by you about the commit which should explain what you changed about the project. It also knows who made the commit and when.

**Your code is not automatically sent to the rest of the group!** When you're ready to send your code to the group, you need to *push* your commits to the remote repository (hosted on GitHub). Similarly, if you want to get the latest changes from the group, you need to *pull* the latest commits down from the remote repository.

You can do all of these things once you [download Git](https://git-scm.com/downloads), optionally [download GitHub Desktop](https://desktop.github.com/), and set up [GitHub For Unity](#github-for-unity). While you don't technically need GitHub Desktop, if you don't get it you'll have to use your computer's command prompt / terminal in order to work with Git.

You can also create *branches*, which are essentially isolated groups of commits which other people won't see. You create branches when you start working on a larger task so that you can create multiple commits in a safe environment that can be instantly deleted if it doesn't work out. Our policy is to create a branch whenever you start work on a new task, using the naming format &lt;Type&gt;/&lt;FeatureName&gt;. The type can be the same as the types of commits, described in the [Commit Message Style section](#commit-messages). But as an example, if you're working on the jumping feature, you might name your branch *Feature/Jump*. Similarly, if you're fixing a bug where gravity doesn't work, you might name your branch *Fix/Gravity*. Our branching policy is described in more detail in the [Branching Section](#branching) below.

**BE CAREFUL** - you may have heard some team members talking about *Merge Conflicts*. Merge conflicts occur when you modify a file and then pull the latest changes down from the remote repository when someone else in the team has also modified that same file in a similar way. Git then doesn't know whose changes to use, because they conflict, so it uses both and sticks lines of crap in between to separate them. If the file is text, it's ugly. If the file is code, the code will no longer work. You then have to go directly into the files and resolve the conflict and then re-commit. As you can likely tell, merge conflicts are literally the worst thing in the world, and will ruin your day VERY quickly. They're so bad that there is an [entire section](#before-you-work) of this document that's basically devoted to avoiding merge conflicts. So, if you get an angry message from Git saying that there is a merge conflict, **contact Aaron, Dennis, or someone else who has lots of Git experience** and they will help you resolve it.

----------
<a name="github-for-unity"></a>
## GitHub For Unity
#### *Automation at its finest*

Unity has a wonderful plugin that integrates GitHub directly into Unity. To download it, go to [https://unity.github.com/] and click the big, pretty "download" button. Once the plugin has been downloaded, open Unity. Once Unity is open with a project open, run the plugin. It will tell you that it needs to be updated. Let it update, and just click through to let it do what it needs. Afterward, you should see a GitHub tab on the right side of the Unity window. Follow Dennis's tutorial to finish setting up the plugin.

Once the plugin is set up with the remote repository (https://github.com/amagid/Game-Development-Fall-2018), you should be able to click a button in the plugin window to create a commit, and there should also be buttons for pushing and pulling. Commits require messages - our message format is described in the [Commit Message Style section](#commit-messages) below. 

----------
<a name="when-to-commit"></a>
## Commit Early And Often
#### *Less is more? Not here*

We can roll the project back to any commit at any time. So the more commits you have, the more points we have to roll back to. Don't be too excessive (don't commit after every change you make to anything), but try to commit every time you complete a piece of code or a version of an asset. As a good rule of thumb, if you feel like you need more than 80 letters (about one line) to describe what you did, it should have been more than one commit. It kind of sucks for you when you realize you've done 4 hours of work and have a massive commit and have to figure out how to break it up. Or even worse, you don't commit for 4 hours, so your changes aren't backed up, and something goes wrong and you lose all of your work.

----------
<a name="commit-messages"></a>
## Pretty Commit Messages
#### *ooh, ahh*

We're going to be using a commit style similar, but not exactly the same as, [Conventional Commits](conventionalcommits.org). Essentially, the general format of a commit message should be as follows:

&lt;type&gt;(&lt;PartOfProject&gt;): &lt;brief summary, less than 80 characters / fits on one line&gt;

&lt;optional longer message body with notes on what you did&gt;

Where &lt;type&gt; is one of:
* feat - short for "feature". Commits that introduce new functionality or assets use this type.
* fix - Commits that fix a bug use this type.
* chore - Commits that do things like setup/documentation/organization but don't affect the game directly use this type.
* refactor - Commits that refactor older code/assets without changing functionality in any way use this type
* test - Commits that deal with automated testing use this type

So, for example, the first commit in the repo (after the default initial commit) is:

chore(Setup): Committing initial Unity files.

You can see immediately from that message that the commit worked on setting up the project by adding the initial Unity files to Git.

----------
<a name="before-you-work"></a>
## Pull & Update Us Before You Work
#### *"Merge conflicts literally are satan" - Albert Einstein*

Avoiding merge conflicts is absolutely critical to this project, since they are difficult to resolve and often result in problems. The single most important thing you can do to avoid merge conflicts is to **do a pull before you do any work**. This will make sure that your project has the latest code before you start modifying anything. Also, make sure that you go to the [GitHub repository page](https://github.com/amagid/Game-Development-Fall-2018) and create a pull request when you're done. That process will be described more in the [Contribution Review section](#contribution-review).

To help with this, please also create a branch whenever you start work on a larger feature. The branching policy is explained more in the [Branching](#branching) section below.

Make sure that when you are pulling at the beginning of your work session that you are on the Master branch. Otherwise, you'll pull changes from whever other branch you're currently on.

Once you have pulled the latest code, post an update to the Scrum-Updates channel on Discord answering three questions:
1. What did you do since your last message?
2. What are you going to do now?
3. What (if anything) is standing in your way / causing problems for you?

These messages will help us keep the team up to date on progress and avoid double-work. You also need to claim the task you're working on in Trello by opening the Sprint Work Trello board, finding the card for your task, clicking on that card, clicking the "Members" button in its edit window, selecting yourself, and then dragging the card over to the In Progress list. This process will put your name on the card and signal to everyone that it is in progress.

----------
<a name="branching"></a>
## Always Work On A Branch
#### *In case things go nuclear, we're safe*

When you are ready to start working, create a new branch in the repository for your work. The name of this branch should follow the format below:

&lt;Type&gt;/&lt;FeatureName&gt;

The Type can be the same as the types of commits, described in the [Commit Message Style section](#commit-messages). But as an example, if you're working on the jumping feature, you might name your branch *Feature/Jump*. Similarly, if you're fixing a bug where gravity doesn't work, you might name your branch *Fix/Gravity*. This will show everyone exactly what is being worked on with that branch.

----------
<a name="contribution-review"></a>
## Not Without Review, Ya Don't!
#### *Two heads are better than one \[unless both heads are stupid\]*

https://yangsu.github.io/pull-request-tutorial/
As a general rule, the Master (default) branch of the repository will be restricted to keep the project repository clean.