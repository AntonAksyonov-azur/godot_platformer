using Godot;
using Platformer.Scripts.Entities.Player.Helpers;

namespace Platformer.Scripts.Entities.Player.States;

public partial class PlayerStateMovement : AbstractPlayerState
{
    [ExportGroup("Movement")]
    [Export] private float _movementSpeed = 300.0f;
    [Export] private float _jumpSpeed = -400.0f;
    [Export] private float _floorSnapLength = 1.0f;
    
    #region Overrides of AbstractPlayerState

    public override void Initialize()
    {
        Player.FloorSnapLength = _floorSnapLength;
    }

    public override void Enter()
    {
        GD.Print($"{nameof(PlayerStateMovement)} {nameof(Enter)}");
    }

    public override void Exit()
    {
        GD.Print($"{nameof(PlayerStateMovement)} {nameof(Exit)}");
    }

    public override AbstractPlayerState HandleInput(InputEvent inputEvent)
    {
        return GetNextState(InputHelper.IsMovementInput(Player.GetInputSettings(), out _));
    }

    public override AbstractPlayerState Process(double delta)
    {
        return base.Process(delta);
    }

    public override AbstractPlayerState PhysicsProcess(double delta)
    {
        var isMovementInput = InputHelper.IsMovementInput(Player.GetInputSettings(), out var direction);
        if (isMovementInput)
        {
            UpdateHorizontalMovement(direction, delta);
        }

        return GetNextState(isMovementInput);
    }

    #endregion

    private void UpdateHorizontalMovement(Vector2 inputDirection, double delta)
    {
        Player.Velocity = new Vector2(inputDirection.X * _movementSpeed, Player.Velocity.Y);
    }

    private AbstractPlayerState GetNextState(bool isMovementInput)
    {
        if (isMovementInput)
        {
            return Player.GetState(PlayerCharacterStateId.Movement);
        }
        else
        {
            return Player.GetState(PlayerCharacterStateId.Idle);
        }
    }
}