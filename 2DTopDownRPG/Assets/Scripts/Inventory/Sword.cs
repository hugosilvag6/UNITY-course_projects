using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : Weapon {
    // Components
    private Animator myAnimator;
    private BoxCollider2D myCollider;
    // References
    [SerializeField] private GameObject slashEffectPrefab;
    // Instances
    private GameObject slashAnimation;
    // Unity Lifecycle
    private void Awake() {
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    private void Start() {
        myCollider.enabled = false;
    }

    // Events
    private void OnTriggerEnter2D(Collider2D other) {
        EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();
        Destructible destructible = other.gameObject.GetComponent<Destructible>();

        if (enemy) {
            enemy.TakeDamage(Info.weaponDamage);
        }

        if (destructible) {
            destructible.DestroySelf();
        }
    }

    // Weapon Implementation
    public override void Attack(Vector3 slashEffectSpawnPoint) {
        myCollider.enabled = true;
        myAnimator.SetTrigger("Attack");
        slashAnimation = Instantiate(slashEffectPrefab, slashEffectSpawnPoint, this.transform.parent.rotation, this.transform.parent);
    }

    // Animation Events
    private void FlipSpriteWhenSwingingUpAnimationEvent() {
        Vector3 rotation = slashAnimation.transform.eulerAngles;
        rotation.x = 180f;
        slashAnimation.transform.eulerAngles = rotation;
    }

    private void FinishAttackAnimationEvent() {
        myCollider.enabled = false;
    }
}
