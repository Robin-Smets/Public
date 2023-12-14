using NovelistBlazor.Common.Interface;

namespace NovelistBlazor.Common.DTO
{
    public class NovelDTO : IDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Abstract { get; set; }

        public NovelDTO()
        {
            
            Id = 0;
            Name = "";
            Description = "";
            Abstract = "";
            
        }

        public static IDTO Create()
        {
            return new NovelDTO();
        }
    }
}
