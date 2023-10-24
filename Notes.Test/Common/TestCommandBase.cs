using Notes.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Test.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly NotesDbContext context;

        public TestCommandBase()
        {
            context = NotesContextFactory.Create();
        }

        public void Dispose()
        {
            NotesContextFactory.Destroy(context);
        }
    }
}
