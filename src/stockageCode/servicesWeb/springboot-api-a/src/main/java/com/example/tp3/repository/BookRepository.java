package com.example.tp3.repository;


import com.example.tp3.model.Books;
import org.springframework.data.repository.CrudRepository;

public interface BookRepository extends CrudRepository<Books, Long> {
}
