using ItemsAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ItemsAPI.Data
{
    public class ItemsDataStore
    {
        public static ItemsDataStore Current { get; } = new ItemsDataStore();
        public List<ItemDto> Items { get; set; }

        public ItemsDataStore()
        {
            Items = new List<ItemDto>()
            {
                new ItemDto()
                {
                    Id = 1,
                    Name = "Hammer",
                    Description = "Big hammer",
                    Rating = new List<RatingDto>()
                    {
                        new RatingDto()
                        {
                            Id = 1,
                            Name = "One",
                            Description = "Alrighty 1"
                        },
                        new RatingDto()
                        {
                            Id = 2,
                            Name = "Two",
                            Description = "Alrighty 2"
                        }
                    }
                },
                new ItemDto()
                {
                    Id = 2,
                    Name = "Wrench",
                    Description = "Smaller wrench",
                    Rating = new List<RatingDto>()
                    {
                        new RatingDto()
                        {
                            Id = 3,
                            Name = "Three",
                            Description = "Alrighty 3"
                        },
                        new RatingDto()
                        {
                            Id = 4,
                            Name = "Four",
                            Description = "Alrighty 4"
                        }
                    }
                },
                new ItemDto()
                {
                    Id = 3,
                    Name = "Screwdriver",
                    Description = "Medium screwdriver",
                    Rating = new List<RatingDto>()
                    {
                        new RatingDto()
                        {
                            Id = 5,
                            Name = "Five",
                            Description = "Alrighty 5"
                        },
                        new RatingDto()
                        {
                            Id = 6,
                            Name = "Six",
                            Description = "Alrighty 6"
                        }
                    }
                }
            };
        }

        public bool AddItem(string name, string description)
        {
            var newId = Items.OrderBy(x=>x.Id).LastOrDefault().Id + 1;

            Items.Add(new ItemDto {Id = newId, Name = name, Description = description });

            return true;
        }

        //public bool AddItemRating(int id, int rating)
        //{
        //    foreach (var item in Items.Where(x => x.Id == id))
        //    {
        //        item.Rating.Add(rating);
        //    }

        //    //Items.FirstOrDefault(x => x.Id == id).Rating.Add(rating);

        //    return true;
        //}

        //public bool AddItemRating(int id, RatingForCreationDto rating)
        //{
        //    foreach (var item in Items.Where(x => x.Id == id))
        //    {
        //        item.Rating.Add(rating);
        //    }

        //    return true;
        //}
    }
}
