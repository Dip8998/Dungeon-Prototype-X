using UnityEngine;

namespace DPX.InputSystem
{
    public class InputHandler
    {
        public Vector3 MovementInput { get; set; }

        public void HandleInput()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            MovementInput = new Vector3(h,0,v).normalized;
        }
    }
}
