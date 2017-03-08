var ViewModel = function () {
    var self = this;
    self.books = ko.observableArray();
    self.error = ko.observable();

    var booksUri = "/api/libros/";

    function ajaxHelper(uri, method, data) {
        self.error("");
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllBooks() {
        ajaxHelper(booksUri, "GET").done(function (data) {
            self.books(data);
        });
    }

    getAllBooks();

    self.detail = ko.observable();
    self.getBookDetail = function (item) {
        ajaxHelper(booksUri + item.Id, "GET").done(function (data) {
            self.detail(data);
        });
    }

    self.authors = ko.observableArray();
    self.newBook = {
        Autor: ko.observable(),
        Genero: ko.observable(),
        Precio: ko.observable(),
        Titulo: ko.observable(),
        Anio: ko.observable()
    }
    var authorsUri = "/api/autores/";
    function getAuthors() {
        ajaxHelper(authorsUri, "GET").done(function (data) {
            self.authors(data);
        });
    }
    self.addBook = function (formElement) {
        var book = {
            AutorId: self.newBook.Autor().Id,
            Genero: self.newBook.Genero(),
            Precio: self.newBook.Precio(),
            Titulo: self.newBook.Titulo(),
            Anio: self.newBook.Anio()
        };

        ajaxHelper(booksUri, 'POST', book).done(function (item) {
            self.books.push(item);
        });
    }

    getAuthors();
};
ko.applyBindings(new ViewModel());