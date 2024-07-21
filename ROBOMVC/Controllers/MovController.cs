using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ROBOMVC.Models;
using ROBOMVC.Service;
using ROBOMVC.Session;

namespace ROBOMVC.Controllers;

public class MovController : Controller
{
    private readonly RoboAppService _roboAppService;
    private const string RoboSessionKey = "RoboEstado";

    public MovController(RoboAppService roboAppService)
    {
        _roboAppService = roboAppService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var viewModel = HttpContext.Session.Get<RoboViewModel>(RoboSessionKey);

        if (viewModel == null)
            viewModel = _roboAppService.EstadoIncialRobo();

        HttpContext.Session.Set(RoboSessionKey, viewModel);

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult EnviarComandos([FromQuery] RoboViewModel viewModel)
    {
        if (viewModel == null)
            return BadRequest("Dados inválidos");

        var estadoAtual = HttpContext.Session.Get<RoboViewModel>(RoboSessionKey);

        var erro = _roboAppService.ValidarMovimento(estadoAtual, viewModel);
        if (erro != null)
            return BadRequest(erro);

        HttpContext.Session.Set(RoboSessionKey, viewModel);

        return AtualizarComandos(viewModel);
    }

    // Privates

    private IActionResult AtualizarComandos(RoboViewModel viewModel)
    {
        return Ok(new
        {
            Cabeca = new
            {
                Rotacao = viewModel.Cabeca.Rotacao,
                Inclinacao = viewModel.Cabeca.Inclinacao
            },
            BracoEsquerdo = new
            {
                Cotovelo = viewModel.BracoEsquerdo.Cotovelo,
                Pulso = viewModel.BracoEsquerdo.Pulso
            },
            BracoDireito = new
            {
                Cotovelo = viewModel.BracoDireito.Cotovelo,
                Pulso = viewModel.BracoDireito.Pulso
            }
        });
    }
}
