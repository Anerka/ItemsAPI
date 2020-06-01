using System.Linq;
using ItemsAPI.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Controller.ItemsAPI
{
    [ApiController]
    [Route("api/items")]
    public class ItemsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetItems()
        {
            return Ok(ItemsDataStore.Current.Items);
        }

        [Route("{id}")]
        public IActionResult GetItem(int id)
        {
            return Ok(ItemsDataStore.Current.Items.FirstOrDefault(i => i.Id == id));
        }
    }
}
