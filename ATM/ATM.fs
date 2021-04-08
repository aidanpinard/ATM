module ATM.ATM

open ATM.BankAccount

// The ATM
let ``The ATM`` accountToAccess =
    let rec ``ATM Loop`` (account: BankAccount) = 
        // clear the screen
        System.Console.Clear()

        // ask input
        printfn "Please select from the following options: 
                1- View Balance
                2- Make a Deposit
                3- Withdraw Cash
                4- Exit"

        // get their input
        let choice = getKey()

        // do what they wanted
        match choice with
        // if they chose view balance
        | ``View Balance`` ->
            // print balance and wait to continue
            printfn "Your current balance is: %s" <| account.balance
            waitToContinue 1000
            // enter the loop again
            ``ATM Loop`` account
        
        // if make a deposiit
        | ``Make a Deposit`` ->
            printf "How much do you wish to deposit?\n\t$"
            // get how much they want to deposit
            let deposit = System.Console.ReadLine()
            // check if its a number
            match System.Double.TryParse(deposit) with
            // if not then fail
            | (false, _) ->
                printfn "You have not entered a valid amount to deposit."
                waitToContinue 1000
                ``ATM Loop`` account

            // if yes then deposit
            | (true, depositValue) ->
                printfn "$%.2f has been deposited into your account." depositValue
                waitToContinue 1000
                // enter the loop with the new account value
                ``ATM Loop`` <| account.deposit depositValue
        // if withdraw money
        | ``Withdraw Money`` ->
            printf "How much do you wish to withdraw?\n\t$"
            let ``Amount to withdraw`` = System.Console.ReadLine()
            // same as deposit with checking
            match System.Double.TryParse(``Amount to withdraw``) with
            | (false, _) ->
                printfn "You have not entered a valid amount to withdraw."
                waitToContinue 1000
                ``ATM Loop`` account

            | (true, withdrawal) ->
                // attempt to withdraw from the account
                let ``Updated account`` = account.tryWithdraw withdrawal
                match ``Updated account`` with
                // if it succeeds then take out the money
                | Some newAccount ->
                    printfn "$%.2f has been withdrawn from your account." withdrawal
                    waitToContinue 1000
                    // enter loop with new account
                    ``ATM Loop`` newAccount

                // if it fails they dont have enough money
                | None ->
                    printfn "You do not have sufficient funds to withdraw $%.2f from your account." withdrawal
                    waitToContinue 1000
                    ``ATM Loop`` account

        // just print goodbye and stop (note it ends with > () < and not > ``ATM Loop`` account <
        // this means it won't enter the loop again and it would return nothing
        // in F# all functions return something. () is called unit and it is used whenever no particular
        // input or output of a function is needed
        | Exit ->
            printfn "Thank you for using THE ATM. Have a nice life."
            ()

        // if no proper key pressed then try again
        | Retry ->
            printfn "No valid Input Detected, please try again."
            waitToContinue 1000
            ``ATM Loop`` account

    // start the loop
    ``ATM Loop`` accountToAccess


