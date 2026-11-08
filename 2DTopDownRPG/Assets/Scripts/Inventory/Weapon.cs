using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    public WeaponInfoSO Info { get; private set; }

    public void Initialize(WeaponInfoSO weaponInfo) {
        Info = weaponInfo;
    }

    public abstract void Attack(Vector3 effectSpawnPoint);
}
