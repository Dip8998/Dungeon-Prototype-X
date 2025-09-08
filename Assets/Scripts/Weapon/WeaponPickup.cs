using UnityEngine;
using DPX.Weapons;
using DPX.Player;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private WeaponView weaponToUnlock;

    private void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!weaponToUnlock) return;

        if (other.CompareTag("Player"))
        {
            PlayerView playerView = other.GetComponent<PlayerView>();
            if (playerView != null)
            {
                playerView.CollectWeapon(weaponToUnlock);
                Destroy(gameObject);
            }
        }
    }
}