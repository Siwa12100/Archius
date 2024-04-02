package com.example.tp3.exception;

import org.hibernate.service.spi.ServiceException;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.ResponseStatus;

@ResponseStatus(value = HttpStatus.SERVICE_UNAVAILABLE)
public class EntityResponseException extends ServiceException {
        public EntityResponseException(String message) {
            super(message);
        }
}
