using TrackIT.DTO.Dtos.LocationDtos;

namespace TrackIT.UI.ViewModels
{
    public class LocationViewModel
    {
        public List<LocationGetDto>? Locations { get; set; }
        public LocationUpdateDto? LocationUpdate { get; set; }
        public LocationAddDto? LocationAdd { get; set; }
    }
}
