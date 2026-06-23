using Godot;
using Platformer.Scenes.Player.Scripts.Data;

namespace Platformer.Scenes.Player.Scripts;

public partial class Player : CharacterBody2D
{
    [ExportGroup("Movement")]
    [Export] private float _movementSpeed = 300.0f;
    [Export] private float _jumpSpeed = -400.0f;

    [ExportGroup("Input")]
    [Export] private string _inputAxisLeft = "axis_left";
    [Export] private string _inputAxisRight = "axis_right";
    [Export] private string _inputAxisUp = "axis_up";
    [Export] private string _inputAxisDown = "axis_down";
    [Export] private string _inputActionJump = "action_jump";

    private PlayerInputData _inputData;
    
    #region Game Cycle

    public override void _PhysicsProcess(double delta)
    {
        var velocity = Velocity;

        UpdatePlayerInputData();
        
        UpdateGravity(ref velocity, delta);
        UpdateHorizontalMovement(ref velocity, delta);
        UpdateJump(ref velocity);
        
/*
        // Get the input direction and handle the movement/deceleration.
        var direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * _movementSpeed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, _movementSpeed);
        }
*/
        Velocity = velocity;
        MoveAndSlide();
    }

    #endregion

    private void UpdatePlayerInputData()
    {
        _inputData.IsOnFloor = IsOnFloor();
    }

    private void UpdateGravity(ref Vector2 velocity, double delta)
    {
        if (_inputData.IsOnFloor)
        {
            return;
        }

        velocity += GetGravity() * (float) delta;
    }

    private void UpdateHorizontalMovement(ref Vector2 velocity, double delta)
    {
        velocity.X = 0.0f;
        
        if (Input.IsActionPressed(_inputAxisLeft))
        {
            velocity.X = -_movementSpeed;
        }

        if (Input.IsActionPressed(_inputAxisRight))
        {
            velocity.X = _movementSpeed;
        }
    }

    private void UpdateJump(ref Vector2 velocity)
    {
        if (!_inputData.IsOnFloor)
        {
            return;
        }

        if (Input.IsActionJustPressed(_inputActionJump))
        {
            velocity.Y = _jumpSpeed;
        }
    }
}