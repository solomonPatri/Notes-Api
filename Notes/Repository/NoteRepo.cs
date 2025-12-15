using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notes_Api.Data;
using Notes_Api.Notes.Dtos;
using Notes_Api.Notes.Exceptions;
using Notes_Api.Notes.Model;

namespace Notes_Api.Notes.Repository
{
    public class NoteRepo : INoteRepo
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public NoteRepo(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetAllNotesDtos> getAllNotesByUserId(int iduser)
        {
            var notes = await _context.Notes
                .AsNoTracking()
                .Include(n => n.NoteCategories)
                .Where(n => n.UserId == iduser)
                .ToListAsync();

            var mapped = _mapper.Map<List<NoteResponse>>(notes ?? new List<Note>());

            return new GetAllNotesDtos
            {
                NotesList = mapped
            };
        }

        public async Task<NoteResponse?> getNoteById(int userid, int noteid)
        {
            var note = await _context.Notes
                .AsNoTracking()
                .Include(n => n.NoteCategories)
                .FirstOrDefaultAsync(n => n.UserId == userid && n.Id == noteid);

            return note == null ? null : _mapper.Map<NoteResponse>(note);
        }

        public async Task<NoteResponse> createNoteAsync(int iduser, NoteRequest request)
        {
            var duplicateTitle = await _context.Notes
                .AnyAsync(n => n.UserId == iduser && n.Title == request.Title);

            if (duplicateTitle)
            {
                throw new NotesAlreadyExistException();
            }

            var note = _mapper.Map<Note>(request);
            note.UserId = iduser;
            note.CreatedAt = DateTime.UtcNow;
            note.UpdatedAt = null;

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            await _context.Entry(note)
                .Collection(n => n.NoteCategories)
                .LoadAsync();

            return _mapper.Map<NoteResponse>(note);
        }

        public async Task<NoteResponse?> updateNoteAsync(int iduser, int noteId, NoteRequest request)
        {
            var note = await _context.Notes
                .Include(n => n.NoteCategories)
                .FirstOrDefaultAsync(n => n.UserId == iduser && n.Id == noteId);

            if (note == null)
            {
                return null;
            }

            var duplicateTitle = await _context.Notes
                .AnyAsync(n => n.UserId == iduser && n.Id != noteId && n.Title == request.Title);

            if (duplicateTitle)
            {
                throw new NotesAlreadyExistException();
            }

            note.Title = request.Title;
            note.Content = request.Content;
            note.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return _mapper.Map<NoteResponse>(note);
        }

        public async Task<bool> deleteNoteAsync(int userId, int noteid)
        {
            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.UserId == userId && n.Id == noteid);

            if (note == null)
            {
                return false;
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
