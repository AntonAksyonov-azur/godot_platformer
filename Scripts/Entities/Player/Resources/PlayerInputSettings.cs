using Godot;

namespace Platformer.Scripts.Entities.Player.Resources;

public partial class PlayerInputSettings : Resource
{
    [ExportGroup("Contents")]
    [Export] public string InputAxisLeft = "axis_left";
    [Export] public string InputAxisRight = "axis_right";
    [Export] public string InputAxisUp = "axis_up";
    [Export] public string InputAxisDown = "axis_down";
}