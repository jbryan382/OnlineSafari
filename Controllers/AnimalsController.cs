using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using onlinesafari;

namespace OnlineSafari.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AnimalsController : ControllerBase
  {

    [HttpGet]
    public ActionResult<IList<Animal>> GetAllSpecies()
    {
      var db = new DatabaseContext();
      var animals = db.Animals.OrderBy(o => o.Species).ToList();
      return animals;

    }


    [HttpGet("{id}")]
    public ActionResult<Animal> GetChosenId(int id)
    {
      var db = new DatabaseContext();
      var animal = db.Animals.FirstOrDefault(f => f.Id == id);
      return animal;
    }

    [HttpGet]
    public ActionResult<string> GetAllSpecies([FromQuery] string query)
    {
      return query;
    }

    [HttpPost]
    public ActionResult<Animal> Post([FromBody] Animal newAnimal)
    {
      var db = new DatabaseContext();
      db.Animals.Add(newAnimal);
      db.SaveChanges();
      return newAnimal;
    }

  }
}