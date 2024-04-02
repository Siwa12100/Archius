package com.example.tp3.model;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;

@Entity
public class Books {
    private String name;
    private String autor;
    private String resume;

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Long id;

    public Books(String name, String autor, String resume){
        this.name = name;
        this.autor = autor;
        this.resume = resume;
    }

    public Books() {}


    public void setName(String name) {
        this.name = name;
    }

    public String getName() {
        return name;
    }

    public String getAutor() {
        return autor;
    }

    public void setAutor(String autor) {
        this.autor = autor;
    }

    public String getResume() {
        return resume;
    }

    public void setResume(String resume) {
        this.resume = resume;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Long getId() {
        return id;
    }
}
