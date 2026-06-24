using System.Collections.Generic;
using Godot;
using Platformer.Scripts.Entities.Player.Data;
using Platformer.Scripts.Entities.Player.Helpers;
using Platformer.Scripts.Entities.Player.States;
using PlayerInputSettings = Platformer.Scripts.Entities.Player.Resources.PlayerInputSettings;

namespace Platformer.Scripts.Entities.Player;

public partial class PlayerCharacter : CharacterBody2D
{
    private const int PreviousStatesLength = 3;

    [ExportGroup("Input")]
    [Export] private PlayerInputSettings _playerInputSettings;

    [ExportGroup("States")]
    [Export] private AbstractPlayerState[] _states;
    [Export] private AbstractPlayerState _currentState;

    private bool _isOnFloor;
    private Vector2 _direction = Vector2.Zero;

    // States
    private readonly Dictionary<PlayerCharacterStateId, AbstractPlayerState> _statesDictionary = new Dictionary<PlayerCharacterStateId, AbstractPlayerState>();
    private readonly Queue<AbstractPlayerState> _previousStates = new Queue<AbstractPlayerState>();

    private PlayerInputData _inputData;

    #region Overrides of Node

    public override void _Ready()
    {
        InitializeStates();
    }

    public override void _Process(double delta)
    {
        UpdateDirection();
        SwitchState(_currentState.Process(delta));
    }

    public override void _PhysicsProcess(double delta)
    {
        UpdateGravity(delta);
        MoveAndSlide();

        SwitchState(_currentState.PhysicsProcess(delta));
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        SwitchState(_currentState.HandleInput(@event));
    }

    #endregion

    private void UpdatePlayerInputData()
    {
        _inputData.IsOnFloor = IsOnFloor();
    }

    private void UpdateGravity(double delta)
    {
        if (_inputData.IsOnFloor)
        {
            return;
        }

        Velocity += GetGravity() * (float) delta;
    }

    private void InitializeStates()
    {
        foreach (var state in _states)
        {
            state.SetPlayer(this);
            state.Initialize();

            _statesDictionary.Add(state.StateId, state);
        }

        // First state
        SwitchState(_states[0]);
    }

    private void SwitchState(AbstractPlayerState newState)
    {
        if (newState == null)
        {
            return;
        }

        // Exit current
        _currentState?.Exit();

        // History
        _previousStates.Enqueue(_currentState);

        // Remove previous
        if (_previousStates.Count > PreviousStatesLength)
        {
            _previousStates.Dequeue();
        }

        // New
        _currentState = newState;
        _currentState.Enter();
    }


    private void UpdateDirection()
    {
        InputHelper.IsMovementInput(_playerInputSettings, out _direction);
    }

    public AbstractPlayerState GetState(PlayerCharacterStateId playerCharacterStateId)
    {
        return _statesDictionary[playerCharacterStateId];
    }

    public PlayerInputSettings GetInputSettings()
    {
        return _playerInputSettings;
    }
}