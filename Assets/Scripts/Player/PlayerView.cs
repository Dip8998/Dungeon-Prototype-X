using DPX.ScriptableObjects;
using UnityEngine;

namespace DPX.Player
{
	public class PlayerView : MonoBehaviour
	{
        [SerializeField] private PlayerSO playerData;

		private PlayerController controller;

        private void Awake()
        {
            controller = new PlayerController(this, playerData);
        }

        private void Start()
        {
            controller?.StartPlayer();
        }

        private void Update()
        {
            controller?.UpdatePlayer();
        }
    }
}
