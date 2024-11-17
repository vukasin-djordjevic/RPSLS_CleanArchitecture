using System.ComponentModel.DataAnnotations;

namespace Application.Abstractions.RandomNumberService
{
    public sealed class RandomNumberServiceSettings
    {
        public const string ConfigurationSection = "BoohmaRnd";
        public const string Uri = "https://codechallenge.boohma.com/random";
    }
}
