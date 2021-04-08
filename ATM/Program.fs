module ATM.Program

// Get access to the Bank Functions
open ATM.BankAccount
// Get access to the ATM Functions
open ATM.ATM

[<EntryPoint>]
let main argv =
    // Declaring the client's account with $15000 and pin 1234
    let ``The Client's Account`` = BankAccount(15000., "1234")
    printf "Welcome to the ATM. \nPlease enter your pin:\n> "
    
    // a recursive function. just think of it like a loop where the person is trying to access the account.
    let rec ``Try to access the account`` retries =
        // match function is like switch but with more freedom
        match retries with
        // each value after | is the statement to test. 
        // "| x" means assign retries to x and "when x < 3" is the test for true or false "->" means do the next indented lines
        | x when x < 3 ->
            let input = getPin()
            match input with
            | Some pin when ``The Client's Account``.checkPin(pin) ->
                // here i access the ATM
                ``The ATM`` ``The Client's Account``
            // | _ -> means ignore the value. kind of like switch's default.
            | _ ->
                printf "The entered pin is invalid. Please enter your pin again: \n> "
                ``Try to access the account`` <| retries + 1
        | _ -> 
            printfn "You have failed to access the account three times. You will now be disconnected."
    // start the loop. a recursive function needs to be called outside for it to start. otherwise nothing will happen.
    ``Try to access the account`` 0
    0 // return an integer exit code
