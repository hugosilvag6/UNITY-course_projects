using System.Collections;
using UnityEngine;

public class Grape : MonoBehaviour, IEnemy {
    // Components
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    // References
    [SerializeField] private GameObject grapeProjectilePrefab;
    // Attack
    [SerializeField, Min(0.01f)] private float attackCooldown = 3f;

    // Unity Lifecycle
    private void Awake() {
        myAnimator = GetComponentInChildren<Animator>();
        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // IEnemy Implementation
    public IEnumerator Attack() {
        FlipSpriteToFacePlayer();
        myAnimator.SetTrigger("Attack");

        yield return new WaitForSeconds(attackCooldown);
    }

    // Private Methods
    public void SpawnProjectileAnimationEvent() { // Fizemos um Animation Event, pra lançar o ataque no fim da animação
        GameObject newBullet = Instantiate(
            grapeProjectilePrefab,
            transform.position,
            transform.rotation
        );
    }

    private void FlipSpriteToFacePlayer() {
        float myX = transform.position.x;
        float playerX = PlayerController.Instance.transform.position.x;
        if (myX - playerX > 0) {
            mySpriteRenderer.flipX = true;
        } else {
            mySpriteRenderer.flipX = false;
        }
    }
}
