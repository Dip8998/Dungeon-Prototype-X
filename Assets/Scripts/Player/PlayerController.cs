using DPX.GameStamina;
using DPX.Inputs;
using DPX.Player.StateMachine;
using DPX.ScriptableObjects;
using DPX.Weapons;
using System.Collections.Generic;
using UnityEngine;



namespace DPX.Player
{
    public class PlayerController
    {
        private readonly PlayerView player;
        private readonly PlayerSO playerData;
        private InputHandler inputs = new InputHandler();

        private CharacterController controller;
        private PlayerStateMachine stateMachine;

        private Camera cam;

        private Vector3 velocity;

        private readonly List<PlayerWeaponView> weapons = new List<PlayerWeaponView>();

        private int currentWeaponIndex = -1;

        public InputHandler Inputs => inputs;

        public PlayerView Player => player;

        private Stamina stamina;

        public PlayerController(PlayerView player, PlayerSO playerSO)
        {
            this.player = player;

            playerData = playerSO;

            stateMachine = new PlayerStateMachine(this);
        }

        public void StartPlayer()
        {
            controller = player.GetComponent<CharacterController>();
            cam = Camera.main;
            stamina = player.GetComponent<Stamina>();

            if (weapons.Count > 0)
                EquipWeapon(0);
            stateMachine.ChangeState(PlayerState.IDLE);
        }

        public void UpdatePlayer()
        {
            inputs.UpdateInput();

            if (currentWeaponIndex >= 0)
                weapons[currentWeaponIndex].UpdateWeapon();

            if (inputs.SwitchWeaponInput)
                CycleWeapon();

            stateMachine.Update();
            
            ApplyGravity();
        }

        public void HandleMovement()
        {
            Quaternion yaw = Quaternion.Euler(0f, cam.transform.eulerAngles.y, 0f);
            Vector3 camF = (yaw * Vector3.forward);
            Vector3 camR = (yaw * Vector3.right);
            Vector3 moveDir = (camF * inputs.MoveInput.z + camR * inputs.MoveInput.x).normalized;

            float speed = playerData.MoveSpeed;

            if (inputs.SprintInput && stamina != null)
            {
                stamina.DrainForSprint();

                if (!stamina.IsExhausted)
                {
                    speed = playerData.SprintSpeed;
                }
            }

            Vector3 horizontal = moveDir * speed;
            controller.Move((horizontal + Vector3.up * velocity.y) * Time.deltaTime);

            if (!RotateTowardsMouse() && moveDir.sqrMagnitude > 0.01f)
            {
                float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
                player.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            }
        }

        public bool RotateTowardsMouse()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                Vector3 target = hit.point;
                Vector3 lookDir = target - player.transform.position;
                lookDir.y = 0f;

                if (lookDir.sqrMagnitude > 0.01f)
                {
                    Quaternion targetRot = Quaternion.LookRotation(lookDir);
                    player.transform.rotation = Quaternion.Slerp(
                      player.transform.rotation,
                      targetRot,
                      Time.deltaTime * playerData.RotationSpeed
                    );
                    return true;
                }
            }
            return false;
        }

        public void HandleAttack()
        {
            if (inputs.FireInput && currentWeaponIndex >= 0)
            {
                if (weapons[currentWeaponIndex] is PlayerGunWeapon)
                {
                    weapons[currentWeaponIndex].Attack();

                    RotateTowardsMouse();
                }
                else if (weapons[currentWeaponIndex] is PlayerMeleeWeapon)
                {
                    if (inputs.MeleeInput)
                    {
                        weapons[currentWeaponIndex].Attack();
                        RotateTowardsMouse();
                    }
                }
            }
        }


        private void ApplyGravity()
        {
            if (controller.isGrounded && velocity.y < 0f)
                velocity.y = -1f;
            else
                velocity.y += playerData.Gravity * playerData.GravityMultiplyer * Time.deltaTime;
        }

        public void AddWeapon(PlayerWeaponView w)
        {
            if (w == null || weapons.Contains(w)) return;

            w.OnUnequip();
            weapons.Add(w);
            if (weapons.Count == 1)
                EquipWeapon(0);

        }

        private void CycleWeapon()
        {
            if (weapons.Count <= 1) return;

            int next = (currentWeaponIndex + 1) % weapons.Count;

            EquipWeapon(next);
        }

        private void EquipWeapon(int index)
        {
            if (index < 0 || index >= weapons.Count) return;
            for (int i = 0; i < weapons.Count; i++)
            {
                if (i == index) weapons[i].OnEquip();

                else weapons[i].OnUnequip();
            }
            currentWeaponIndex = index;
        }
    }
}