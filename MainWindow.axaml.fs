namespace ImageProcessingApp

open Avalonia
open Avalonia.Controls
open Avalonia.Markup.Xaml
open Avalonia.Interactivity
open System.Diagnostics

type MainWindow () as this = 
    inherit Window ()

    do this.InitializeComponent()
    
    member this.ButtonClicked(source: obj, args: RoutedEventArgs ) =
        Debug.WriteLine("Click!")
        
    member private this.InitializeComponent() =
#if DEBUG
        this.AttachDevTools()
#endif
        this.Width <- 1260
        this.Height <- 896
        
        AvaloniaXamlLoader.Load(this)

        

        