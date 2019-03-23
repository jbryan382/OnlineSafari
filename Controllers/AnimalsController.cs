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

    [HttpGet("{query}")]
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

    [HttpPut("{id}")]
    public ActionResult<Animal> UpdateMovie(int id, [FromBody] Animal addToAnimal)
    {
      var db = new DatabaseContext();
      var animal = db.Animals.FirstOrDefault(f => f.Id == id);
      animal.Species = addToAnimal.Species;
      animal.CountOfTimesSeen = addToAnimal.CountOfTimesSeen;
      animal.LocationOfLastSeen = addToAnimal.LocationOfLastSeen;
      db.SaveChanges();
      return animal;
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteMovie(int id)
    {
      var db = new DatabaseContext();
      var animal = db.Animals.FirstOrDefault(f => f.Id == id);
      db.Animals.Remove(animal);
      db.SaveChanges();
      return Ok();
    }

  }
}