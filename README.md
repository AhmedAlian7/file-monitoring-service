# 📂 Folder Monitor Windows Service
A Windows Service built with C# that monitors a specified source folder for newly added files, renames them using a globally unique identifier (GUID), moves them to a destination folder, deletes the original files, and logs each operation.
---
## ✅ Features
- 🔍 Real-time folder monitoring using `FileSystemWatcher`
- 🔄 Automatic file renaming with `GUID` to ensure uniqueness
- 📁 File movement to a configurable destination directory
- 📝 Robust logging of all file operations with timestamps
- ⚙️ Dynamic folder configuration through `App.config`
- 🛠️ Can run silently in the background as a **Windows Service**
---
## 🏗️ How It Works
1. The service watches a specified **source folder**.
2. When a new file is created:
   - A **new file name** is generated using a `GUID`.
   - The file is **moved** to the **destination folder** with the new name.
   - The **original file is deleted**.
   - The operation is **logged** with a timestamp.
3. All folder paths and log file location are configured in `App.config`.
---
## 📺 Video Tutorial
Watch my detailed explanation of this project on YouTube:

[![Folder Monitor Windows Service Tutorial](https://img.youtube.com/vi/XBLhyoAqXiE/maxresdefault.jpg)](https://youtube.com/watch?v=XBLhyoAqXiE)

In this video, I walk through:
- 🎯 Project overview and use cases
- 💻 Code walkthrough and implementation details
- ⚙️ Configuration setup and customization
- 🔧 Installation and deployment process
- 🧪 Live demonstration of the service in action

*Don't forget to **like**, **subscribe**, and **hit the bell icon** for more programming tutorials!*
---
## 🔧 Installation
### 1. Build the Service
- Open the project in **Visual Studio**.
- Switch the build configuration to **Release**.
- Build the solution to generate the executable file (`FolderMonitorService.exe`).
### 2. Open Developer Command Prompt
- Run **Developer Command Prompt for Visual Studio** as **Administrator**.
### 3. Install the Service
Run the following command in the directory where `FolderMonitorService.exe` is located:
```bash
InstallUtil.exe FolderMonitorService.exe
```
---
## 🙏 Thank You
Thanks for checking out this project!
If you have any **suggestions**, **questions**, or **feedback**, feel free to [open an issue](https://github.com/YourUsername/FolderMonitorService/issues) or submit a pull request.
I'm always open to improvements and collaboration!
---
