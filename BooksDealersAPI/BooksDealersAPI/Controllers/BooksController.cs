﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksDealersAPI.FrontendModels;
using BooksDealersAPI.Models;
using BooksDealersAPI.Repository;
using BooksDealersAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BooksDealersAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/books")]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(
                IBookService bookService
            )
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult Books()
        {
            return Ok(_bookService.GetAllBooks());
        }

        [HttpGet("user-books/{id}")]
        public IActionResult UserBooks(int id)
        {
            return Ok(_bookService.GetAllBooksByOwner(id));
        }

        [HttpGet("{id}")]
        public IActionResult Book(int id)
        {
            return Ok(_bookService.GetBook(id));
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] BookAddModel book)
        {
            bool created = _bookService.AddBook(book);
            if (created)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook([FromBody] BookViewModel book, int id)
        {
            bool updated = _bookService.UpdateBook(book);
            if (updated)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            bool deleted = _bookService.DeleteBook(id);
            if (deleted)
            {
                return Ok();
            }
            return NotFound();
        }
    }
    
}
