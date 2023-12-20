using NovelistBlazor.Common.Interface;
using NovelistBlazor.Common.Service;

namespace NovelistBlazor.Common.DTO
{
    public class PlotUnitTypeDTO : IDTO
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

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }

        public PlotUnitTypeDTO()
        {
            _id = 1;
            _name = "Free Hand Plot";
        }
    }
}
