using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Persistance;

namespace Notes.Test.Common
{
    public class QueryTestFixture : IDisposable
    {
        public NotesDbContext context;
        public IMapper mapper;

        public QueryTestFixture()
        {
            context = NotesContextFactory.Create();
            var configurationProvider = new MapperConfiguration(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(
                    typeof(INotesDbContext).Assembly));
            });
            mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            NotesContextFactory.Destroy(context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
