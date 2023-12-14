using NovelistBlazor.Common.Interface;

namespace NovelistBlazor.Common.DTO
{
    public class PlotUnitTypeDTO : IDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public PlotUnitTypeDTO()
        {
            Id = 0;
            Name = "Default Name";
        }

        public static IDTO Create()
        {
            return new PlotUnitTypeDTO();
        }
    }
}
