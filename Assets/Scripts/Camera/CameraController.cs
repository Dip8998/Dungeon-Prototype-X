using System;
using UnityEngine;

namespace DPX.PlayerCamera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform playerTarget;
        [SerializeField] private MouseSensitivity mouseSensitivity;
        [SerializeField] private CameraAngle cameraAngle;
        [SerializeField] private Vector3 cameraOffset = new Vector3(0, 2f, -4f);

        private CameraRotation cameraRotation;
        private Vector2 mouseInput;

        private void Update()
        {
            HandleMouseInput();
            UpdateRotation();
        }

        private void LateUpdate()
        {
            ApplyCameraTransform();
        }

        private void HandleMouseInput()
        {
            mouseInput.x = Input.GetAxisRaw("Mouse X");
            mouseInput.y = Input.GetAxisRaw("Mouse Y");
        }

        private void UpdateRotation()
        {
            cameraRotation.yRot += mouseInput.x * mouseSensitivity.horizontal * Time.deltaTime * (mouseSensitivity.invertH ? -1 : 1);
            cameraRotation.xRot += mouseInput.y * mouseSensitivity.vertical * Time.deltaTime * (mouseSensitivity.invertV ? 1 : -1);
            cameraRotation.xRot = Mathf.Clamp(cameraRotation.xRot, cameraAngle.min, cameraAngle.max);
        }

        private void ApplyCameraTransform()
        {
            Quaternion rotation = Quaternion.Euler(cameraRotation.xRot, cameraRotation.yRot, 0);
            transform.position = playerTarget.position + rotation * cameraOffset;
            transform.LookAt(playerTarget.position + Vector3.up * 1.5f);
        }
    }

    [Serializable]
    public struct MouseSensitivity
    {
        public float horizontal;
        public float vertical;
        public bool invertH;
        public bool invertV;
    }

    [Serializable]
    public struct CameraRotation
    {
        public float xRot;
        public float yRot;
    }

    [Serializable]
    public struct CameraAngle
    {
        public float min;
        public float max;
    }
}
