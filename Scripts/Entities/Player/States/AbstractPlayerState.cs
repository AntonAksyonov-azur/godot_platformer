using Godot;

namespace Platformer.Scripts.Entities.Player.States;

[Icon("res://Sprites/Editor/state.svg")]
public abstract partial class AbstractPlayerState : Node
{
    [ExportGroup("State")]
    [Export] private PlayerCharacterStateId _stateId;
    
    public PlayerCharacterStateId StateId => _stateId;
    
    protected PlayerCharacter Player;
    protected AbstractPlayerState NextState;

    public void SetPlayer(PlayerCharacter player)
    {
        Player = player;
    }
    
    #region Required Methods

    /// <summary>
    /// Initialize state
    /// </summary>
    public abstract void Initialize();
    
    /// <summary>
    /// Every time on Enter this state
    /// </summary>
    public abstract void Enter();
    
    /// <summary>
    /// Every time on Exit this state
    /// </summary>
    public abstract void Exit();

    #endregion

    #region Virtual Methods

    public virtual AbstractPlayerState HandleInput(InputEvent inputEvent)
    {
        return NextState;
    }

    /// <summary>
    /// Updates this state in game loop
    /// </summary>
    /// <param name="delta"></param>
    /// <returns></returns>
    public virtual AbstractPlayerState Process(double delta)
    {
        return NextState;
    }
    
    /// <summary>
    /// Updates this state in physics game loop
    /// </summary>
    /// <param name="delta"></param>
    /// <returns></returns>
    public virtual AbstractPlayerState PhysicsProcess(double delta)
    {
        return NextState;
    }

    #endregion
}