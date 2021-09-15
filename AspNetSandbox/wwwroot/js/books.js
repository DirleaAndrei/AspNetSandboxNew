"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/messagehub").build();

connection.on("BookCreated", function (book) {
    console.log(`BookCreated ${JSON.stringify(book)}`);
    $("tbody").append(`
        <tr id="${book.bookId}">
            <td>${book.bookTitle}</td>
            <td>${book.bookAuthor}</td>
            <td>${book.bookLanguage}</td>
            <td><button click="GetBookToEdit">Edit</button>
            <button click="DeleteBook">Delete</button></td>
        </tr>
    `)
});

connection.start().then(function () {
    console.log("Connection established.");
}).catch(function (err)
    {
        return console.error(err.toString());
    });
