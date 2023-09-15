namespace BasicViewLocatorSample.ViewModels

open Reactive.Bindings
open System
open System.ComponentModel.DataAnnotations

type ThirdPageViewModel() =
    inherit PageViewModelBase()

    member val Message = "Done"

    override this.CanNavigateNext = new ReactiveProperty<bool>(false)
    override this.CanNavigatePrevious = new ReactiveProperty<bool>(true)
