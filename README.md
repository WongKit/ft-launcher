# FT Launcher

## Introduction

The **FT Launcher** is an open source project to replace the original launcher/patcher of the game **Fantasy Tennis**.

It is heavily inspired by the discontinued **Phoenix Fastload Advanced** launcher, that I released in 2009 when the game was still active.

As a result of the [JFTSE - Java Fantasy Tennis Server Emulator](https://github.com/sstokic-tgm/JFTSE) progress, I completely rebuild the launchers core functions using C# and Windows Forms while also keeping its original style.

![FT Launcher Screenshot](misc/launcher-screenshot.png)

## Installation

Grab the binary from the [RELEASE](https://github.com/WongKit/FT-Launcher/releases) section or build your own version using [Microsoft Visual Studio](https://visualstudio.microsoft.com/).

## Usage

### Configuration

Despite its name, the FT Launcher can keep any application up-to-date as it is freely configurable. Along with the FT_Launcher.exe, there is a file named **FT_Launcher.exe.config**, that can be used to modify certain parameters within the `<appSettings>` section.

| key            | example                    | description                                                                                                                       |
|----------------|----------------------------|-----------------------------------------------------------------------------------------------------------------------------------|
| title          | FT Launcher                | Application name shown in the titlebar and taskbar                                                                                |
| newsUrl        | http://localhost/news.html | Website to be shown in the news tab                                                                                               |

In the `<downloadUrls>` section, you can add **up to 5** different updates servers that can be selected by the user in the applications settings menu.
| key            | example                    | description                                                                                                                       |
|----------------|----------------------------|-----------------------------------------------------------------------------------------------------------------------------------|
| name           | Default Server             | Name of the update server that is shown in the settings menu                                                                      |
| url            | http://localhost/update/   | Path to the remote url that contains all patch files and the **files.md5**. Don't forget to include the trailing slash of the URL |
| launchFile     | FT_Client.exe              | Application that is launched after successfully checking for updates                                                              |
| launchArgs     | 0                          | Command line arguments for the launched application                                                                               |

### Providing update files

The launcher checks the `<updateUrl>/files.md5` to determine which local files (relative to the launcher path) are missing or needed to be updated. Therefore, you need to upload all game/application files to a remote webserver along with the checksum file.

You can generate the files.md5 by launching the FT_Launcher.exe **while holding the *Shift*-Key**. You will see an additional button "Create Checksum" that lets you select a directory and build the files.md5 for you.

The launcher can also update itself, if it is part of the checksum list.

### Hints for building the news page

The browser control which shows your custom news page uses the embedded Internet Explorer engine, because it is available on every recent Windows computer. I recommend using the following meta tags within your HTML's `<head>` tag:

```html
<!-- disable IE compatibility mode, allows more "modern" web tech -->
<meta http-equiv="x-ua-compatible" content="IE=Edge"/>

<!-- disable caching, always loads the current version of your page -->
<meta http-equiv="cache-control" content="no-cache">
```