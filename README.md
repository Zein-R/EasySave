# EasySave

**EasySave** is a Windows desktop backup application built in C# / WPF (MVVM), developed as a school project (CESI). This version (v3) is a graphical rewrite of an earlier console-based EasySave project, adding a full UI, real-time monitoring, file encryption, and multi-language support.

## Features

- **Backup jobs ("works")**: create, edit, and run backup jobs, each with a source folder, a target folder, and a backup type (`FULL` or `DIFFERENTIAL`).
- **Real-time progress**: each job's state (progress, current file, files remaining, etc.) is tracked live and persisted to `State.json`, and reflected in the UI (progress bars).
- **Logging**: every file copy is logged (`Log.cs`) to a log file for traceability.
- **File encryption**: files matching configured extensions are encrypted via an external "CryptoSoft" executable, configured through `Settings.json` / `CryptExtension.json` (see the technical documentation below for the exact syntax).
- **Priority extensions**: certain file extensions can be marked as priority so matching files are backed up first.
- **Business software detection**: backups can be automatically paused while a configured "business software" process is running, to avoid interfering with the user's work.
- **Max simultaneous transfer size**: a configurable threshold limits how much can be copied in parallel across jobs.
- **Multi-language UI**: English (default) and French, via `.resx` resource files (`Langs/`).
- **Remote monitoring**: `WorkToSend` / `ReceiveObject` models support sending job state over a socket connection, for a remote/real-time monitoring console.

## Project structure

```
.
├── App.xaml(.cs)              # Application entry point
├── MainWindow.xaml(.cs)       # Main window
├── NS_Model/                  # Domain models (Work, Settings, State, Log, BackupType, ...)
├── NS_View/                   # WPF views (Menu, AddWork, Settings, Error)
├── NS_ViewModel/              # MVVM view models
├── Observable/                # Base observable/INotifyPropertyChanged helpers
├── Theme/                     # UI theming resources
├── Langs/                     # Localization resources (en, fr-FR)
├── Fonts/, Images/            # Static assets
├── Doc_utilisateur.odt        # User documentation (French)
├── Documentation technique EasySave3.0.txt  # Technical documentation (French)
└── EasySave.sln / EasySave.csproj
```

## Tech stack

- **Language:** C#
- **UI framework:** WPF (Windows Presentation Foundation), MVVM pattern
- **Target framework:** .NET Core 3.1 (Windows Desktop)
- **Data formats:** JSON for settings/state/logs

## Getting started

### Requirements

- Windows
- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet/3.1) (or a compatible Visual Studio installation with the ".NET desktop development" workload)

### Build & run

```bash
git clone https://github.com/Zein-R/EasySave-V3-Final.git
cd EasySave-V3-Final
dotnet build
dotnet run --project EasySave.csproj
```

Alternatively, open `EasySave.sln` in Visual Studio and run the `EasySave` project.

### Configuration

On first run, EasySave creates its configuration and state files (`Settings.json`, `State.json`) next to the executable. Key settings:

- `cryptoSoftPath`: path to the external CryptoSoft executable used to encrypt files.
- `cryptoExtensions`: list of file extensions to encrypt (e.g. `.txt,.csv`).
- `prioExtensions`: list of file extensions to back up first.
- `businessSoftwares`: process names that, when running, pause backups.
- `maxSimultaneousFilesSize`: maximum total size (in the app's unit) for simultaneous file transfers.
- `language`: UI language (`en-US` or `fr-FR`).

Refer to `Documentation technique EasySave3.0.txt` and `Doc_utilisateur.odt` (in French) for more detailed usage notes.

## Logs & state

- Job state is persisted to `State.json` next to the executable.
- Copy logs are written to a `Logs/` folder next to the executable.

## Contributors

Built as a group project, with contributions from Zein Rafiq and teammates.

## License

No license has been specified for this project. All rights reserved by the contributors unless stated otherwise.
