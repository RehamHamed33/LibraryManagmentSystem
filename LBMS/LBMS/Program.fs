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

let addBook title author genre =
    if String.IsNullOrWhiteSpace(title) || String.IsNullOrWhiteSpace(author) || String.IsNullOrWhiteSpace(genre) then
        MessageBox.Show("Please fill in all fields (Title, Author, Genre).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        |> ignore
    elif libraryBooks.ContainsKey(title) then
        MessageBox.Show("A book with this title already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        |> ignore
    else
        let newBook =
            { Title = title
              Author = author
              Genre = genre
              IsBorrowed = false
              BorrowDate = None
              Borrower = "" }

        libraryBooks <- libraryBooks.Add(title, newBook)

        MessageBox.Show("Book added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        |> ignore


let searchBook title =
    match libraryBooks.TryFind title with
    | Some book ->
        let status = if book.IsBorrowed then "Borrowed" else "Available"

        MessageBox.Show(
            $"Title: {book.Title}\nAuthor: {book.Author}\nGenre: {book.Genre}\nStatus: {status}",
            "Book Found",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        )
        |> ignore
    | None ->
        MessageBox.Show("Book not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        |> ignore


let borrowBook title borrower =
    if String.IsNullOrWhiteSpace(borrower) then
        MessageBox.Show("Please provide a borrower's name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        |> ignore
    else
        match libraryBooks.TryFind title with
        | Some book when not book.IsBorrowed ->
            let updatedBook =
                { book with
                    IsBorrowed = true
                    BorrowDate = Some DateTime.Now
                    Borrower = borrower }

            libraryBooks <- libraryBooks.Add(title, updatedBook)

            MessageBox.Show("Book borrowed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            |> ignore
        | Some _ -> 
            MessageBox.Show("The book is already borrowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            |> ignore
        | None -> 
            MessageBox.Show("Book not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            |> ignore

// Windows Forms UI
let form = new Form(Text = "Library Management System", Width = 600, Height = 600)



[<EntryPoint>]
let main _ =
    Application.Run(form)
    0
