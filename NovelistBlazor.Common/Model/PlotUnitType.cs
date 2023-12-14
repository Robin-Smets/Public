using NovelistBlazor.Common.Interface;

namespace NovelistBlazor.Common.Model
{
    public class PlotUnitType : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public PlotUnitType()
        {
            Id = 0;

            Name = "Default Name";
        }
    }
}
