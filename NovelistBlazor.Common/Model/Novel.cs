using NovelistBlazor.Common.Interface;

namespace NovelistBlazor.Common.Model
{
    public class Novel : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Abstract { get; set; }

        public ICollection<PlotUnit> PlotUnits { get; set; }
        public ICollection<Character> Characters { get; set; }

        public Novel()
        {
            Id = 0;

            Name = "Name";
            Description = "Description";
            Abstract = "Abstract";

            PlotUnits = new List <PlotUnit>();
            Characters = new List <Character>();
        }

    }
}
