using ROBOMVC.Models;

namespace ROBOMVC.Service;

public class RoboAppService
{
    public RoboViewModel InitialRoboState() =>
        new RoboViewModel
        {
            Head = new HeadViewModel { Rotation = HeadRotation.AtRest, Tilt = HeadTilt.AtRest },
            LeftArm = new ArmViewModel { Elbow = Elbow.AtRest, Wrist = Wrist.AtRest },
            RightArm = new ArmViewModel { Elbow = Elbow.AtRest, Wrist = Wrist.AtRest }
        };
    

    public string ValidateMovement(RoboViewModel currentState, RoboViewModel viewModel)
    {
        // Verificar se o Pulso pode ser movimentado
        if (viewModel.LeftArm.Wrist != currentState.LeftArm.Wrist ||
            viewModel.RightArm.Wrist != currentState.RightArm.Wrist)
        {
            if (viewModel.LeftArm.Elbow != Elbow.StronglyContracted ||
                viewModel.RightArm.Elbow != Elbow.StronglyContracted)
            {
                return string.Format("O Pulso só pode ser movimentado caso o cotovelo esteja fortemente contraído.");
            }
        }

        // Verificar se a Cabeça pode ser rotacionada
        if (viewModel.Head.Rotation != currentState.Head.Rotation &&
            viewModel.Head.Tilt == HeadTilt.Downward)
        {
            return string.Format("A cabeça não pode ser rotacionada se a inclinação estiver para baixo.");
        }

        // Verificar a progressão dos estados
        if (!CheckStateProgress(currentState, viewModel))
            return string.Format("A progressão dos estados deve ser crescente ou decrescente, sem pular estados.");

        return string.Empty;
    }

    // Privates

    private bool CheckStateProgress(RoboViewModel currentState, RoboViewModel newState)
    {
        return CheckStateProgression(currentState.Head.Rotation, newState.Head.Rotation) &&
               CheckStateProgression(currentState.Head.Tilt, newState.Head.Tilt) &&
               CheckStateProgression(currentState.LeftArm.Elbow, newState.LeftArm.Elbow) &&
               CheckStateProgression(currentState.LeftArm.Wrist, newState.LeftArm.Wrist) &&
               CheckStateProgression(currentState.RightArm.Elbow, newState.RightArm.Elbow) &&
               CheckStateProgression(currentState.RightArm.Wrist, newState.RightArm.Wrist);
    }

    private bool CheckStateProgression<T>(T currentState, T newState) where T : Enum
    {
        var estados = Enum.GetValues(typeof(T)).Cast<T>().ToList();
        var currentIndex = estados.IndexOf(currentState);
        var newIndex = estados.IndexOf(newState);

        return Math.Abs(currentIndex - newIndex) <= 1;
    }

}

