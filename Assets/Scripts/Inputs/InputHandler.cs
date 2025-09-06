using UnityEngine;

namespace DPX.Inputs
{
    public class InputHandler
    {
        public Vector3 MoveInput { get; private set; }
        public bool SprintInput { get; private set; }   
        public bool JumpInput { get; private set; }

        public void UpdateInput()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            MoveInput = new Vector3(h, 0, v);
            SprintInput = Input.GetKey(KeyCode.LeftShift);  // ✅ now bool
            JumpInput = Input.GetButtonDown("Jump");
        }
    }
}
