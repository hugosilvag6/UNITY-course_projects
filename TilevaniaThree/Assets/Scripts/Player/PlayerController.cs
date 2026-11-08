using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    // Configuration
    float moveSpeed = 10f;
    float jumpSpeed = 20f;

    // Components
    Rigidbody2D myRigidbody;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    Animator myAnimator;
    [SerializeField] GameObject gun; // Poderíamos tirar o SerializeField se gun tivesse um .cs como component. Supondo que ele tivesse um Gun.cs, faríamos no start: gun = GetComponentInChildren<Gun>();
    [SerializeField] Bullet bulletPrefab;

    // States
    bool canMove = true;
    Vector2 movementVector;

    // Cached Values
    float originalRigidBodyGravity;

    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponentInChildren<Animator>();
        originalRigidBodyGravity = myRigidbody.gravityScale;
    }

    void Update() {
        if (!canMove) { return; }

        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value) {
        if (!canMove) { return; }

        movementVector = value.Get<Vector2>();
    }

    void OnJump(InputValue value) {
        if (!canMove) { return; }

        // OBS: aqui usamos GetMask em vez de NameToLayer pq queremos passar a máscara, não o índice.
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (value.isPressed) {
            myRigidbody.linearVelocity += new Vector2(0f, jumpSpeed);
        }
    }

    void OnAttack(InputValue value) {
        if (!canMove) { return; }

        if (value.isPressed) {
            // var position = new Vector3(0f, 0f, 0f);
            var position = gun.transform.position;
            var quaternion = transform.rotation; // Poderia ser um "new Quaternion();", mas usuamos a minha rotação mesmo.

            var newBullet = Instantiate(bulletPrefab, position, quaternion);
            newBullet.Initialize(transform.localScale.x);
        }
    }

    void Run() {
        float x = moveSpeed * movementVector.x;
        // Aqui basicamente dizemos: "faça no eixo Y o que o rigidBody acha que vc deveria fazer".
        // Antes de seguir na aula, eu tinha feito "float y = moveSpeed", o que forçava o player a cair de acordo com a moveSpeed. Mas aí é uma força artificial, criada manualmente.
        float y = myRigidbody.linearVelocity.y;
        myRigidbody.linearVelocity = new Vector2(x, y);

        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", hasHorizontalSpeed);
    }

    void FlipSprite() {
        // Mathf.Abs retorna o valor absoluto (maior que 0), então se for negativo ele converte.
        // Mathf.Epsilon retorna o menor valor positivo "não zero" que um float pode ter. Fazemos isso pq as vezes o valor não é 0, é um pouco maior (muitas casas decimais adiante, tipo 0.00000001), mas pra nós humanos seria 0.
        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon;

        if (hasHorizontalSpeed) {
            float x = myRigidbody.linearVelocity.x;
            // Sign retorna 1f ou -1f.
            transform.localScale = new Vector2(Mathf.Sign(x), 1f);
        }
    }

    void ClimbLadder() {
        bool isTouchingLadder = myBodyCollider.IsTouchingLayers(LayerMask.GetMask("ClimbingLadder"));
        if (isTouchingLadder) {
            // Movimento
            float x = myRigidbody.linearVelocity.x;
            float y = moveSpeed * movementVector.y;
            myRigidbody.linearVelocity = new Vector2(x, y);

            // Remoção do efeito de "cair lentamente" na escada
            myRigidbody.gravityScale = 0f;
        } else {
            myRigidbody.gravityScale = originalRigidBodyGravity;
        }

        bool hasVerticalSpeed = Mathf.Abs(myRigidbody.linearVelocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", hasVerticalSpeed && isTouchingLadder);
    }

    public void TakeDamage() {
        if (!canMove) { return; }

        canMove = false;
        myAnimator.SetTrigger("isDead");
        myRigidbody.linearVelocity += new Vector2(0f, jumpSpeed);
        FindAnyObjectByType<GameSession>().ProcessPlayerDeath();
    }

    public void EnteredPortal() {
        canMove = false;
    }
}