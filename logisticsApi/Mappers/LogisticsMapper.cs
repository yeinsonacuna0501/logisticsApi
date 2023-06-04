using AutoMapper;
using logisticsApi.Models;
using logisticsApi.Models.Dtos;

namespace logisticsApi.Mappers
{
    public class LogisticsMapper : Profile
    {
        public LogisticsMapper()
        { 
            CreateMap<Clientes, ClientesDto>().ReverseMap();
            CreateMap<Clientes, CrearClientesDto>().ReverseMap();
            CreateMap<Bodegas, BodegasDto>().ReverseMap();
            CreateMap<Puertos, PuertosDto>().ReverseMap();
            CreateMap<TiposProductos, TipoProductosDto>().ReverseMap();
            CreateMap<Bodegas, BodegasDto>().ReverseMap();
            CreateMap<LogisticaTerrestre, LogisticaTerrestreDto>().ReverseMap();
            CreateMap<LogisticaMaritima, LogisticaMaritimaDto>().ReverseMap();
            CreateMap<Usuarios, UsuariosDto>().ReverseMap();
            CreateMap<Usuarios, UsuariosRegistroDto>().ReverseMap();
            CreateMap<Usuarios, UsuariosLoginDto>().ReverseMap();

        }
    }
}
