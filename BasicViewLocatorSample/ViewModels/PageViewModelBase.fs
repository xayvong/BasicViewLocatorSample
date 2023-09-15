namespace BasicViewLocatorSample.ViewModels

open Reactive.Bindings
open System

[<AbstractClass>]
type PageViewModelBase() =
    inherit ViewModelBase()

    abstract member CanNavigateNext: IObservable<bool> with get

    abstract member CanNavigatePrevious: IObservable<bool> with get
