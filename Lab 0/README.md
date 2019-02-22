# Lab 0 - configure your development environment

The following tools and software needs to be installed:

## Git

- Install [git](https://git-scm.com/) to clone this github project.

## Node

- Install NPM and [Node.js](https://nodejs.org/en/) version 8.9.4 or higher.

## Windows Build Tools

- Install Windows Build Tools globally (<https://www.npmjs.com/package/windows-build-tools>).

Start a new command prompt as Administrator and type this command: `npm install --global --production windows-build-tools`

Note that this will take some time (minutes), only continue to next steps when you see output like this:

``` x
C:\Users\stef>npm install --global --production windows-build-tools

> windows-build-tools@2.3.0 postinstall C:\Users\stef\AppData\Roaming\npm\node_modules\windows-build-tools
> node ./lib/index.js

Downloading python-2.7.14.amd64.msi
Downloading BuildTools_Full.exe
[============================================>] 100.0% of 3.29 MB (692.2 kB/s)
Downloaded BuildTools_Full.exe. Saved to C:\Users\stef\.windows-build-tools\BuildTools_Full.exe.

Starting installation...
Launched installers, now waiting for them to finish.
This will likely take some time - please be patient!

Status from the installers:
---------- Visual Studio Build Tools ----------
Successfully installed Visual Studio Build Tools.
------------------- Python --------------------
Successfully installed Python 2.7
+ windows-build-tools@2.3.0
updated 1 package in 637.13s

C:\Users\stef>
```

## Ganache

- Install Ganache (personal Ethereum blockchain) from [truffleframework.com/ganache](https://truffleframework.com/ganache).

## Visual Code

Install latest Visual Studio Code from <https://code.visualstudio.com>.

Install the following the following extensions in Visual Studio Code:

- `c#`
- `solidity`
- `.ejs`
- `eslint`
- `markdownlint`
- `Version Lens`

## .NET Core 2

Install latest .NET Core SDK (2.0 or higher) from <https://www.microsoft.com/net/download/windows>

## Steps

### Git Clone

Clone this project using **git**:

- make a folder where you want to download this project
- go to that folder
- execute this command in the command-prompt:

``` x
git clone https://github.com/mstack/bootcamp-blockchain-smartcontracts
```

### Or download

- Just download this project as a zip-file and extract that zip-file into the folder.