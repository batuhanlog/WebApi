using FluentValidation;
using WebApi.Application.GenreOperations.Queries.GetGenresDetailQuery;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
    {

        public GetGenreDetailQueryValidator()
        {
            RuleFor(x => x.GenreId).GreaterThan(0);

        }

    }
}
