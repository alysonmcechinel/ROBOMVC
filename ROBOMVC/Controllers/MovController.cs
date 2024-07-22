using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ROBOMVC.Models;
using ROBOMVC.Service;
using ROBOMVC.Session;

namespace ROBOMVC.Controllers;

public class MovController : Controller
{
    private readonly RoboAppService _roboAppService;
    private const string RoboSessionKey = "RobotState";

    public MovController(RoboAppService roboAppService)
    {
        _roboAppService = roboAppService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var viewModel = HttpContext.Session.Get<RoboViewModel>(RoboSessionKey);

        if (viewModel == null)
            viewModel = _roboAppService.InitialRoboState();

        HttpContext.Session.Set(RoboSessionKey, viewModel);

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult SendCommands([FromQuery] RoboViewModel viewModel)
    {
        if (viewModel == null)
            return BadRequest("Dados inválidos");

        var currentState = HttpContext.Session.Get<RoboViewModel>(RoboSessionKey);

        var erro = _roboAppService.ValidateMovement(currentState, viewModel);
        if (!string.IsNullOrEmpty(erro))
        {
            viewModel = _roboAppService.InitialRoboState();
            HttpContext.Session.Set(RoboSessionKey, viewModel);
            return BadRequest(erro);
        }            

        HttpContext.Session.Set(RoboSessionKey, viewModel);
        
        return UpdateCommands(viewModel);
    }

    // Privates

    private IActionResult UpdateCommands(RoboViewModel viewModel)
    {
        return Ok(new
        {
            Head = new
            {
                Rotation = viewModel.Head.Rotation,
                Tilt = viewModel.Head.Tilt
            },
            LeftArm = new
            {
                Elbow = viewModel.LeftArm.Elbow,
                Wrist = viewModel.LeftArm.Wrist
            },
            RightArm = new
            {
                Elbow = viewModel.RightArm.Elbow,
                Wrist = viewModel.RightArm.Wrist
            }
        });
    }
}
