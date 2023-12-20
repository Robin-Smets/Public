using NovelistBlazor.Common.Interface;
using NovelistBlazor.Common.Model;
using NovelistBlazor.Common.Service;

namespace NovelistBlazor.Common.DTO
{
    public class CharacterDTO : IDTO
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

        private int _age;
        public int Age 
        { 
            get => _age;
            set
            {
                _age = value;
            }  
        }

        private string _occupation;
        public string Occupation 
        { 
            get => _occupation;
            set
            {
                _occupation = value;
            }  
        }

        private string _roleInStory;
        public string RoleInStory 
        { 
            get => _roleInStory;
            set
            {
                _roleInStory = value;
            }  
        }

        private string _physicalDescription;
        public string PhysicalDescription 
        { 
            get => _physicalDescription;
            set
            {
                _physicalDescription = value;
            }  
        }

        private string _personalityTraits;
        public string PersonalityTraits 
        { 
            get => _personalityTraits;
            set
            {
                _personalityTraits = value;
            }  
        }

        private string _background;
        public string Background 
        { 
            get => _background;
            set
            {
                _background = value;
            }  
        }

        private string _goalsAndMotivations;
        public string GoalsAndMotivations 
        { 
            get => _goalsAndMotivations;
            set
            {
                _goalsAndMotivations = value;
            }  
        }

        private string _characterArc;
        public string CharacterArc 
        { 
            get => _characterArc;
            set
            {
                _characterArc = value;
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
        
        public CharacterDTO()
        {
            _id = 0;
            
            _name = "New Character";
            _age = 0;
            _occupation = "";
            _roleInStory = "";
            _physicalDescription = "";
            _personalityTraits = "";
            _background = "";
            _goalsAndMotivations = "";
            _characterArc = "";

            _novelId = 0;
        }
    }
}
