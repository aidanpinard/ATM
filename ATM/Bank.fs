// F# doesn't have classes in the same way as Java. Code is separated into modules and namespaces. 
//If you ever encounter C#, its the same way. F# and C# are largely interoperable.
// Here, ATM is the namespace, it encompasses the project. BankAccount is the module.
module ATM.BankAccount

// A custom type. Types are used for many things in F#, from classes to discriminated unions to 
//records to interfaces (don't worry too much about those)

/// <summary> BankAccount is a class that represents an account, containing a <c>balance</c> and accessible with a <c> pin </c>. </summary>
/// <typeparam name="?startingBalance"> The starting balance. If no starting balance is given, 0 is set as the balance. </typeparam>
/// <typeparam name="userPin"> The account pin. If no pin is given, 0000 is set as the pin. </typeparam> 
type BankAccount(?startingBalance, ?userPin) =
    // Declaring Account values. No need to declare its type or as private. 
    // F# infers the type from its usage and all values are private by default.
    // The values are optional. I use the <=> operator to assign a default value (Think of it as if not left then right).
    let AccountBalance = startingBalance <=> 0.
    let AccountPin = userPin <=> "0000"

    /// <summary> Returns the balance of the account </summary>
    member this.balance =
        sprintf "$%.2f" AccountBalance

    /// <summary> Compares a given pin to the account pin </summary>
    /// <typeparam name="pin"> The pin to compare </typeparam>
    member this.checkPin pin =
        AccountPin = pin

    /// <summary> Attempts to withdraw an amount from this account </summary>
    /// <typeparam name="amount> The amount to withdraw </amount>
    /// <returns> An optional edited account </returns>
    member this.tryWithdraw amount = 
        match AccountBalance with
        | balance when amount > balance -> 
            None
        | balance ->
             Some <| BankAccount(balance - amount)

    /// <summary> Attempts to withdraw an amount from this account </summary>
    /// <typeparam name="amount> The amount to deposit </amount>
    /// <returns> The account after the deposit </returns
    member this.deposit amount = 
        BankAccount(AccountBalance + amount)

    
