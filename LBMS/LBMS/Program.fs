open System
open System.Windows.Forms

type Book =
    { Title: string
      Author: string
      Genre: string
      IsBorrowed: bool
      BorrowDate: DateTime option
      Borrower: string }


let mutable libraryBooks = Map.empty<string, Book>




// Windows Forms UI
let form = new Form(Text = "Library Management System", Width = 600, Height = 600)



[<EntryPoint>]
let main _ =
    Application.Run(form)
    0
