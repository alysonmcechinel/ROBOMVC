using ROBOMVC.Models;
using ROBOMVC.Service;

namespace ROBOMVC.Tests
{
    public class MovTest
    {
        private readonly RoboAppService _service = new RoboAppService();

        [Fact]
        public void ValidateMovement_ShouldReturnError_WhenElbowIsNotStronglyContractedButWristIsMoved()
        {
            var currentState = _service.InitialRoboState();
            var newState = new RoboViewModel
            {
                RightArm = { Elbow = Elbow.SlightlyContracted, Wrist = Wrist.RotationToMinus45 }
            };

            var error = _service.ValidateMovement(currentState, newState);

            Assert.Equal("O Pulso só pode ser movimentado caso o cotovelo esteja fortemente contraído.", error);
        }

        [Fact]
        public void ValidateMovement_ShouldPass_WhenElbowIsStronglyContractedAndWristIsMoved()
        {
            var currentState = new RoboViewModel
            {
                LeftArm = { Elbow = Elbow.StronglyContracted, Wrist = Wrist.AtRest }
            };
            var newState = new RoboViewModel
            {
                LeftArm = { Elbow = Elbow.StronglyContracted, Wrist = Wrist.RotationTo45 }
            };

            var error = _service.ValidateMovement(currentState, newState);

            Assert.Equal(string.Empty, error);
        }

        [Fact]
        public void ValidateMovement_ShouldReturnError_WhenHeadTiltedDownwardAndRotationIsChanged()
        {
            var currentState = new RoboViewModel
            {
                Head = { Rotation = HeadRotation.AtRest, Tilt = HeadTilt.Downward }
            };
            var newState = new RoboViewModel
            {
                Head = { Rotation = HeadRotation.RotationTo45, Tilt = HeadTilt.Downward }
            };

            var error = _service.ValidateMovement(currentState, newState);

            Assert.Equal("A cabeça não pode ser rotacionada se a inclinação estiver para baixo.", error);
        }

        [Fact]
        public void ValidateMovement_ShouldPass_WhenHeadTiltedNotDownwardAndRotationIsChanged()
        {
            var currentState = new RoboViewModel
            {
                Head = { Rotation = HeadRotation.AtRest, Tilt = HeadTilt.Upward }
            };
            var newState = new RoboViewModel
            {
                Head = { Rotation = HeadRotation.RotationTo45, Tilt = HeadTilt.Upward }
            };

            var error = _service.ValidateMovement(currentState, newState);

            Assert.Equal(string.Empty, error);
        }

        [Fact]
        public void ValidateMovement_ShouldReturnError_WhenElbowProgressionIsInvalid()
        {
            var currentState = new RoboViewModel
            {
                LeftArm = { Elbow = Elbow.AtRest }
            };
            var newState = new RoboViewModel
            {
                LeftArm = { Elbow = Elbow.StronglyContracted }
            };

            var error = _service.ValidateMovement(currentState, newState);

            Assert.Equal("A progressão dos estados deve ser crescente ou decrescente, sem pular estados.", error);
        }

        [Fact]
        public void ValidateMovement_ShouldPass_WhenElbowProgressionIsValid()
        {
            var currentState = new RoboViewModel
            {
                LeftArm = { Elbow = Elbow.AtRest }
            };
            var newState = new RoboViewModel
            {
                LeftArm = { Elbow = Elbow.SlightlyContracted }
            };

            var error = _service.ValidateMovement(currentState, newState);

            Assert.Equal(string.Empty, error);
        }

        [Fact]
        public void ValidateMovement_ShouldReturnError_WhenHeadRotationIsOutOfBounds()
        {
            var currentState = _service.InitialRoboState();
            var newState = new RoboViewModel
            {
                Head = { Rotation = (HeadRotation)999 } // Invalid state
            };

            var error = _service.ValidateMovement(currentState, newState);

            Assert.NotEqual(string.Empty, error);
        }

        [Fact]
        public void ValidateMovement_ShouldReturnError_WhenHeadTiltIsOutOfBounds()
        {
            var currentState = _service.InitialRoboState();
            var newState = new RoboViewModel
            {
                Head = { Tilt = (HeadTilt)999 } // Invalid state
            };

            var error = _service.ValidateMovement(currentState, newState);

            Assert.NotEqual(string.Empty, error);
        }

        [Fact]
        public void ValidateMovement_ShouldReturnError_WhenWristIsOutOfBounds()
        {
            var currentState = _service.InitialRoboState();
            var newState = new RoboViewModel
            {
                LeftArm = { Wrist = (Wrist)999 } // Invalid state
            };

            var error = _service.ValidateMovement(currentState, newState);

            Assert.NotEqual(string.Empty, error);
        }
    }
}