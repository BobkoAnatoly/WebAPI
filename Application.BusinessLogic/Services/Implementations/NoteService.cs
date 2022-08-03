using Application.BusinessLogic.Services.Interfaces;
using Application.Common.Models;
using Application.Model;
using Application.Model.Models;
using Application.Parser;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.Services.Implementations
{
    public class NoteService : INoteServise
    {
        private readonly ApplicationDatabaseContext _context;
        private readonly IMapper _mapper;
        public NoteService(ApplicationDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public NoteModel GetNoteById(int id)
        {
            var note = _context.Notes.AsNoTracking().SingleOrDefault(x => x.Id == id);
            var noteModel = _mapper.Map<Note, NoteModel>(note);
            return noteModel; 
        }

        public IEnumerable<NoteModel> GetNotes()
        {
            var notes = _context.Notes.AsNoTracking().ToList();
            var notesModels = _mapper.Map<List<Note>, List<NoteModel>>(notes);
            return notesModels;
        }

        public void ParseNotes()
        {
            NotesParser parser = new NotesParser();
            var notesModelList = parser.Parse() as List<NoteModel>;
            if (notesModelList == null) throw new Exception();

            var notesList = _mapper.Map<List<NoteModel>,List<Note>>(notesModelList);
            _context.AddRange(notesList);
            _context.SaveChanges();
        }
    }
}
