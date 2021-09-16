"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/messagehub").build();

connection.on("BookCreated", function (book) {
    console.log(`BookCreated ${JSON.stringify(book)}`);
    $("#tb").append(`
        <tr id="${book.bookId}">
            <td>${book.bookTitle}</td>
            <td>${book.bookAuthor}</td>
            <td>${book.bookLanguage}</td>
            <td>
                <button class="edit" onclick="GetBookToEdit(${book.bookId})">Edit</button>
                <button class="delete" onclick="DeleteBook(${book.bookId})">Delete</button>
            </td>
        </tr>
    `)
    $("#add-book-button").css("display", "none");
    $("#edit-book-button").css("display", "block");
    $("#book-title").val("");
    $("#book-author").val("");
    $("#book-language").val("");
});

connection.on("GetBooks", function (books) {
    console.log(`AllBooks ${JSON.stringify(books)}`);
    $("#tb").empty();
    books.forEach(book => {
        $("#tb").append(`
        <tr id="${book.bookId}">
            <td>${book.bookTitle}</td>
            <td>${book.bookAuthor}</td>
            <td>${book.bookLanguage}</td>
            <td>
                <button class="edit" onclick="GetBookToEdit(${book.bookId})">Edit</button>
                <button class="delete" onclick="DeleteBook(${book.bookId})">Delete</button>
            </td>
        </tr>
    `)
    })
});

connection.on("BookDeleted", function (book) {
    console.log(`BookDeleted ${JSON.stringify(book)}`);
    $(`#${book.bookId}`).remove();
});

connection.on("SpecificBook", function (book) {
    console.log(`SpecificBook ${JSON.stringify(book)}`);
    $("#book-title").val(book.bookTitle);
    $("#book-author").val(book.bookAuthor);
    $("#book-language").val(book.bookLanguage);
    $("#add-book-button").css("display", "none");
    $("#edit-book-button").css("display", "block");
});

connection.on("UpdatedBook", function (book) {
    console.log(`UpdatedBook ${JSON.stringify(book)}`);
    $(`#${book.bookId}`).remove();
    $("#tb").append(`
        <tr id="${book.bookId}">
            <td>${book.bookTitle}</td>
            <td>${book.bookAuthor}</td>
            <td>${book.bookLanguage}</td>
            <td>
                <button class="edit" onclick="GetBookToEdit(${book.bookId})">Edit</button>
                <button class="delete" onclick="DeleteBook(${book.bookId})">Delete</button>
            </td>
        </tr>
    `)
    $("#add-book-button").css("display", "block");
    $("#edit-book-button").css("display", "none");
    $("#book-title").val("");
    $("#book-author").val("");
    $("#book-language").val("");
});

connection.start().then(function () {
    console.log("Connection established.");
}).catch(function (err)
    {
        return console.error(err.toString());
    });
