namespace BasicViewLocatorSample.ViewModels

open Reactive.Bindings
open System.ComponentModel.DataAnnotations

type FirstPageViewModel() =
    inherit PageViewModelBase()

    member val Title = "Welcome to our Wizard-Sample"

    member val Message = """Press "Next" to register yourself."""

    override this.CanNavigateNext = new ReactiveProperty<bool>(true)
    override this.CanNavigatePrevious = new ReactiveProperty<bool>(false)
