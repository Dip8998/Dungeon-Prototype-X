namespace DPX.Weapons
{
    public interface IWeapon
    {
        void Attack();
        void UpdateWeapon();
        void OnEquip();
        void OnUnequip();
    }
}