using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperation;
using WebApi.Entities;
using Xunit;
using WebApi.UnitTests.TestsSetup;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture testfixture)
        {
            _context = testfixture.Context;
            _mapper = testfixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange
            var book = new Book() { Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount = 100, PublishDate = new DateTime(1990, 01, 10), GenreId = 1 };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel() { Title = book.Title };
            // Act

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book already exists");


        }
             [Fact]
            public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
            {
                CreateBookCommand command = new CreateBookCommand(_context, _mapper);
                CreateBookModel model = new CreateBookModel() { Title = "Hobbit", PageCount = 100, PublishDate = DateTime.Now.Date.AddYears(-1), GenreId = 1 };
                command.Model = model;

                FluentActions.Invoking(() => command.Handle()).Invoke();

                var book = _context.Books.SingleOrDefault(book => book.Title == model.Title);
                book.Should().NotBeNull();
                book.PageCount.Should().Be(model.PageCount);
                book.PublishDate.Date.Should().Be(model.PublishDate);
                book.GenreId.Should().Be(model.GenreId);
            }
        }
    }
