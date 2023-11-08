namespace CounterApp

open Avalonia
open Avalonia.Controls.ApplicationLifetimes
open Avalonia.Themes.Fluent
open Avalonia.FuncUI.Hosts
open Avalonia.Controls
open Avalonia.FuncUI
open Avalonia.FuncUI.DSL
open Avalonia.Layout
open Avalonia.Controls.Shapes


module Main =
    let view () =
        //initialization here.
        let initialBoard = Set [(0,0)]
        //produce a list of rectangles from a function inside DockPanel
        
        Component(fun ctx ->
            // define state (state = living), on state update -> update board
            let state = ctx.useState initialBoard
            //Button.onClick(fun _ -> living.Set(nextGeneration living.Current))
            
            DockPanel.create [
                DockPanel.children [
                    //Function accepts a list of drawing children
                    Canvas.create [
                        Canvas.dock Dock.Top
                        Canvas.width CONSTANTS.CanvasWidth
                        Canvas.height CONSTANTS.CanvasHeight
                        Canvas.background "yellow"
                        Canvas.children [
                            // boardFromLiving (accepts living, returns a list of rectangles)
                            Rectangle.create [
                                Rectangle.fill "blue"
                                Rectangle.width 63.0
                                Rectangle.height 41.0
                                Rectangle.left 40.0
                                Rectangle.top 31.0
                            ]
                            
                            Polyline.create [
                                Polyline.points [
                                    Point(0.0, 0.0)
                                    Point(65.0, 0.0)
                                    Point(78.0, -26.0)
                                    Point(91.0, 39.0)
                                    Point(104.0, -39.0)
                                    Point(117.0, 13.0)
                                    Point(130.0, 0.0)
                                    Point(195.0, 0.0)
                                ]
                                Polyline.stroke "Brown"
                                Polyline.strokeThickness 1.0
                                Polyline.left 30.0
                                Polyline.top 350.0
                            ]
                        ]
                    ]
                    Button.create [
                        Button.dock Dock.Bottom
                        Button.onClick (fun _ -> state.Set(state.Current))
                        Button.content "-"
                        Button.horizontalAlignment HorizontalAlignment.Stretch
                        Button.horizontalContentAlignment HorizontalAlignment.Center
                    ]
                    Button.create [
                        Button.dock Dock.Bottom
                        Button.onClick (fun _ -> state.Set(state.Current))
                        Button.content "+"
                        Button.horizontalAlignment HorizontalAlignment.Stretch
                        Button.horizontalContentAlignment HorizontalAlignment.Center
                    ]
                ]
            ]
        )

type MainWindow() =
    inherit HostWindow()
    do
        base.Title <- "Counter Example"
        base.Content <- Main.view ()

type App() =
    inherit Application()

    override this.Initialize() =
        this.Styles.Add (FluentTheme())
        this.RequestedThemeVariant <- Styling.ThemeVariant.Dark

    override this.OnFrameworkInitializationCompleted() =
        match this.ApplicationLifetime with
        | :? IClassicDesktopStyleApplicationLifetime as desktopLifetime ->
            desktopLifetime.MainWindow <- MainWindow()
        | _ -> ()

module Program =

    [<EntryPoint>]
    let main(args: string[]) =
        AppBuilder
            .Configure<App>()
            .UsePlatformDetect()
            .UseSkia()
            .StartWithClassicDesktopLifetime(args)