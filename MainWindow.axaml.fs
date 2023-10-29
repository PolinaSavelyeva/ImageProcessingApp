namespace ImageProcessingApp

open System
open Avalonia
open Avalonia.Controls
open Avalonia.Markup.Xaml
open Avalonia.Interactivity
open System.Diagnostics
open Avalonia.Styling
open System.IO
open MsBox.Avalonia
open Avalonia.Media.Imaging
open MsBox.Avalonia.Models

type MainWindow () as this = 
    inherit Window ()
    
    do this.InitializeComponent()    
      
    member this.SwitchToDarkTheme(source: obj, args: RoutedEventArgs) =
        
        this.SetValue<ThemeVariant>(Application.RequestedThemeVariantProperty, ThemeVariant.Dark) |> ignore
        
    member this.SwitchToLightTheme(source: obj, args: RoutedEventArgs) =
           
        this.SetValue<ThemeVariant>(Application.RequestedThemeVariantProperty, ThemeVariant.Light) |> ignore
    
    member this.ImportButtonClicked(source: obj, args: RoutedEventArgs) =
        let importBorder = this.FindControl<Border>("ImportBorder")
        let saveBorder =  this.FindControl<Border>("SaveBorder")
        
        importBorder.IsVisible <- not importBorder.IsVisible
        saveBorder.IsVisible <- false
    
    member this.SaveButtonClicked(source: obj, args: RoutedEventArgs) =
        let importBorder = this.FindControl<Border>("ImportBorder")
        let saveBorder =  this.FindControl<Border>("SaveBorder")
        
        importBorder.IsVisible <- false
        saveBorder.IsVisible <- not saveBorder.IsVisible
    
    member this.ClickOnBorder (source: obj, args: Avalonia.Input.TappedEventArgs) =
        let importBorder = this.FindControl<Border>("ImportBorder")
        let saveBorder =  this.FindControl<Border>("SaveBorder")
        
        importBorder.IsVisible <- false
        saveBorder.IsVisible <- false
        
    member this.ClickOnAnotherButton (source: obj, args: RoutedEventArgs) =
        let importBorder = this.FindControl<Border>("ImportBorder")
        let saveBorder =  this.FindControl<Border>("SaveBorder")
        
        importBorder.IsVisible <- false
        saveBorder.IsVisible <- false
    
    member this.ImportImage (source: obj, args: RoutedEventArgs) =
        let image = this.FindControl<Image>("Image")
        let imagePath = this.FindControl<TextBox>("Path").Text
        let window = this.FindControl<Window>("MainWindow")
        
        let incorrectPathBox = MessageBoxManager.GetMessageBoxStandard("Caption", "Incorrect path to image", MsBox.Avalonia.Enums.ButtonEnum.Ok)

        if File.Exists(imagePath) then
            let bitmap = new Bitmap(imagePath)
            image.Source <- bitmap
        else
            incorrectPathBox.ShowAsPopupAsync(window) |> ignore
        
        
    member private this.InitializeComponent() =        
#if DEBUG
        this.AttachDevTools()
#endif
        this.Width <- 1500
        this.Height <- 920
        
        AvaloniaXamlLoader.Load(this)

        

        