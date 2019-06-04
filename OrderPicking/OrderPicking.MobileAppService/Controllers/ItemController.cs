#region Usings

using System;
using Microsoft.AspNetCore.Mvc;
using OrderPicking.Models;

#endregion

namespace OrderPicking.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        #region Fields

        private readonly IItemRepository ItemRepository;

        #endregion

        #region Constructors

        public ItemController(IItemRepository itemRepository)
        {
            this.ItemRepository = itemRepository;
        }

        #endregion

        #region Methods

        [HttpGet]
        public IActionResult List()
        {
            return this.Ok(this.ItemRepository.GetAll());
        }

        [HttpGet("{id}")]
        public Item GetItem(string id)
        {
            var item = this.ItemRepository.Get(id);

            return item;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Item item)
        {
            try
            {
                if (item == null || !this.ModelState.IsValid)
                    return this.BadRequest("Invalid State");

                this.ItemRepository.Add(item);
            }
            catch (Exception)
            {
                return this.BadRequest("Error while creating");
            }

            return this.Ok(item);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Item item)
        {
            try
            {
                if (item == null || !this.ModelState.IsValid)
                    return this.BadRequest("Invalid State");
                this.ItemRepository.Update(item);
            }
            catch (Exception)
            {
                return this.BadRequest("Error while creating");
            }

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            this.ItemRepository.Remove(id);
        }

        #endregion
    }
}