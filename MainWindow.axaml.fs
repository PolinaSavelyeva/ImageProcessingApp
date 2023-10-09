namespace ImageProcessingApp

open System.Globalization
open Avalonia
open Avalonia.Controls
open Avalonia.Markup.Xaml
open Avalonia.Interactivity
open System.Diagnostics
open Avalonia.Styling
open Avalonia.Themes.Fluent

type MainWindow () as this = 
    inherit Window ()

    do this.InitializeComponent()    
      
    member private this.InitializeComponent() =        
#if DEBUG
        this.AttachDevTools()
#endif
        this.Width <- 1500
        this.Height <- 920
        
        AvaloniaXamlLoader.Load(this)

        

        