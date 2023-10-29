namespace ImageProcessingApp

open Avalonia
open Avalonia.Controls
open Avalonia.Controls.Primitives
open Avalonia.Markup.Xaml
open Avalonia.Interactivity
open Avalonia.Styling
open System.IO
open MsBox.Avalonia
open Avalonia.Media.Imaging

type MainWindow () as this = 
    inherit Window ()
    
    do this.InitializeComponent()    
    
    member this.SwitchThemes(source: obj, args: RoutedEventArgs) =
       match this.ActualThemeVariant.ToString() with
         | "Light" -> this.SetValue<ThemeVariant>(Application.RequestedThemeVariantProperty, ThemeVariant.Dark)
         | "Dark" -> this.SetValue<ThemeVariant>(Application.RequestedThemeVariantProperty, ThemeVariant.Light) 
       |> ignore
           
    member this.ImportButtonClick(source: obj, args: RoutedEventArgs) =
        let importBorder = this.FindControl<Border>("ImportBorder")
        let saveBorder =  this.FindControl<Border>("SaveBorder")
       
        importBorder.IsVisible <- not importBorder.IsVisible
        saveBorder.IsVisible <- false
    
    member this.SaveButtonClick(source: obj, args: RoutedEventArgs) =
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
        let imageContainer = this.FindControl<Image>("Image")
        let pathToImportImage = this.FindControl<TextBox>("PathToImport").Text
        let window = this.FindControl<Window>("MainWindow")
         
        let incorrectPathBox = MessageBoxManager.GetMessageBoxStandard("ImportCaption", "Incorrect path to import image", MsBox.Avalonia.Enums.ButtonEnum.Ok)
        
        if File.Exists(pathToImportImage) then
            let bitmap = new Bitmap(pathToImportImage)
            imageContainer.Source <- bitmap
        else
            incorrectPathBox.ShowAsPopupAsync(window) |> ignore
    
    member this.SaveImage (source: obj, args: RoutedEventArgs) =
        let imageContainer = this.FindControl<Image>("Image")
        let pathToSaveImage = this.FindControl<TextBox>("PathToSave").Text
        let window = this.FindControl<Window>("MainWindow")
        
        let incorrectPathBox = MessageBoxManager.GetMessageBoxStandard("SaveCaption", "Incorrect path to save image", MsBox.Avalonia.Enums.ButtonEnum.Ok)

        if File.Exists(pathToSaveImage) then
            ()
        else
            incorrectPathBox.ShowAsPopupAsync(window) |> ignore    
        
    member private this.InitializeComponent() =        
#if DEBUG
        this.AttachDevTools()
#endif
        this.Width <- 1500
        this.Height <- 920
        
        AvaloniaXamlLoader.Load(this)

        

        