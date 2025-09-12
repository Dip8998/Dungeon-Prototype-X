using UnityEngine;

namespace DPX.Inputs
{
    public class InputHandler
    {
        public Vector3 MoveInput { get; private set; }
        public bool SprintInput { get; private set; }
        public bool FireInput { get; private set; }
        public bool MeleeInput { get; private set; }
        public bool SwitchWeaponInput { get; private set; }

        public void UpdateInput()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            MoveInput = new Vector3(h,0, v);

            SprintInput = Input.GetKey(KeyCode.LeftShift);
            FireInput = Input.GetMouseButton(0);
            MeleeInput = Input.GetMouseButtonDown(0);
            SwitchWeaponInput = Input.GetKeyDown(KeyCode.Tab);
        }

        //public void ClearInputs()
        //{
        //    MoveInput = Vector2.zero;
        //    SprintInput = false;
        //    FireInput = false;
        //    MeleeInput = false;
        //    SwitchWeaponInput = false;
        //}
    }
}