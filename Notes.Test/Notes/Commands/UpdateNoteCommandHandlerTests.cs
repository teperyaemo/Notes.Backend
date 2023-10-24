using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Test.Common;

namespace Notes.Test.Notes.Commands
{
    public class UpdateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateNoteCommandHandler_Success()
        {
            //Arrange
            var handler = new UpdateNoteCommandHandler(context);
            var updatedTitle = "new Title";

            //Act
            await handler.Handle(
                new UpdateNoteCommand
                {
                    Title = updatedTitle,
                    Id = NotesContextFactory.NoteIdForUpdate,
                    UserId = NotesContextFactory.UserBId
                }, CancellationToken.None);

            //Assert
            Assert.NotNull(
               await context.Notes.SingleOrDefaultAsync(note =>
               note.Id == NotesContextFactory.NoteIdForUpdate
               && note.Title == updatedTitle));
        }

        [Fact]
        public async Task UpdateNoteCommandHandler_FailOnWrongId()
        {
            //Arrange
            var handler = new UpdateNoteCommandHandler(context);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateNoteCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = NotesContextFactory.UserAId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateNoteCommandHandler_FailOnWrongUserId()
        {
            //Arrange
            var handler = new UpdateNoteCommandHandler(context);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => 
                await handler.Handle(
                    new UpdateNoteCommand
                    {
                        Id = NotesContextFactory.NoteIdForUpdate,
                        UserId = NotesContextFactory.UserAId
                    },
                    CancellationToken.None));
        }
    }
}
