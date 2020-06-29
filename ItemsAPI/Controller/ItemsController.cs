using System.Collections.Generic;
using System.Linq;
using ItemsAPI.Data;
using ItemsAPI.Entities;
using ItemsAPI.Models;
using ItemsAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Controller.ItemsAPI
{
    [ApiController]
    [Route("api/items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemInfoRepository _itemInfoRepository;

        public ItemsController(IItemInfoRepository itemInfoRepository)
        {
            _itemInfoRepository = itemInfoRepository;
        }

        [HttpGet]
        public IActionResult GetItems()
        {
            var itemEntities = _itemInfoRepository.GetItems();

            var result = new List<ItemWIthoutRatingDto>();

            foreach (var itemEntity in itemEntities)
            {
                result.Add(new ItemWIthoutRatingDto
                {
                    Id = itemEntity.Id,
                    Name = itemEntity.Name,
                    Description = itemEntity.Description
                });
            }

            return Ok(result);
        }

        [Route("{id}")]
        public IActionResult GetItem(int id, bool includeRating = false)
        {
            var item = _itemInfoRepository.GetItem(id, includeRating);

            if(item == null)
            {
                return NotFound();
            }

            if (includeRating)
            {
                var itemResult = new ItemDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description
                };


                foreach (var rating in item.Rating)
                {
                    itemResult.Rating.Add(
                        new RatingDto()
                        {
                            Id = rating.Id,
                            Name = rating.Name,
                            Description = rating.Description
                        });
                }


                return Ok(itemResult);
            }

            var itemWithoutRatingResult =
                new ItemWIthoutRatingDto()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description
                };

            return Ok(itemWithoutRatingResult);
        }

        //[Route("{id}")]
        //public IActionResult GetItem(int id)
        //{
        //    return Ok(ItemsDataStore.Current.Items.FirstOrDefault(i => i.Id == id));
        //}
    }
}
