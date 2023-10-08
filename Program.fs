﻿namespace ImageProcessingApp

open System
open Avalonia

module Program =

    [<CompiledName "ImageProcessingApp">] 
    let buildImageProcessingApp () = 
        AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace(areas = Array.empty)

    [<EntryPoint; STAThread>]
    let main argv =
        buildImageProcessingApp().StartWithClassicDesktopLifetime(argv)