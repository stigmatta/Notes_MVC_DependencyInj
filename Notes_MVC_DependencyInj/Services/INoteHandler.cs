using Notes_MVC_DependencyInj.Models;

namespace Notes_MVC_DependencyInj.Services
{
    public interface INoteHandler
    {
        public Task Save(Note note);
        public Task<List<Note>> Load();
    }
}
