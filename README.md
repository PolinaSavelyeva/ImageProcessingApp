# ImageProcessingApp

ImageProcessingApp is an easy-to-use application with GPU-processing support, which uses the [PolinaSavelyeva.ImageProcessing](https://github.com/PolinaSavelyeva/ImageProcessing) library and [Avalonia UI](https://github.com/AvaloniaUI/Avalonia) framework.

## Supported Features

* Load an image by specifying its directory path.
* Process images using either GPU or CPU modes.
* Image filters, e.g
    * Gaussian Blur
    * Edges
    * Darken
    * Lighten
    * Sharpen
* Clockwise/Counterclockwise rotation
* Vertical/Horizontal flip
* Save the image by specifying the preferred directory path and naming the file for saving.

| Standard window                                                                                            |
|:-----------------------------------------------------------------------------------------------------------|
| ![image](https://github.com/PolinaSavelyeva/ImageProcessingApp/blob/dev/Assets/Samples/StandardWindow.png) |


|GPU mode on|
|:-----------------------------------------------------------------------------------------------------------------|
| ![image](https://github.com/PolinaSavelyeva/ImageProcessingApp/blob/dev/Assets/Samples/GPUModeOn.png)     |

|Filter menu|
|:-----------------------------------------------------------------------------------------------------------------------|
| ![image](https://github.com/PolinaSavelyeva/ImageProcessingApp/blob/dev/Assets/Samples/FilterMenu.png) |

## Simple Usage

- Make sure you have at least version 7 of .NET installed
  
- Clone the source repository
   
   - using Github SSH-key authentication
     
     ```sh
     git clone git@github.com:PolinaSavelyeva/ImageProcessingApp.git
     ```
     
   - or using GitHub password
     
     ```sh
     git clone https://github.com/PolinaSavelyeva/ImageProcessingApp.git
     ```

- Got to cloned project's directory

    ```sh
    cd <PathWhereProjectWasCloned>/ImageProcessingApp
    ```
    
- Download  `PolinaSavelyeva.ImageProcessing` library
    - using command
      
      ```sh
      wget https://github.com/PolinaSavelyeva/ImageProcessing/releases/download/v1.0.0/polinasavelyeva.imageprocessing.1.0.0.nupkg
      ```
      
    - or [manually](https://github.com/PolinaSavelyeva/ImageProcessing/releases/download/v1.0.0/polinasavelyeva.imageprocessing.1.0.0.nupkg)

- Add the downloaded  `PolinaSavelyeva.ImageProcessing ` library to nuget package source manually
    - move it (you nedd to add some directories) to `<PathToNugetFolder>/.nuget/packages/polinasavelyeva.imageprocessing/1.0.0` folder 

    - or using command from within a project's  `ImageProcessingApp` root, as the command **will modify your `NuGet.config` file**

       ```sh
       dotnet nuget add source $(pwd)
       ```

- Now you can restore, build and run project using these commands from `ImageProcessingApp` root directory

    ```sh
    dotnet restore
    ```
    
    ```sh
    dotnet build
    ```
    
    ```sh
    dotnet run
    ```
