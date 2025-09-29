using UnityEngine;
using DPX.Weapons;
using DPX.Player;
using DPX.Main;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private PlayerWeaponView weaponToUnlock;

    private void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!weaponToUnlock) return;

        if (other.CompareTag("Player"))
        {
            if (GameService.Instance != null && GameService.Instance.PlayerService != null)
            {
                GameService.Instance.PlayerService.AddWeaponToPlayer(weaponToUnlock);
                Destroy(gameObject);
            }
        }
    }
}