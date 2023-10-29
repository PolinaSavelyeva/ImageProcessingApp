namespace ImageProcessingApp

open Avalonia
open Avalonia.Controls
open Brahma.FSharp
open ImageProcessing.MyImage
open ImageProcessing.Process
open Avalonia.Markup.Xaml
open Avalonia.Interactivity
open Avalonia.Styling
open System.IO
open MsBox.Avalonia
open Avalonia.Media.Imaging

type MainWindow() as this =
    inherit Window()

    let mutable temporaryImagePath = __SOURCE_DIRECTORY__ + "/Assets/Temporary/tmp.png"
    let mutable myImage = load temporaryImagePath

    let device = ClDevice.GetFirstAppropriateDevice()
    let clContext = ClContext(device)
    let transformationsParserGPU = transformationsParserGPU clContext 64

    let processImage currentTheme (imageContainer: Image) (transformation: Transformations) =

        match currentTheme with
        | "Light" ->
            myImage <- myImage |> transformationsParserCPU transformation
            save myImage temporaryImagePath
            imageContainer.Source <- new Bitmap(temporaryImagePath)

        | "Dark" ->
            myImage <- myImage |> transformationsParserGPU transformation
            save myImage temporaryImagePath
            imageContainer.Source <- new Bitmap(temporaryImagePath)
        |> ignore

    do this.InitializeComponent()

    member this.SwitchThemes(source: obj, args: RoutedEventArgs) =
        match this.ActualThemeVariant.ToString() with
        | "Light" ->
            this.SetValue<ThemeVariant>(Application.RequestedThemeVariantProperty, ThemeVariant.Dark)

            let gpuModeOnBox =
                MessageBoxManager.GetMessageBoxStandard(
                    "GPUNotification",
                    "GPU mode on",
                    MsBox.Avalonia.Enums.ButtonEnum.Ok
                )

            gpuModeOnBox.ShowAsPopupAsync(this) |> ignore

        | "Dark" ->
            this.SetValue<ThemeVariant>(Application.RequestedThemeVariantProperty, ThemeVariant.Light)

            let gpuModeOffBox =
                MessageBoxManager.GetMessageBoxStandard(
                    "GPUNotification",
                    "GPU mode off",
                    MsBox.Avalonia.Enums.ButtonEnum.Ok
                )

            gpuModeOffBox.ShowAsPopupAsync(this) |> ignore
        |> ignore

    member this.ButtonWithInputBorderClick(source: obj, args: RoutedEventArgs) =

        let saveBorder = this.FindControl<Border>("SaveBorder")
        let importBorder = this.FindControl<Border>("ImportBorder")

        let _inner (currentBorder: Border) (anotherBorder: Border) =
            currentBorder.IsVisible <- not currentBorder.IsVisible
            anotherBorder.IsVisible <- false

        match source with
        | :? Button as button ->
            match button.Name with
            | "ImportButton" -> _inner importBorder saveBorder
            | "SaveButton" -> _inner saveBorder importBorder

    member this.ClickOnBorder(source: obj, args: Avalonia.Input.TappedEventArgs) =
        let importBorder = this.FindControl<Border>("ImportBorder")
        let saveBorder = this.FindControl<Border>("SaveBorder")

        importBorder.IsVisible <- false
        saveBorder.IsVisible <- false

    member this.ClickOnAnotherButton(source: obj, args: RoutedEventArgs) =
        let importBorder = this.FindControl<Border>("ImportBorder")
        let saveBorder = this.FindControl<Border>("SaveBorder")

        importBorder.IsVisible <- false
        saveBorder.IsVisible <- false

    member this.ImportImage(source: obj, args: RoutedEventArgs) =
        let imageContainer = this.FindControl<Image>("Image")
        let pathToImportImage = this.FindControl<TextBox>("PathToImport").Text

        if File.Exists pathToImportImage then
            try
                myImage <- load pathToImportImage

                temporaryImagePath <-
                    __SOURCE_DIRECTORY__
                    + "/Assets/Temporary/tmp"
                    + Path.GetExtension pathToImportImage

                save myImage temporaryImagePath

                imageContainer.Source <- new Bitmap(pathToImportImage)

                let importingWasSucceedBox =
                    MessageBoxManager.GetMessageBoxStandard(
                        "ImportNotification",
                        "Importing was successful",
                        MsBox.Avalonia.Enums.ButtonEnum.Ok
                    )

                importingWasSucceedBox.ShowAsPopupAsync(this) |> ignore
            with ex ->
                let importingWasFailedBox =
                    MessageBoxManager.GetMessageBoxStandard(
                        "ImportNotification",
                        ex.Message,
                        MsBox.Avalonia.Enums.ButtonEnum.Ok
                    )

                importingWasFailedBox.ShowAsPopupAsync(this) |> ignore
        else
            let incorrectPathBox =
                MessageBoxManager.GetMessageBoxStandard(
                    "ImportNotification",
                    "Incorrect path to import image",
                    MsBox.Avalonia.Enums.ButtonEnum.Ok
                )

            incorrectPathBox.ShowAsPopupAsync(this) |> ignore

    member this.Process(source: obj, args: RoutedEventArgs) =
        let imageContainer = this.FindControl<Image>("Image")
        let processImage = processImage (this.ActualThemeVariant.ToString()) imageContainer

        match source with
        | :? MenuItem as item ->
            match item.Name with
            | "GaussianBlur" -> processImage Gauss
            | "Edges" -> processImage Edges
            | "Darken" -> processImage Darken
            | "Lighten" -> processImage Lighten
            | "Sharpen" -> processImage Sharpen
            | "VerticalFlip" -> processImage FlipV
            | "HorizontalFlip" -> processImage FlipH
            | "ClockwiseRotate" -> processImage RotationR
            | "CounterclockwiseRotate" -> processImage RotationL

    member this.SaveImage(source: obj, args: RoutedEventArgs) =
        let pathToSaveImage = this.FindControl<TextBox>("PathToSave").Text

        if Directory.Exists(Path.GetDirectoryName pathToSaveImage) then
            try
                File.Copy(temporaryImagePath, pathToSaveImage)

                let savingWasSucceedBox =
                    MessageBoxManager.GetMessageBoxStandard(
                        "SavingNotification",
                        "Saving was successful",
                        MsBox.Avalonia.Enums.ButtonEnum.Ok
                    )

                savingWasSucceedBox.ShowAsPopupAsync(this) |> ignore
            with :? System.Exception as ex ->
                let savingWasFailedBox =
                    MessageBoxManager.GetMessageBoxStandard(
                        "SavingNotification",
                        ex.Message,
                        MsBox.Avalonia.Enums.ButtonEnum.Ok
                    )

                savingWasFailedBox.ShowAsPopupAsync(this) |> ignore
        else
            let pathWasIncorrectBox =
                MessageBoxManager.GetMessageBoxStandard(
                    "SavingNotification",
                    "Incorrect path to save image",
                    MsBox.Avalonia.Enums.ButtonEnum.Ok
                )

            pathWasIncorrectBox.ShowAsPopupAsync(this) |> ignore

    member private this.InitializeComponent() =
#if DEBUG
        this.AttachDevTools()
#endif
        this.Width <- 1500
        this.Height <- 920

        AvaloniaXamlLoader.Load(this)
