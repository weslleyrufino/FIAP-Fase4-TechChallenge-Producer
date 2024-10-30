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

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            return Ok(_contatoService.GetContatos().ToViewModel());
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("{ddd}")]
    public IActionResult ConsultaPorDDD(int ddd)
    {
        try
        {
            return Ok(_contatoService.GetContatosPorDDD(ddd)?.ToViewModel());
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPost]
    public IActionResult PostInserirContato([FromBody] CreateContatoViewModel contato)
    {
        try
        {
            _contatoService.PostInserirContato(contato.ToModel());
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPut]
    public IActionResult PutAlterarContato([FromBody] UpdateContatoViewModel contato)
    {
        try
        {
            _contatoService.PutAlterarContato(contato.ToModel());
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteContato(int id)
    {
        try
        {
            _contatoService.DeleteContato(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
