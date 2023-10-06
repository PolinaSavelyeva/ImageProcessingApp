namespace ImageProcessingApp

open Avalonia
open Avalonia.Controls
open Avalonia.Markup.Xaml

type MainWindow () as this = 
    inherit Window ()

    do this.InitializeComponent()
    
    member private this.InitializeComponent() =
#if DEBUG
        this.AttachDevTools()
#endif
        this.Width <- 1260
        this.Height <- 896
        
        AvaloniaXamlLoader.Load(this)
