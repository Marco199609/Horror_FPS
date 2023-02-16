public interface IWeaponUI
{
    void UIUpdate(WeaponData CurrentWeaponData, WeaponController weaponController);
    void CrosshairColorUpdate(bool enemyInRange);
}