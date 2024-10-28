using GestorContatos.API.ExtensionMethods;
using GestorContatos.API.ViewModel;
using GestorContatos.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestorContatos.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ContatoController : ControllerBase
{
    private readonly IContatoService _contatoService;

    public ContatoController(IContatoService contatoService)
    {
        _contatoService = contatoService;
    }

    // GET: api/<ContatoController>
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_contatoService.GetContatos().ToViewModel());
    }

    // GET api/<ContatoController>/5
    [HttpGet("{ddd}")]
    public IActionResult ConsultaPorDDD(int ddd)
    {
        return Ok(_contatoService.GetContatosPorDDD(ddd)?.ToViewModel());
    }

    // POST api/<ContatoController>
    [HttpPost]
    public IActionResult PostInserirContato([FromBody] ContatoViewModel contato)
    {
        _contatoService.PostInserirContato(contato.ToModel());
        return Created("", _contatoService.PostInserirContato(contato.ToModel()).ToViewModel());
    }

    // PUT api/<ContatoController>/5
    [HttpPut("{id}")]
    public IActionResult PutAlterarContato(int id, [FromBody] ContatoViewModel contato)
    {
        _contatoService.PutAlterarContato(id, contato.ToModel());
        return NoContent();
    }

    // DELETE api/<ContatoController>/5
    [HttpDelete("{id}")]
    public IActionResult DeleteContato(int id)
    {
        _contatoService.DeleteContato(id);

        return NoContent();
    }
}

