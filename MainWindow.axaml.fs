namespace ImageProcessingApp

open Avalonia
open Avalonia.Controls
open ImageProcessing
open ImageProcessing.MyImage
open Avalonia.Markup.Xaml
open Avalonia.Interactivity
open Avalonia.Styling
open System.IO
open MsBox.Avalonia
open Avalonia.Media.Imaging
    
type MainWindow () as this =
    inherit Window ()
    
    let mutable temporaryImagePath = __SOURCE_DIRECTORY__ + "/Assets/Temporary/tmp.png"
    
    let device = Brahma.FSharp.ClDevice.GetFirstAppropriateDevice()
    let clContext = Brahma.FSharp.ClContext(device)
    let applyFilterGPU = GPU.applyFilter clContext 64
    let rotateGPU = GPU.rotate clContext 64
    let flipGPU = GPU.flip clContext 64
    
    do  this.InitializeComponent()
        
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
                 
        if File.Exists pathToImportImage then
            try 
                let myImage = load pathToImportImage
                temporaryImagePath <- __SOURCE_DIRECTORY__ + "/Assets/Temporary/tmp" + Path.GetExtension pathToImportImage
                save myImage temporaryImagePath
                
                let bitmap = new Bitmap(pathToImportImage)
                imageContainer.Source <- bitmap
                
                let importingWasSucceedBox = MessageBoxManager.GetMessageBoxStandard("ImportNotification", "Importing was successful", MsBox.Avalonia.Enums.ButtonEnum.Ok)
                importingWasSucceedBox.ShowAsPopupAsync(window) |> ignore
            with             
            | :? System.Exception as ex ->
                 let importingWasFailedBox = MessageBoxManager.GetMessageBoxStandard("ImportNotification", ex.Message, MsBox.Avalonia.Enums.ButtonEnum.Ok)
                 importingWasFailedBox.ShowAsPopupAsync(window) |> ignore
        else
            let incorrectPathBox = MessageBoxManager.GetMessageBoxStandard("ImportNotification", "Incorrect path to import image", MsBox.Avalonia.Enums.ButtonEnum.Ok)
            incorrectPathBox.ShowAsPopupAsync(window) |> ignore
        
    
    
    member this.GaussFilter (source: obj, args: RoutedEventArgs) =
       let imageContainer = this.FindControl<Image>("Image")
       match this.ActualThemeVariant.ToString() with
         | "Light" -> let myImage = load temporaryImagePath
                      let newImage = CPU.applyFilter Kernels.gaussianBlurKernel myImage
                      save newImage temporaryImagePath
                      
                      let bitmap = new Bitmap(temporaryImagePath)
                      imageContainer.Source <- bitmap
                      
         | "Dark" ->  let myImage = load temporaryImagePath
                      let newImage = applyFilterGPU Kernels.gaussianBlurKernel myImage
                      save newImage temporaryImagePath
                      
                      let bitmap = new Bitmap(temporaryImagePath)
                      imageContainer.Source <- bitmap
       |> ignore
                 
    member this.EdgesFilter (source: obj, args: RoutedEventArgs) =
       let imageContainer = this.FindControl<Image>("Image")
       match this.ActualThemeVariant.ToString() with
         | "Light" -> let myImage = load temporaryImagePath
                      let newImage = CPU.applyFilter Kernels.edgesKernel myImage
                      save newImage temporaryImagePath
                      
                      let bitmap = new Bitmap(temporaryImagePath)
                      imageContainer.Source <- bitmap
                      
         | "Dark" ->  let myImage = load temporaryImagePath
                      let newImage = applyFilterGPU Kernels.edgesKernel myImage
                      save newImage temporaryImagePath
                      
                      let bitmap = new Bitmap(temporaryImagePath)
                      imageContainer.Source <- bitmap
       |> ignore
    
    member this.DarkenFilter (source: obj, args: RoutedEventArgs) =
       let imageContainer = this.FindControl<Image>("Image")
       match this.ActualThemeVariant.ToString() with
         | "Light" -> let myImage = load temporaryImagePath
                      let newImage = CPU.applyFilter Kernels.darkenKernel myImage
                      save newImage temporaryImagePath
                      
                      let bitmap = new Bitmap(temporaryImagePath)
                      imageContainer.Source <- bitmap
                      
         | "Dark" ->  let myImage = load temporaryImagePath
                      let newImage = applyFilterGPU Kernels.darkenKernel myImage
                      save newImage temporaryImagePath
                      
                      let bitmap = new Bitmap(temporaryImagePath)
                      imageContainer.Source <- bitmap
       |> ignore
    
    member this.LightenFilter (source: obj, args: RoutedEventArgs) =
       let imageContainer = this.FindControl<Image>("Image")
       match this.ActualThemeVariant.ToString() with
         | "Light" -> let myImage = load temporaryImagePath
                      let newImage = CPU.applyFilter Kernels.lightenKernel myImage
                      save newImage temporaryImagePath
                      
                      let bitmap = new Bitmap(temporaryImagePath)
                      imageContainer.Source <- bitmap
                      
         | "Dark" ->  let myImage = load temporaryImagePath
                      let newImage = applyFilterGPU Kernels.lightenKernel myImage
                      save newImage temporaryImagePath
                      
                      let bitmap = new Bitmap(temporaryImagePath)
                      imageContainer.Source <- bitmap
       |> ignore
    
    member this.SharpenFilter (source: obj, args: RoutedEventArgs) =
       let imageContainer = this.FindControl<Image>("Image")
       match this.ActualThemeVariant.ToString() with
         | "Light" -> let myImage = load temporaryImagePath
                      let newImage = CPU.applyFilter Kernels.sharpenKernel myImage
                      save newImage temporaryImagePath
                      
                      let bitmap = new Bitmap(temporaryImagePath)
                      imageContainer.Source <- bitmap
                      
         | "Dark" ->  let myImage = load temporaryImagePath
                      let newImage = applyFilterGPU Kernels.sharpenKernel myImage
                      save newImage temporaryImagePath
                      
                      let bitmap = new Bitmap(temporaryImagePath)
                      imageContainer.Source <- bitmap
       |> ignore
    
    member this.VerticalFlip (source: obj, args: RoutedEventArgs) =
       let imageContainer = this.FindControl<Image>("Image")
       match this.ActualThemeVariant.ToString() with
         | "Light" -> let myImage = load temporaryImagePath
                      let newImage = CPU.flip true myImage
                      save newImage temporaryImagePath
                      
                      let bitmap = new Bitmap(temporaryImagePath)
                      imageContainer.Source <- bitmap
                      
         | "Dark" ->  let myImage = load temporaryImagePath
                      let newImage = flipGPU true myImage
                      save newImage temporaryImagePath
                      
                      let bitmap = new Bitmap(temporaryImagePath)
                      imageContainer.Source <- bitmap
       |> ignore
       
    member this.HorizontalFlip (source: obj, args: RoutedEventArgs) =
       let imageContainer = this.FindControl<Image>("Image")
       match this.ActualThemeVariant.ToString() with
         | "Light" -> let myImage = load temporaryImagePath
                      let newImage = CPU.flip false myImage
                      save newImage temporaryImagePath
                      
                      let bitmap = new Bitmap(temporaryImagePath)
                      imageContainer.Source <- bitmap
                      
         | "Dark" ->  let myImage = load temporaryImagePath
                      let newImage = flipGPU false myImage
                      save newImage temporaryImagePath
                      
                      let bitmap = new Bitmap(temporaryImagePath)
                      imageContainer.Source <- bitmap
       |> ignore
    
    member this.ClockwiseRotate (source: obj, args: RoutedEventArgs) =
       let imageContainer = this.FindControl<Image>("Image")
       match this.ActualThemeVariant.ToString() with
         | "Light" -> let myImage = load temporaryImagePath
                      let newImage = CPU.rotate true myImage
                      save newImage temporaryImagePath
                      
                      let bitmap = new Bitmap(temporaryImagePath)
                      imageContainer.Source <- bitmap
                      
         | "Dark" ->  let myImage = load temporaryImagePath
                      let newImage = rotateGPU true myImage
                      save newImage temporaryImagePath
                      
                      let bitmap = new Bitmap(temporaryImagePath)
                      imageContainer.Source <- bitmap
       |> ignore
       
    member this.CounterclockwiseRotate (source: obj, args: RoutedEventArgs) =
       let imageContainer = this.FindControl<Image>("Image")
       match this.ActualThemeVariant.ToString() with
         | "Light" -> let myImage = load temporaryImagePath
                      let newImage = CPU.rotate false myImage
                      save newImage temporaryImagePath
                      
                      let bitmap = new Bitmap(temporaryImagePath)
                      imageContainer.Source <- bitmap
                      
         | "Dark" ->  let myImage = load temporaryImagePath
                      let newImage = rotateGPU false myImage
                      save newImage temporaryImagePath
                      
                      let bitmap = new Bitmap(temporaryImagePath)
                      imageContainer.Source <- bitmap
       |> ignore
    
    member this.SaveImage (source: obj, args: RoutedEventArgs) =
        let pathToSaveImage = this.FindControl<TextBox>("PathToSave").Text
        let window = this.FindControl<Window>("MainWindow")
        
        if Directory.Exists(Path.GetDirectoryName pathToSaveImage) then
            try
                File.Copy(temporaryImagePath, pathToSaveImage)
                
                let savingWasSucceedBox= MessageBoxManager.GetMessageBoxStandard("SavingNotification", "Saving was successful", MsBox.Avalonia.Enums.ButtonEnum.Ok)
                savingWasSucceedBox.ShowAsPopupAsync(window) |> ignore
            with
            | :? System.Exception as ex ->
                 let savingWasFailedBox = MessageBoxManager.GetMessageBoxStandard("SavingNotification", ex.Message, MsBox.Avalonia.Enums.ButtonEnum.Ok)
                 savingWasFailedBox.ShowAsPopupAsync(window) |> ignore
        else
            let pathWasIncorrectBox = MessageBoxManager.GetMessageBoxStandard("SavingNotification", "Incorrect path to save image", MsBox.Avalonia.Enums.ButtonEnum.Ok)
            pathWasIncorrectBox.ShowAsPopupAsync(window) |> ignore
        
    member private this.InitializeComponent() =        
#if DEBUG
        this.AttachDevTools()
#endif      
        this.Width <- 1500
        this.Height <- 920
        
        AvaloniaXamlLoader.Load(this)

        

        