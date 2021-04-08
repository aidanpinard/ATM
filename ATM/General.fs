// AutoOpen means I don't have to say open ATM.General like i have to do with ATM.BankAccount and ATM.ATM
[<AutoOpen>]
module ATM.General
// Just declaring random functions I use here.

// Possible options at the ATM
type ATMChoice = ``View Balance`` | ``Make a Deposit`` | ``Withdraw Money`` | Exit | Retry

// just an alias for defaultArg. F# allows for custom operators that you can make
// like <^> or ?|+|. Using custom operators can make the code look nicer sometimes
let (<=>) = defaultArg

// tests pin to make sure 4 numbers
let getPin() = 
    let input = System.Console.ReadLine()
    // uses Regular Expressions to see if the input is valid
    let isMatch = System.Text.RegularExpressions.Regex.Match(input, "[0-9]{4}").Success
    if isMatch then
        // if valid then say yes valid
        Some input
    else 
        // if invalid say got nothing
        None

 // get the key that the user pressed
let getKey() = 
    // read the key but dont show it
    let input = System.Console.ReadKey(true)
    // match the key with
    match input.Key with
    // 1 or numberpad 1
    | System.ConsoleKey.D1 | System.ConsoleKey.NumPad1 ->
        // if they press 1 they want their balance
        ``View Balance``
    // 2 or numberpad 2
    | System.ConsoleKey.D2 | System.ConsoleKey.NumPad2 ->
        // if they press 2 they want to deposit
        ``Make a Deposit``
    // 3 or numberpad 3
    | System.ConsoleKey.D3 | System.ConsoleKey.NumPad3 ->
        // if they press 3 they want to withdraw
        ``Withdraw Money``
    // 4 or numberpad 4
    | System.ConsoleKey.D4 | System.ConsoleKey.NumPad4 ->
        // if they press 4 they want to leave
        Exit
    | _ ->
        // anything else fails
        Retry

// wait for n seconds then ask them to continue
let waitToContinue (n:int) = 
    System.Threading.Thread.Sleep(n)
    printfn "\nPress any key to continue."
    getKey() |> ignore