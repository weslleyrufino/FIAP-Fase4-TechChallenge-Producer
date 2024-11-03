using GestorContatos.Application.ExtensionMethods;
using GestorContatos.Application.Interfaces.Services;
using GestorContatos.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GestorContatos.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ContatoController(IContatoService contatoService) : ControllerBase
{
    private readonly IContatoService _contatoService = contatoService;

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var contatos = _contatoService.GetContatos().ToViewModel();

            if (!contatos.Any())
                return NoContent();

            return Ok(contatos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor {ex.Message}");
        }
    }

    [HttpGet("{ddd:int}")]
    public IActionResult ConsultaPorDDD([FromRoute] int ddd)
    {
        try
        {
            var contatos = _contatoService.GetContatosPorDDD(ddd)?.ToViewModel();

            if(contatos == null || !contatos.Any())
                return NotFound("Nenhum contato encontrado para o DDD especificado.");

            return Ok(contatos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor {ex.Message}");
        }
    }

    [HttpPost]
    public IActionResult PostInserirContato([FromBody] CreateContatoViewModel contato)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _contatoService.PostInserirContato(contato.ToModel());

            return Created();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor {ex.Message}");
        }
    }

    [HttpPut]
    public IActionResult PutAlterarContato([FromBody] UpdateContatoViewModel contato)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_contatoService.ObterPorId(contato.Id) is null)
                return NotFound("Contato não existe");

            _contatoService.PutAlterarContato(contato.ToModel());
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor {ex.Message}");
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteContato([FromRoute]int id)
    {
        try
        {
            _contatoService.DeleteContato(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor {ex.Message}");
        }
    }
}
