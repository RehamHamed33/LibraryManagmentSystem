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

let displayBooks (dataGridView: DataGridView) =
    dataGridView.Rows.Clear()

    libraryBooks
    |> Map.iter (fun _ book ->
        let status = if book.IsBorrowed then "Borrowed" else "Available"
        let borrowDate = 
            match book.BorrowDate with
            | Some date -> date.ToShortDateString()
            | None -> ""

        let row = dataGridView.Rows.Add()
        dataGridView.Rows.[row].Cells.["Title"].Value <- book.Title
        dataGridView.Rows.[row].Cells.["Author"].Value <- book.Author
        dataGridView.Rows.[row].Cells.["Genre"].Value <- book.Genre
        dataGridView.Rows.[row].Cells.["Status"].Value <- status
        dataGridView.Rows.[row].Cells.["Borrower"].Value <- book.Borrower
        dataGridView.Rows.[row].Cells.["Borrow Date"].Value <- borrowDate)


// Windows Forms UI
let form = new Form(Text = "Library Management System", Width = 600, Height = 600)



[<EntryPoint>]
let main _ =
    Application.Run(form)
    0
