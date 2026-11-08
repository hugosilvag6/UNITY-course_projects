using UnityEngine;

[CreateAssetMenu(fileName = "WeaponInfoSO", menuName = "Scriptable Objects/WeaponInfoSO")]
public class WeaponInfoSO : ScriptableObject {
    public GameObject weaponPrefab;
    public float weaponCooldown;
    public bool shouldFollowMouse;
    public int weaponDamage;
    public float projectileRange;
}
