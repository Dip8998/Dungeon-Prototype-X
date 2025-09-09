namespace DPX.Interfaces
{
    public interface IWeapon
    {
        void Attack();
        void UpdateWeapon();
        void OnEquip();
        void OnUnequip();
    }
}