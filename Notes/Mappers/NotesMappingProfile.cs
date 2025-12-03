using System.Linq;
using AutoMapper;
using Notes_Api.NoteCategories.Model;
using Notes_Api.Notes.Dtos;
using Notes_Api.Notes.Model;

namespace Notes_Api.Notes.Mappers
{
    public class NotesMappingProfile:Profile
    {

          public NotesMappingProfile()
        {

            CreateMap<NoteRequest, Note>();

            CreateMap<Note, NoteResponse>()
                .ForMember(dest => dest.Categories,
                    opt => opt.MapFrom(src => src.NoteCategories != null
                        ? src.NoteCategories.Select(nc => nc.Category)
                        : Enumerable.Empty<CategoryType>()));





        }

            









    }
}
