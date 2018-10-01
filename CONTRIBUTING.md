# Contributing Guidelines
### Last Updated: 9/26/18 @ 2:43 PM
Hey, everyone! Since we've got a lot of people on this team, we need to work especially hard to make sure our Git repo stays organized. In that spirit, here are some important guidelines for contributing to our Git repository.

**Note: This file may be updated from time to time. If and when that happens, everyone will be notified via discord. If you're ever wondering whether or not the file has been updated, check the "Last Updated" label up at the top.**

----------
## Table Of Contents
#### *It's really more of a list than a table, but whatever*

* [Basics Of Git](#basics-of-git "For newbies, or if you just need a refresher")
* [GitHub Desktop](#github-desktop "Click buttons rather than typing in commands!")
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

In Git, code changes are grouped into *commits*. Each commit stores a set of changes you've made to the various files in the project as well as a message written by you about the commit which should explain what you changed about the project. It also knows who made the commit and when.

**Your code is not automatically sent to the rest of the group!** When you're ready to send your code to the group, you need to *push* your commits to the remote repository (hosted on GitHub). Similarly, if you want to get the latest changes from the group, you need to *pull* the latest commits down from the remote repository onto your local computer.

You can do all of these things once you [download Git](https://git-scm.com/downloads) and optionally [download GitHub Desktop](https://desktop.github.com/). While you don't technically need GitHub Desktop, if you don't get it you'll have to use your computer's command prompt / terminal in order to work with Git.

You can also create *branches*, which are essentially isolated groups of commits which other people won't see. You create branches when you start working on a larger task so that you can create multiple commits in a safe environment that can be instantly deleted if it doesn't work out. Our policy is to create a branch whenever you start work on a new task, using the naming format &lt;Type&gt;/&lt;FeatureName&gt;. The type can be the same as the types of commits, described in the [Commit Message Style section](#commit-messages), though it should be capitalized and not abbreviated. If you'd like, you can also just have one branch of your own where you do your work, but this will require you to make sure your personal branch stays updated with everyone else's code. But as an example, if you're working on the jumping feature, you might name your branch *Feature/Jump*. Similarly, if you're fixing a bug where gravity doesn't work, you might name your branch *Fix/Gravity*. Our branching policy is described in more detail in the [Branching Section](#branching) below.

**BE CAREFUL** - you may have heard some team members talking about *Merge Conflicts*. Merge conflicts occur when you modify a file and then pull the latest changes down from the remote repository when someone else in the team has also modified that same file in a similar way. Git then doesn't know whose changes to use, because they conflict, so it uses both and sticks lines of crap in between to separate them. If the file is text, it's ugly. If the file is code, the code will no longer work. You then have to go directly into the files and resolve the conflict and then re-commit. As you can likely tell, merge conflicts are literally the worst thing in the world, and will ruin your day VERY quickly. They're so bad that there is an [entire section](#before-you-work) of this document that's basically devoted to avoiding merge conflicts. So, if you get an angry message from Git saying that there is a merge conflict, **contact Aaron, Dennis, or someone else who has lots of Git experience** and they will help you resolve it.

----------
<a name="github-desktop"></a>
## GitHub Desktop
#### *Automation at its finest*

While there is the GitHub For Unity plugin, we've noticed that its functionality is extremely limited so you'll need to use GitHub Desktop, which you can download [here](http://desktop.github.com). Once you've created a GitHub account, **send your username to Aaron!** If you don't, you won't be able to contribute to the project.

Once you've done that and downloaded GitHub Desktop, you'll need to tell GitHub Desktop where your project is. If you haven't downloaded our project code yet, click the "Clone Repository" button, and enter "https://github.com/amagid/Game-Development-Fall-2018" as the remote repository URL. If you already have the code, just direct GitHub Desktop over to where you have the code, and it should set itself up.

Check the [Before You Work](#before-you-work) section for more on what to do when you're actually working.

----------
<a name="when-to-commit"></a>
## Commit Early And Often
#### *Less is more? Not here.*

We can roll the project back to any previous commit at any time. So the more commits you have, the more points we have to roll back to. Don't be too excessive (don't commit after every change you make to anything), but try to commit every time you complete a piece of code or a version of an asset. As a good rule of thumb, if you feel like you need more than 80 characters (about one line) to describe what you did, it should have been more than one commit. It kind of sucks for you when you realize you've done 4 hours of work and have a massive commit and have to figure out how to break it up. Or even worse, you don't commit for 4 hours, so your changes aren't backed up, and something goes wrong and you lose all of your work. So, *commit early and often*.

----------
<a name="commit-messages"></a>
## Pretty Commit Messages
#### *ooh, ahh*

In Git, when you make a commit you must attach a message to that commit. The message will explain to other members of the team what you did on a conceptual level with those changes to the project. If you're using GitHub Desktop, what we're talking about here is the *Summary* line of your commit. You can generally leave the larger *Description* field blank, unless you have specific comments you need to make.

We're going to be using a commit style similar to, but not exactly the same as, [Conventional Commits](conventionalcommits.org). Essentially, the general format of a commit message should be as follows:

&lt;type&gt;(&lt;PartOfProject&gt;): &lt;brief summary, less than 80 characters / fits on one line&gt;

Where &lt;type&gt; is one of:
* feat - short for "feature". Commits that introduce new functionality or assets use this type.
* fix - Commits that fix a bug use this type.
* chore - Commits that do things like setup/documentation/organization but don't affect the game directly use this type.
* refactor - Commits that refactor older code/assets without changing functionality in any way use this type.
* test - Commits that deal with automated testing use this type.
* asset - Commits that add an asset should use this type.

So, for example, the first commit in the repo (after the default initial commit) is:

chore(Setup): Committing initial Unity files.

You can see immediately from that message that the commit worked on setting up the project by adding the initial Unity files to Git, without having to look at the actual changes it made to the files.

----------
<a name="before-you-work"></a>
## Before You Work
#### *"Merge conflicts literally are satan" - Albert Einstein*

Avoiding merge conflicts is absolutely critical to this project, since they are difficult to resolve and often result in problems. The single most important thing you can do to avoid merge conflicts is to **do a pull before you do any work**. This will make sure that your project has the latest code before you start modifying anything. Also, make sure that you go to the [GitHub repository page](https://github.com/amagid/Game-Development-Fall-2018) and create a pull request when you're done. That process will be described more in the [Contribution Review section](#contribution-review).

To help with this, please also create a branch whenever you start work on a larger feature, and make sure you're creating the branch *from the Master branch*. The branching policy is explained more in the [Branching](#branching) section below.

Make sure that when you are pulling at the beginning of your work session that you are on the Master branch. Otherwise, you'll pull changes from whatever other branch you're currently on, which may or may not be the latest changes.

Once you have pulled the latest code, post an update to the Scrum-Updates channel on Discord answering three questions:
1. What did you do since your last message?
2. What are you going to do now?
3. What (if anything) is standing in your way / causing problems for you?

These messages will help us keep the team up to date on progress and avoid double-work. You also need to claim the task you're working on in Target Process by opening up your To Do board, finding the card for your task, clicking on that card, clicking the plus button next to "Developer" in the "Assignments" area, selecting yourself, and then dragging the card over to the In Progress column of your To Do board. This process will put your name on the card and signal to everyone that it is in progress.

----------
<a name="branching"></a>
## Always Work On A Branch
#### *In case things go nuclear, we're safe*

When you are ready to start working, create a new branch in the repository for your work. Make sure you are on the Master branch when you create your new branch, or else the branch might not have the latest changes. The name of this branch should follow the format below:

&lt;Type&gt;/&lt;FeatureName&gt;

The Type can be the same as the types of commits, described in the [Commit Message Style section](#commit-messages), though it should be capitalized and not abbreviated. As an example, if you're working on the jumping feature, you might name your branch *Feature/Jump*. Similarly, if you're fixing a bug where gravity doesn't work, you might name your branch *Fix/Gravity*. This will show everyone exactly what is being worked on with that branch.

Publish your branch to the remote repository to make sure that an equivalent of your branch is created there.

----------
<a name="contribution-review"></a>
## Contribution Review
#### *Two heads are better than one \[unless both heads are stupid\]*

As a general rule, the Master (default) branch of the repository will be restricted to keep the project repository clean, and to give one of the project leads a chance to review your changes before they are added to the Master version, which our professors can see and where all of our submissions will come from.

When you are finished with a task, have updated Target Process to reflect that, and are ready for your working branch to be merged into the Master branch of the repository, you will need to submit a *Pull Request*. Here's a quick tutorial on [Pull Requests](https://yangsu.github.io/pull-request-tutorial/). Long story short though, you will need to follow these steps:

1. Head to [our GitHub page](https://github.com/amagid/Game-Development-Fall-2018)
2. Click the *Pull Requests* tab
3. Click the big, green "New Pull Request" button
4. You should see two dropdowns at the top of the area that appears. The one on the left should say "base: " followed by a branch name, and the one on the right should say "compare: " followed by another branch name. Change them so that the left one says "base: master" and the right one says "compare: " followed by the name of the branch that you were working on.
5. You should see a message that says "Able to be merged". If you don't, it means there's a merge conflict. In that case, **contact Aaron, Dennis, or someone else with lots of Git experience to help you resolve it**.
6. If there's no merge conflict, click the "Create Pull Request" button. This will notify Aaron, Dennis, and Shannon that you want your changes merged into the master branch, and one of them will review your changes and then merge them in (or let you know if something needs work).
