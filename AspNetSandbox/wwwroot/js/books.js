"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/messagehub").build();

connection.on("BookCreated", function (book) {
    console.log(`BookCreated ${JSON.stringify(book)}`);
    $("tbody").append(`
        <tr id="${book.bookId}">
            <td>${book.bookTitle}</td>
            <td>${book.bookAuthor}</td>
            <td>${book.bookLanguage}</td>
            <td>
                <button class="edit" onclick="GetBookToEdit()">Edit</button>
                <button class="delete" onclick="DeleteBook()">Delete</button>
            </td>
        </tr>
    `)
});

connection.on("GetBooks", function (books) {
    console.log(`BookCreated ${JSON.stringify(books)}`);
    //books.forEach(book => {
    //    $("tbody").append(`
    //    <tr id="${book.bookId}">
    //        <td>${book.bookTitle}</td>
    //        <td>${book.bookAuthor}</td>
    //        <td>${book.bookLanguage}</td>
    //        <td>
    //            <button class="edit" onclick="GetBookToEdit()">Edit</button>
    //            <button class="delete" onclick="DeleteBook()">Delete</button>
    //        </td>
    //    </tr>
    //`)
    //})
});

connection.on("BookDeleted", function (book) {
    console.log(`BookCreated ${JSON.stringify(book)}`);

});

connection.on("UpdatedBook", function (book) {
    console.log(`UpdatedBook ${JSON.stringify(book)}`);

});

connection.start().then(function () {
    console.log("Connection established.");
}).catch(function (err)
    {
        return console.error(err.toString());
    });
