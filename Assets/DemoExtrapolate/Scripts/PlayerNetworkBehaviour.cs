using Bolt;
using UnityEngine;

namespace Assets.DemoExtrapolate.Scripts
{
    [RequireComponent(typeof(PlayerMoveBehaviour))]
    public class PlayerNetworkBehaviour : EntityBehaviour<IDEPlayerState>
    {
        private PlayerMoveBehaviour _motor;

        void Awake()
        {
            _motor = GetComponent<PlayerMoveBehaviour>();
        }

        public override void Attached()
        {
            state.SetTransforms(state.DEPlayerTransform, transform);
        }

       

        public override void SimulateController()
        {
            var input = DEPlayerMoveCommand.Create();
            input.Forward = true;

            entity.QueueInput(input);
        }

        // both on client and server
        public override void ExecuteCommand(Command command, bool resetState)
        {
            var cmd = (DEPlayerMoveCommand) command;
            var result = cmd.Result;
            var input = cmd.Input;

            if (resetState)
            {
                // we got a correction from the server, reset (this only runs on the client)
                _motor.SetState(
                    result.Position,
                    result.Velocity,
                    result.IsGrounded,
                    result.JumpFrames
                );
            }
            else
            {
                // apply movement (this runs on both server and client)
                var motorState = _motor.Move(
                    input.Forward,
                    false,
                    false,
                    false,
                    false,
                    0);

                // copy the motor state to the commands result (this gets sent back to the client)
                result.Position = motorState.position;
                result.Velocity = motorState.velocity;
                result.IsGrounded = motorState.isGrounded;
                result.JumpFrames = motorState.jumpFrames;
            }
        }
    }
}