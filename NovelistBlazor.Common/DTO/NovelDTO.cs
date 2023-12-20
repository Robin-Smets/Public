using NovelistBlazor.Common.Interface;
using NovelistBlazor.Common.Service;

namespace NovelistBlazor.Common.DTO
{
    public class NovelDTO : IDTO
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

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
            }
        }

        private string _abstract;
        public string Abstract
        {
            get => _abstract;
            set
            {
                _abstract = value;
            }
        }

        public NovelDTO()
        {

            _id = 0;
            _name = "New novel";
            _description = "";
            _abstract = ""; 
        }
    }
}
