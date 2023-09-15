namespace BasicViewLocatorSample.ViewModels

open Reactive.Bindings
open System
open System.ComponentModel.DataAnnotations

type SecondPageViewModel() as this =
    inherit PageViewModelBase()


    let mailAddress = new ReactiveProperty<string>()

    let password = new ReactiveProperty<string>()

    let mutable canNavigate = new ReactiveProperty<bool>()

    do
        mailAddress.SetValidateAttribute(fun _ -> this.MailAddress) |> ignore
        password.SetValidateAttribute(fun _ -> this.Password) |> ignore

        password.Subscribe(fun _ -> canNavigate.Value <- password.HasErrors = false && mailAddress.HasErrors = false)
        |> ignore

    override this.CanNavigateNext = canNavigate
    override this.CanNavigatePrevious = new ReactiveProperty<bool>(true)

    [<Required(ErrorMessage = "Email is required")>]
    [<EmailAddress>]
    member this.MailAddress = mailAddress


    [<Required(ErrorMessage = "Password is required")>]
    member this.Password = password
