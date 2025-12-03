using AutoMapper;
using Notes_Api.Notes.Dtos;
using Notes_Api.Notes.Model;

namespace Notes_Api.Notes.Mappers
{
    public class NotesMappingProfile:Profile
    {

          public NotesMappingProfile()
        {

            CreateMap<NoteRequest, Note>();

            CreateMap<Note, NoteResponse>();


            CreateMap<List<NoteResponse>, GetAllNotesDtos>();           





        }

            









    }
}
