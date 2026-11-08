using UnityEngine;

public class EnemyController : MonoBehaviour {
    // Configuration
    [SerializeField] float moveSpeed = 1f;

    // Components
    Rigidbody2D myRigidbody;

    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        myRigidbody.linearVelocity = new Vector2(moveSpeed, myRigidbody.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        int layerIndex = LayerMask.NameToLayer("Player");
        if (other.gameObject.layer == layerIndex) {
            var playerController = other.gameObject.GetComponent<PlayerController>();
            playerController?.TakeDamage();
        }
    }

    public void ChangeDirection() {
        // Change direction
        moveSpeed = -moveSpeed;
        // Flip sprite according to direction
        transform.localScale = new Vector2(Mathf.Sign(moveSpeed), 1f);
    }

    public void TakeDamage() {
        Destroy(gameObject);
    }
}