using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CachingMVC.Models
{
    public class Post : BaseEntity
    {
        public string Topic { get; set; }
        public string Description { get; set; }
    }
}
