# Ruyi-GUI Setup Guide

## Prerequisites

1. **Download and set up a Python environment**  
   The recommended environment is `webui_forge_cu124_torch24.7z`, which you can download from the [Stable Diffusion WebUI Forge GitHub Releases](https://github.com/lllyasviel/stable-diffusion-webui-forge/releases/tag/latest).

2. **Extract and run Forge**  
   - Extract the contents of the downloaded file.  
   - Run `run.bat` to let Forge automatically download all necessary dependencies.  
   - After setup, your folder structure should look like this:
     ```
     system/
     webui/
     environment.bat
     run.bat
     update.bat
     ```

## Setting Up Ruyi-GUI

1. **Download Ruyi-GUI**  
   - Navigate to [Ruyi-GUI/bin/Release/](https://github.com/bmgjet/Ruyi-GUI/blob/master/Ruyi-GUI/bin/Release/Ruyi-GUI.exe).  
   - Download the `Ruyi-GUI.exe` file.

2. **Place Ruyi-GUI.exe in the Forge folder**  
   Copy the downloaded `Ruyi-GUI.exe` into the root of the Forge folder (the same location as `run.bat`).

3. **Prepare the Ruyi-Models folder**  
   - Create a new folder named `Ruyi-Models` in the Forge folder.  
   - Download the Ruyi Models from [IamCreateAI/Ruyi-Models](https://github.com/IamCreateAI/Ruyi-Models).  
   - Extract the contents of the downloaded archive into the `Ruyi-Models` folder.

## Running the Application

1. Double-click `Ruyi-GUI.exe` to start the application.
2. Follow the on-screen instructions to load models and begin using the GUI.

## Screenshot

![Ruyi-GUI Screenshot](https://raw.githubusercontent.com/bmgjet/Ruyi-GUI/refs/heads/master/guiscreenshot.png)


---

Additional respective GitHub repositories:  
- [Stable Diffusion WebUI Forge](https://github.com/lllyasviel/stable-diffusion-webui-forge)  
- [Ruyi-GUI](https://github.com/bmgjet/Ruyi-GUI)  
- [Ruyi-Models](https://github.com/IamCreateAI/Ruyi-Models)
