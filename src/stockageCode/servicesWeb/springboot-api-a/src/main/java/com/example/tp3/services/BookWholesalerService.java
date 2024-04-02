package com.example.tp3.services;

import com.example.tp3.exception.EntityResponseException;
import com.example.tp3.model.Books;
import com.example.tp3.repository.BookRepository;
import org.springframework.stereotype.Service;
import org.springframework.web.bind.annotation.*;
import java.util.Optional;

@Service
public class BookWholesalerService {
    private final BookRepository bookRepository;

    public BookWholesalerService(BookRepository r){
        this.bookRepository= r;
    }

    public Optional<Books> order(@PathVariable Long isbn) throws EntityResponseException {
        try{
            Optional<Books> b = bookRepository.findById(isbn);
            bookRepository.deleteById(isbn);
            return b;
        } catch (Exception e){
            throw new EntityResponseException("Impossible de trouver le livre avec l'id donné.");
        }
    }
}
