using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Model
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public DateTime DatePublished { get; set; }
    }
}
