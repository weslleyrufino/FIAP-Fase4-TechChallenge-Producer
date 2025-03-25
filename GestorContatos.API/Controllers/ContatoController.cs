using GestorContatos.Application.ExtensionMethods;
using GestorContatos.Application.Interfaces.Services;
using GestorContatos.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GestorContatos.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ContatoController(IContatoService contatoService, ILogger<ContatoController> logger) : ControllerBase
{
    private readonly IContatoService _contatoService = contatoService;
    private readonly ILogger<ContatoController> _logger = logger;

    [HttpGet]
    public IActionResult Get()
    {
        var contatos = _contatoService.GetContatos().ToViewModel();

        if (!contatos.Any())
            return NoContent();
        
        return Ok(contatos);
    }

    [HttpGet("{ddd:int}")]
    public IActionResult ConsultaPorDDD([FromRoute] int ddd)
    {
        var contatos = _contatoService.GetContatosPorDDD(ddd)?.ToViewModel();

        if (contatos == null || !contatos.Any())
            return NotFound("Nenhum contato encontrado para o DDD especificado.");

        return Ok(contatos);
    }

    [HttpPost]
    public IActionResult PostInserirContato([FromBody] CreateContatoViewModel contato)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _contatoService.PostInserirContato(contato.ToModel());

        return Created();
    }

    [HttpPut]
    public IActionResult PutAlterarContato([FromBody] UpdateContatoViewModel contato)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Aqui não deverá mais obter direto da base de dados. Deverá obter pelo azure function.
        if (_contatoService.ObterPorId(contato.Id) is null)
            return NotFound("Contato não existe");

        _contatoService.PutAlterarContato(contato.ToModel());
        return NoContent();

    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteContato([FromRoute] int id)
    {
        _contatoService.DeleteContato(id);

        return NoContent();
    }
}
