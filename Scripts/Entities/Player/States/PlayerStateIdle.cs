using Godot;
using Platformer.Scripts.Entities.Player.Helpers;

namespace Platformer.Scripts.Entities.Player.States;

public partial class PlayerStateIdle : AbstractPlayerState
{
    #region Overrides of AbstractPlayerState

    public override void Initialize()
    {
        GD.Print($"{nameof(PlayerStateIdle)} {nameof(Initialize)}");
    }

    public override void Enter()
    {
        GD.Print($"{nameof(PlayerStateIdle)} {nameof(Enter)}");
    }

    public override void Exit()
    {
        GD.Print($"{nameof(PlayerStateIdle)} {nameof(Exit)}");
    }

    public override AbstractPlayerState HandleInput(InputEvent inputEvent)
    {
        if (InputHelper.IsMovementInput(Player.GetInputSettings(), out _))
        {
            return Player.GetState(PlayerCharacterStateId.Movement);
        }

        return null;
    }
    
    public override AbstractPlayerState Process(double delta)
    {
        return base.Process(delta);
    }

    public override AbstractPlayerState PhysicsProcess(double delta)
    {
        Player.Velocity = new Vector2(0.0f, Player.Velocity.Y);

        return null;
    }

    #endregion
}