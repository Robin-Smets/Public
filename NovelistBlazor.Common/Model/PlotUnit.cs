using NovelistBlazor.Common.Interface;

namespace NovelistBlazor.Common.Model
{
    public class PlotUnit : IEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Premise { get; set; }
        public string Location { get; set; }

        public int PlotUnitTypeId { get; set; }
        public PlotUnitType PlotUnitType { get; set; }
        public int NovelId { get; set; }
        public Novel Novel { get; set; }

        public PlotUnit() 
        { 
            Id = 0;

            Title = "Default Title";
            Description = "Default Description";
            Premise = "Default Premise";
            Location = "Default Location";

            PlotUnitTypeId = 0;
            PlotUnitType = new PlotUnitType();
            NovelId = 0;
            Novel = new Novel();    
        }
    }
}
