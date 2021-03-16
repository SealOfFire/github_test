using Models.Database;
using System.Collections.Generic;

namespace GIthub_Test.Models
{
    public class HomeModel
    {
        public IEnumerable<Left> Lefts { get; set; }
        public IEnumerable<Right> Rights { get; set; }
    }
}
