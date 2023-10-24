using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Persistance;
using Notes.Test.Common;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Test.Notes.Queries
{
    public class GetNoteListQueryHandlerTests
    {
        private readonly NotesDbContext context;
        private readonly IMapper mapper;

        public GetNoteListQueryHandlerTests(QueryTestFixture fixture)
        {
            context = fixture.context;
            mapper = fixture.mapper;
        }

        [Fact]
        public async void GetNoteListQueryHandler_Success()
        {
            //Arrange
            var handler = new GetNoteListQueryHandler(context, mapper);

            //Act
            var result = await handler.Handle(
                new GetNoteListQuery
                {
                    UserId = NotesContextFactory.UserBId
                },
                CancellationToken.None);

            //Assert
            result.ShouldBeOfType<NoteListVm>();
            result.Notes.Count.ShouldBe(2);
        }
    }
}
