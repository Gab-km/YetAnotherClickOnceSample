namespace YetAnotherClickOnceSample.Wpf.Test

open Persimmon
open UseTestNameByReflection

module WindowPrinterTest =

    open YetAnotherClickOnceSample.Wpf.Models
    
    let ``Debug can output Debug`` = test {
        let sut = OutputLevel.Debug
        do! assertPred <| sut.CanOutputDebug()
    }

    let ``Debug can output Info`` = test {
        let sut = OutputLevel.Debug
        do! assertPred <| sut.CanOutputInfo()
    }

    let ``Debug can outpu Error`` = test {
        let sut = OutputLevel.Debug
        do! assertPred <| sut.CanOutputError()
    }

    let ``Info cannot output Debug`` = test {
        let sut = OutputLevel.Info
        do! assertPred << not <| sut.CanOutputDebug()
    }

    let ``Info can output Info`` = test {
        let sut = OutputLevel.Info
        do! assertPred <| sut.CanOutputInfo()
    }

    let ``Info can output Error`` = test {
        let sut = OutputLevel.Info
        do! assertPred <| sut.CanOutputError()
    }

    let ``Error cannot output Debug`` = test {
        let sut = OutputLevel.Error
        do! assertPred << not <| sut.CanOutputDebug()
    }

    let ``Error cannot output Info`` = test {
        let sut = OutputLevel.Error
        do! assertPred << not <| sut.CanOutputInfo()
    }

    let ``Error can output Error`` = test {
        let sut = OutputLevel.Error
        do! assertPred <| sut.CanOutputError()
    }