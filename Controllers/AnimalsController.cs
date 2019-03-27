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

    private DatabaseContext db;
    public AnimalsController()
    {
      this.db = new DatabaseContext();
    }

    [HttpGet]
    public ActionResult<IList<Animal>> GetAllSpecies()
    {
      var animals = db.Animals.OrderBy(o => o.Species).ToList();
      return animals;
    }


    [HttpGet("{id}")]
    public ActionResult<Animal> GetChosenId(int id)
    {
      var animal = db.Animals.FirstOrDefault(f => f.Id == id);
      return animal;
    }

    [HttpGet("query/{query}")]
    public ActionResult<string> GetAllSpecies([FromQuery] string query)
    {
      return query;
    }

    [HttpGet("location/{location}")]
    public ActionResult<IList<Animal>> GetAnimalsByLocation(string location)
    {
      var animalsLocation = db.Animals.Where(animal => animal.LocationOfLastSeen.ToLower() == location.ToLower()).ToList();
      return animalsLocation;
    }

    [HttpPost]
    public ActionResult<Animal> Post([FromBody] Animal newAnimal)
    {
      db.Animals.Add(newAnimal);
      db.SaveChanges();
      return newAnimal;
    }

    [HttpPut("{id}")]
    public ActionResult<Animal> UpdateMovie(int id, [FromBody] Animal addToAnimal)
    {
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
      var animal = db.Animals.FirstOrDefault(f => f.Id == id);
      db.Animals.Remove(animal);
      db.SaveChanges();
      return Ok();
    }

    [HttpDelete("location/{location}")]

    public ActionResult DeleteAnimals(string location)
    {
      var deleteAnimalsByLocation = db.Animals.Where(w => w.LocationOfLastSeen.ToLower() == location.ToLower());
      db.Animals.RemoveRange(deleteAnimalsByLocation);
      db.SaveChanges();
      return Ok();
    }

  }
}