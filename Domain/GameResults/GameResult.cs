using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.GameResults
{
    public class GameResult
    {
        [Key]
        public Guid Id { get; set; }        
        public required string Results { get; set; }
        public int Player { get; set; }
        public int Computer { get; set; }
        public DateTime Created { get; set; }
    }
}
