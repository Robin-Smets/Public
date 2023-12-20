using NovelistBlazor.Common.Interface;
using NovelistBlazor.Common.Service;

namespace NovelistBlazor.Common.DTO
{
    public class PlotUnitDTO : IDTO
    {
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
            }
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
            }
        }

        private string _premise;
        public string Premise
        {
            get => _premise;
            set
            {
                _premise = value;
            }
        }

        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
            }
        }

        private int _plotUnitTypeId;
        public int PlotUnitTypeId
        {
            get => _plotUnitTypeId;
            set
            {
                _plotUnitTypeId = value;
            }
        }

        private int _novelId;
        public int NovelId
        {
            get => _novelId;
            set
            {
                _novelId = value;
            }
        }

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
    }
}
