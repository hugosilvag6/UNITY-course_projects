using UnityEngine;

public class Bullet : MonoBehaviour {
    // Configuration
    float bulletSpeed = 20f;

    // Components
    Rigidbody2D myRigidbody;

    void Awake() {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    public void Initialize(float direction) {
        myRigidbody.linearVelocity = new Vector2(direction * bulletSpeed, 0f);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        int enemyIndex = LayerMask.NameToLayer("Enemy");
        if (other.gameObject.layer == enemyIndex) {
            var enemyController = other.gameObject.GetComponent<EnemyController>();
            enemyController.TakeDamage();
        }
        Destroy(gameObject);
    }
}
