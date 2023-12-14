using NovelistBlazor.Common.Interface;

namespace NovelistBlazor.Common.Model
{
    public class Character : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }
        public string RoleInStory { get; set; }
        public string PhysicalDescription { get; set; }
        public string PersonalityTraits { get; set; }
        public string Background { get; set; }
        public string GoalsAndMotivations { get; set; }
        public string CharacterArc { get; set; }

        public int NovelId { get; set; }
        public Novel Novel { get; set; }

        public Character()
        {
            Id = 0;

            Name = "Default Name";
            Age = 0;
            Occupation = "Default Occupation";
            RoleInStory = "Default RoleInStory";
            PhysicalDescription = "Default PhysicalDescription";
            PersonalityTraits = "Default PersonalityTraits";
            Background = "Default Background";
            GoalsAndMotivations = "Default GoalsAndMotivations";
            CharacterArc = "Default CharacterArc";

            NovelId = 0;
            Novel = new Novel();
        }
    }
}
