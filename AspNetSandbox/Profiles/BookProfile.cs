using AspNetSandbox.DTOs;
using AspNetSandbox.Models;
using AutoMapper;

namespace AspNetSandbox.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<CreateBookDto, Book>();

            CreateMap<Book, ReadBookDto>();
        }
    }
}
