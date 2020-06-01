using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemsAPI.Models
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfRating
        {
            get
            {
                return Rating.Count;
            }
        }
        public ICollection<RatingDto> Rating { get; set; } = new List<RatingDto>();
    }
}