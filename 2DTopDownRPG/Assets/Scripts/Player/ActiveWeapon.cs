using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveWeapon : Singleton<ActiveWeapon> {
    // References
    [SerializeField] private Transform slashEffectSpawnPoint;
    // Input
    private InputAction attackAction;
    private InputAction pointAction;
    // States
    private Coroutine attackCoroutine;
    private Weapon currentWeapon;

    // Unity Lifecycle
    protected override void Awake() {
        base.Awake();

        attackAction = InputSystem.actions.FindAction("Attack");
        pointAction = InputSystem.actions.FindAction("Point");
    }

    private void Update() {
        Attack();
        MouseFollowWithOffset();
    }

    // Private Methods
    private void Attack() {
        if (currentWeapon && attackAction.IsPressed() && attackCoroutine == null) {
            attackCoroutine = StartCoroutine(AttackContinuously());
        }
    }

    private IEnumerator AttackContinuously() {
        while (currentWeapon && attackAction.IsPressed()) {
            currentWeapon.Attack(slashEffectSpawnPoint.position);

            yield return new WaitForSeconds(currentWeapon.Info.weaponCooldown);
        }

        attackCoroutine = null;
    }

    private void MouseFollowWithOffset() {
        if (!currentWeapon) {
            return;
        }

        var playerPosition = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);
        Vector2 mousePosition = pointAction.ReadValue<Vector2>();

        // O angulo abaiox deixa a animação mais "dinâmica", mas não gostei então desliguei.
        float angle = 0;
        if (currentWeapon.Info.shouldFollowMouse) {
            angle = Mathf.Atan2(
            mousePosition.y - playerPosition.y,
            Mathf.Abs(mousePosition.x - playerPosition.x)
            ) * Mathf.Rad2Deg;
        }

        if (mousePosition.x < playerPosition.x) {
            transform.rotation = Quaternion.Euler(0, -180, angle);
        } else {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    // Public Methods
    public void SetActiveWeapon(WeaponInfoSO weaponInfo) {
        if (currentWeapon) {
            Destroy(currentWeapon.gameObject);
            currentWeapon = null;
        }

        if (weaponInfo == null) {
            return;
        }

        // Reseta a rotação, que pode estar "rodada" devido à posição do mouse
        transform.rotation = Quaternion.Euler(0, 0, 0);

        GameObject newWeapon = Instantiate(
            weaponInfo.weaponPrefab,
            transform.position,
            Quaternion.identity,
            transform
        );
        Weapon weapon = newWeapon.GetComponent<Weapon>();

        if (!weapon) {
            Destroy(newWeapon);
            return;
        }

        weapon.Initialize(weaponInfo);
        currentWeapon = weapon;
    }
}
