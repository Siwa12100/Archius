package com.example.tp3.controller;

import com.example.tp3.exception.EntityResponseException;
import com.example.tp3.model.Books;
import com.example.tp3.repository.BookRepository;
import com.example.tp3.services.BookWholesalerService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.net.URI;
import java.util.List;
import java.util.Optional;

@RestController
public class BookController {
    private final BookRepository repository;
    private final BookWholesalerService bookService;

    @Autowired
    public BookController(BookRepository bookRepository){
        this.repository = bookRepository;
        this.bookService = new BookWholesalerService(this.repository);
        this.repository.save(new Books("Harry Potter", "J,K,Rolling", "Un enfant..."));
    }

    // GET
    @GetMapping("/")
    public String home(){
        return "Hello World";
    }

    // GET
    @RequestMapping("/books")
    public List<Books> getAllBooks()
    {
        return (List<Books>) repository.findAll();
    }

    // GET
    @RequestMapping("/books/{id}")
    public Optional<Books> getBookById(@PathVariable Long id){
        return repository.findById(id);
    }

    // POST
    @PostMapping(value = "/books/add",
            consumes = {MediaType.APPLICATION_JSON_VALUE},
            produces = {MediaType.APPLICATION_JSON_VALUE})

    public ResponseEntity<Books> addBook(@RequestBody Books books) {
        Books persistentBook = repository.save(books);
        return ResponseEntity
                .created(URI
                        .create(String.format("/books/%s", books.getId())))
                .body(persistentBook);
    }

    // UPDATE
    @PutMapping("/books/{id}")
    public Books replaceBook(@RequestBody Books newBooks, @PathVariable Long id) {
        return repository.findById(id)
                .map(book -> {
                    book.setName(newBooks.getName());
                    book.setAutor(newBooks.getAutor());
                    book.setResume(newBooks.getResume());
                    return repository.save(book);
                })
                .orElseGet(() -> {
                    newBooks.setId(id);
                    return repository.save(newBooks);
                });
    }

    // DELETE
    @DeleteMapping("/books/{id}")
    void deleteBook(@PathVariable Long id) {
        repository.deleteById(id);
    }

    // TP5
    // GETSTOCK
    @RequestMapping("/service/stock")
    public List<Books> getStock()
    {
        return (List<Books>) repository.findAll();
    }


    // ORDER
    @PutMapping("/service/order/{isbn}")
    public Optional<Books> order(@PathVariable Long isbn) throws EntityResponseException {
        return bookService.order(isbn);
    }
}
