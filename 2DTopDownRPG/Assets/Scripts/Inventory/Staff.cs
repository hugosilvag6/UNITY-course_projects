using UnityEngine;
using UnityEngine.InputSystem;

public class Staff : Weapon {
    // Referencess
    [SerializeField] private GameObject laserPrefab;
    // Input
    private InputAction pointAction;
    // Components
    private Animator myAnimator;
    // Unity Lifecycle
    private void Awake() {
        myAnimator = GetComponent<Animator>();
        pointAction = InputSystem.actions.FindAction("Point");
    }

    // Weapon Implementation
    public override void Attack(Vector3 slashEffectSpawnPoint) {
        myAnimator.SetTrigger("Attack");

        GameObject newLaser = Instantiate(
            laserPrefab,
            transform.position,
            transform.parent.rotation
        // não fizemos ser filha do Bow pq se não ela vai rodar quando o arco rodar
        );

        Vector2 targetDirection = GetTargetDirection(newLaser.transform.position);
        if (targetDirection.sqrMagnitude > 0f) {
            newLaser.transform.right = targetDirection;
        }

        MagicLaser laser = newLaser.GetComponent<MagicLaser>();
        laser.Initialize(Info.weaponDamage, Info.projectileRange);
    }

    // Private Methods
    private Vector2 GetTargetDirection(Vector3 projectilePosition) {
        Vector2 mousePosition = pointAction.ReadValue<Vector2>();
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        targetPosition.z = 0f;

        return targetPosition - projectilePosition;
    }
}
