using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService01
{
    public class AuthorService : Author.AuthorBase
    {
        private readonly ILogger<AuthorService> _logger;
        private List<AuthorResponse> authors;

        public AuthorService(ILogger<AuthorService> logger)
        {
            _logger = logger;
            authors = new List<AuthorResponse>();

            var authors01 = new AuthorResponse() { Name = "Antonio Gonzalez" };
            authors01.BooksAuthored.Add(new BookReply() { Title = "Much Ado about nothing" });
            authors01.BooksAuthored.Add(new BookReply() { Title = "How to do a split" });
            authors.Add(authors01);

            var authors02 = new AuthorResponse() { Name = "Jack Olabisi" };
            authors02.BooksAuthored.Add(new BookReply() { Title = "Early morning bird" });
            authors02.BooksAuthored.Add(new BookReply() { Title = "Fly me to Paris" });
            authors.Add(authors02);
        }

        public override Task<AuthorResponse> GetAuthor(AuthorRequest request, ServerCallContext context)
        {
            var author = authors.FirstOrDefault(x => x.Name.Contains(request.Name));
            return Task.FromResult(author);
        }
    }
}
