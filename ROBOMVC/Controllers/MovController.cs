using Microsoft.AspNetCore.Mvc;
using ROBOMVC.Models;
using ROBOMVC.Service;

namespace ROBOMVC.Controllers;

public class MovController : Controller
{
    private readonly RoboAppService _roboAppService;

    public MovController(RoboAppService roboAppService)
    {
        _roboAppService = roboAppService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var viewModel = new RoboViewModel
        {
            Cabeca = new CabecaViewModel
            {
                Rotacao = RotacaoCabeca.EmRepouso,
                Inclinacao = InclinacaoCabeca.EmRepouso
            },
            BracoEsquerdo = new BracoViewModel
            {
                Cotovelo = Cotovelo.EmRepouso,
                Pulso = Pulso.EmRepouso
            },
            BracoDireito = new BracoViewModel
            {
                Cotovelo = Cotovelo.EmRepouso,
                Pulso = Pulso.EmRepouso
            }
        };

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult EnviarComandos([FromQuery] RoboViewModel viewModel)
    {
        if (viewModel == null)
            return BadRequest("Dados inválidos");

        var estadoAtual = _roboAppService.ObterEstadoAtualRobo();

        var erro = _roboAppService.ValidarMovimento(estadoAtual, viewModel);
        if (erro != null)
        {
            return BadRequest(erro);
        }

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
