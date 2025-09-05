using UnityEngine;

namespace DPX.PlayerCamera
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private Transform playerTarget;
		[SerializeField] private float smoothSpeed;

		private Vector3 offset;
		private Vector3 currentVelocity = Vector3.zero;

        private void Awake()
        {
            offset = transform.position - playerTarget.position;
        }

        private void LateUpdate()
        {
            Vector3 targetPos = playerTarget.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothSpeed);
        }
    }
}
