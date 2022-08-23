using Application.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteServise _noteService;
        public NotesController(INoteServise noteServise)
        {
            _noteService = noteServise;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            var notesCollection = _noteService.GetNotes();
            if (notesCollection == null) return BadRequest(notesCollection);

            return Ok(notesCollection);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var note = _noteService.GetNoteById(id);
            if (note == null) return BadRequest(note);
            return Ok(note);
        }
        [HttpPost]
        public IActionResult Post()
        {
            _noteService.ParseNotes();
            return Ok();
        }

    }
}
