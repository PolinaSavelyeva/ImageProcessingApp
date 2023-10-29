namespace ImageProcessingApp

open System
open Avalonia

module Program =

    [<CompiledName "BuildAvaloniaApp">]
    let buildImageProcessingApp () =
        AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace(areas = Array.empty)

    [<EntryPoint; STAThread>]
    let main argv =
        let temporaryImagePath = __SOURCE_DIRECTORY__ + "/Assets/Temporary/tmp.png"
        IO.File.Delete(temporaryImagePath)
        IO.File.Copy(__SOURCE_DIRECTORY__ + "/Assets/Temporary/main.png", temporaryImagePath)
        
        buildImageProcessingApp().StartWithClassicDesktopLifetime(argv)
