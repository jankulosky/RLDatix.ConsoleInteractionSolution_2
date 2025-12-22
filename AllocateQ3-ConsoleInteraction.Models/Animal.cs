namespace AllocateQ3_ConsoleInteraction.Models
{
    public class Animal : BaseEntity
    {
        public Animal(string type, string sound)
        {
            Type = type;
            Sound = sound;
        }

        public string Type { get; set; }

        public string Sound { get; set; }
    }
}
