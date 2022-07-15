using UnityEngine;

namespace Gameplay
{
    public partial class Player2D
    {
        public void SetMoveInput(Vector2 input)
        {
            _moveInput = input;
            if (input != Vector2.zero) _lastMoveInput = input;

            // Debug.Log($"Move input: {input}");
        }

        public void SetAimInput(Vector2 input)
        {
            _aimInput = input;
            // Debug.Log($"Aim input: {input}");
        }
    }
}