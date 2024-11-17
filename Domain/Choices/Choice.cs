
using System.Diagnostics;

namespace Domain.Choices
{
    public class Choice
    {
        public Choice(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
