using Godot;
using Platformer.Scripts.Entities.Player.Resources;

namespace Platformer.Scripts.Entities.Player.Helpers;

public static class InputHelper
{
    public static bool IsMovementInput(PlayerInputSettings playerInputSettings, out Vector2 direction)
    {
        direction = Input.GetVector(
            playerInputSettings.InputAxisLeft,
            playerInputSettings.InputAxisRight,
            playerInputSettings.InputAxisUp,
            playerInputSettings.InputAxisDown);
        return direction != Vector2.Zero;
    }
}