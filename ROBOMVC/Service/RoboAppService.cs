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
        if (!CheckStateProgress(currentState, viewModel))
            return ValidationMessages.StateProgress;

        if (viewModel.LeftArm.Wrist != currentState.LeftArm.Wrist)
            if (viewModel.LeftArm.Elbow != Elbow.StronglyContracted)
                return ValidationMessages.WristMovement;

        if (viewModel.RightArm.Wrist != currentState.RightArm.Wrist)
            if (viewModel.RightArm.Elbow != Elbow.StronglyContracted)
                return ValidationMessages.WristMovement;

        if (viewModel.Head.Rotation != currentState.Head.Rotation && viewModel.Head.Tilt == HeadTilt.Downward)
            return ValidationMessages.HeadRotation;

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

    private class ValidationMessages
    {
        public const string StateProgress = "A progressão dos estados deve ser crescente ou decrescente, sem pular estados.";
        public const string WristMovement = "O Pulso só pode ser movimentado caso o cotovelo esteja fortemente contraído.";
        public const string HeadRotation = "A cabeça não pode ser rotacionada se a inclinação estiver para baixo.";
    }
}
