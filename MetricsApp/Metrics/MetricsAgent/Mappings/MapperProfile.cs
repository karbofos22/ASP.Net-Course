using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;

namespace MetricsAgent.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetricCreateRequest, CpuMetric>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<CpuMetric, CpuMetricDto>();

            CreateMap<DotnetMetricCreateRequest, DotnetMetric>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<DotnetMetric, DotnetMetricDto>();

            CreateMap<HddMetricCreateRequest, HddMetric>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<HddMetric, HddMetricDto>();

            CreateMap<NetworkMetricCreateRequest, NetworkMetric>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<NetworkMetric, NetworkMetricDto>();

            CreateMap<RamMetricCreateRequest, RamMetric>()
                .ForMember(x => x.Time,
                opt => opt.MapFrom(src => (long)src.Time.TotalSeconds));

            CreateMap<RamMetric, RamMetricDto>();
        }
    }
}
