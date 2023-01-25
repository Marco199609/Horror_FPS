public interface IWeaponShoot
{
    bool DealDamage { get; }
    bool PlaySound { get; }

    void Shoot(WeaponInput weaponInput, WeaponData weaponData);
}