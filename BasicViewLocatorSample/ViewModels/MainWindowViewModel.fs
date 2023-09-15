namespace BasicViewLocatorSample.ViewModels

open Reactive.Bindings
open System.Reactive.Disposables

type MainWindowViewModel() =
    inherit ViewModelBase()

    let currentPageIndex = new ReactiveProperty<int>(0)
    let canNavNext = new ReactiveProperty<bool>()
    let canNavPrev = new ReactiveProperty<bool>()
    let mutable canNavNextDisposable = Disposable.Empty
    let mutable canNavPrevDisposable = Disposable.Empty

    let navigateNextCommand = new ReactiveCommand()
    let navigatePreviousCommand = new ReactiveCommand()

    let firstPage = FirstPageViewModel()
    let secondPage = SecondPageViewModel()
    let thirdPage = ThirdPageViewModel()

    let pages: PageViewModelBase[] = [| firstPage; secondPage; thirdPage |]

    let currentPage = new ReactiveProperty<PageViewModelBase>(firstPage)

    do
        navigateNextCommand.Subscribe(fun _ ->
            canNavNextDisposable.Dispose()
            canNavPrevDisposable.Dispose()
            currentPageIndex.Value <- currentPageIndex.Value + 1
            currentPage.Value <- pages[currentPageIndex.Value]

            canNavNextDisposable <-
                currentPage.Value.CanNavigateNext.Subscribe(fun canNav -> canNavNext.Value <- canNav)

            canNavPrevDisposable <-
                currentPage.Value.CanNavigatePrevious.Subscribe(fun canNav -> canNavPrev.Value <- canNav))
        |> ignore

        navigatePreviousCommand.Subscribe(fun _ ->
            canNavNextDisposable.Dispose()
            canNavPrevDisposable.Dispose()
            currentPageIndex.Value <- currentPageIndex.Value - 1
            currentPage.Value <- pages.[currentPageIndex.Value]

            canNavNextDisposable <-
                currentPage.Value.CanNavigateNext.Subscribe(fun canNav -> canNavNext.Value <- canNav)

            canNavPrevDisposable <-
                currentPage.Value.CanNavigatePrevious.Subscribe(fun canNav -> canNavPrev.Value <- canNav))
        |> ignore


        canNavNextDisposable <- currentPage.Value.CanNavigateNext.Subscribe(fun canNav -> canNavNext.Value <- canNav)

        canNavPrevDisposable <-
            currentPage.Value.CanNavigatePrevious.Subscribe(fun canNav -> canNavPrev.Value <- canNav)



    member this.CurrentPage = currentPage

    member this.CanNavNext = canNavNext
    member this.CanNavPrevious = canNavPrev

    member this.NavigateNext = navigateNextCommand
    member this.NavigatePrevious = navigatePreviousCommand
