using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : Weapon {
    // References
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed = 22f;
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

        GameObject newArrow = Instantiate(
            arrowPrefab,
            transform.position,
            transform.rotation
        // não fizemos ser filha do Bow pq se não ela vai rodar quando o arco rodar
        );

        Vector2 targetDirection = GetTargetDirection(newArrow.transform.position);
        if (targetDirection.sqrMagnitude > 0f) {
            newArrow.transform.right = targetDirection;
        }

        Projectile projectile = newArrow.GetComponent<Projectile>();
        projectile.Initialize(Info.weaponDamage, Info.projectileRange, arrowSpeed);
    }

    // Private Methods
    private Vector2 GetTargetDirection(Vector3 projectilePosition) {
        Vector2 mousePosition = pointAction.ReadValue<Vector2>();
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        targetPosition.z = 0f;

        return targetPosition - projectilePosition;
    }
}
