using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RollinsPizza.Models;
using RollinsPizza.Services;

namespace RollinsPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController() { }

    //Get all Pizza
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();

    //Retrieve a single pizza by id action
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);
        if (pizza == null)
            return NotFound();
        return pizza;
    }

    //Create and add pizza
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
       //This code will save the pizza and return the result
       PizzaService.Add(pizza);
       return CreatedAtAction(nameof(Get), new {id = pizza.Id}, pizza);
    }

    //Modifying or updating a pizza

    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
       //This code will update the pizza and return a result
       if(id != pizza.Id)
            return BadRequest();
       var existingPizza = PizzaService.Get(id);
       if(existingPizza is null)
            return NotFound();

       PizzaService.Update(pizza);
       return NoContent();          
    }

    // Deleting the pizza
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        //This code will delete the pizza and return a result
        var pizza = PizzaService.Get(id);

        if(pizza is null)
           return NotFound();

        PizzaService.Delete(id);

        return NoContent();
    }
}
