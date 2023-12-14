using NovelistBlazor.Common.Interface;

namespace NovelistBlazor.Common.DTO
{
    public class PlotUnitDTO : IDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Premise { get; set; }
        public string Location { get; set; }
        public int PlotUnitTypeId { get; set; }
        public int NovelId { get; set; }

        public PlotUnitDTO()
        {
            Id = 0;
            Title = "Default Title";
            Description = "Default Description";
            Premise = "Default Premise";
            Location = "Default Location";
            PlotUnitTypeId = 0;
            NovelId = 0;
        }

        public static IDTO Create()
        {
            return new PlotUnitDTO();
        }
    }
}
