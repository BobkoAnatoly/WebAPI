using Application.Common.Models;

namespace Application.BusinessLogic.Services.Interfaces
{
    public interface INoteServise
    {
        IEnumerable<NoteModel> GetNotes();
        NoteModel GetNoteById(int id);
        void ParseNotes();
    }
}
