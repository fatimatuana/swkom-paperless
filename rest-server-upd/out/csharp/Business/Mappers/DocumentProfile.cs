using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Mappers
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<Document, DocumentTypeDto>()
                .ForMember(dest => dest.DocumentExtension, op => op.MapFrom(src => GetExtension(src.Title)));
        }

        public static string GetExtension(string name)
        {
            int index = name.LastIndexOf(".");

            if (index == -1)
                return "";

            if (index == name.Length - 1)
                return "";

            return name.Substring(index);
        }
    }
}
