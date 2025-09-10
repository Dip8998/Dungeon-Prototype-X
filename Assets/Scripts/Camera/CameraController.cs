using System;
using UnityEngine;

namespace DPX.PlayerCamera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform playerTarget;
        [SerializeField] private Vector3 cameraOffset = new Vector3(0, 2f, -4f);

        private Vector2 mouseInput;

        private void LateUpdate()
        {
            ApplyCameraTransform();
        }

        private void ApplyCameraTransform()
        {
            if(playerTarget != null)
            {
                transform.position = playerTarget.position + cameraOffset;
                transform.LookAt(playerTarget.position + Vector3.up * 1.5f);
            }
        }
    }
}
