using NovelistBlazor.Common.Interface;

namespace NovelistBlazor.Common.Model
{
    public class PlotUnitType : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public PlotUnitType()
        {
            Id = 1;

            Name = "Free Hand Plot";
        }
    }
}
