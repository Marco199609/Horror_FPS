public interface IWeaponUI
{
    void CrosshairActivate(WeaponGeneralData weaponGeneralData);
    void CrosshairDeactivate(WeaponGeneralData weaponGeneralData);
    void UIUpdate(WeaponGeneralData weaponGeneralData, WeaponData CurrentWeaponData, WeaponController weaponController);
}