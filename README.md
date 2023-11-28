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

|Standard window|
|:--------------------------------------------------------------------------------------------------------------------|
| ![image](https://github.com/PolinaSavelyeva/ImageProcessingApp/blob/dev/Assets/Samples/FilterMenu.png) |


|GPU mode on|
|:-----------------------------------------------------------------------------------------------------------------|
| ![image](https://github.com/PolinaSavelyeva/ImageProcessingApp/blob/dev/Assets/Samples/GPUModeOn.png)     |

|Filter menu|
|:-----------------------------------------------------------------------------------------------------------------------|
| ![image](https://github.com/PolinaSavelyeva/ImageProcessingApp/blob/dev/Assets/Samples/FilterMenu.png) |

## Simple Usage
- Make sure you have at least version 6 of .NET installed
- Clone the source repository
- Go to the directory and type ```dotnet run```