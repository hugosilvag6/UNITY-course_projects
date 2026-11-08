using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    // Types
    private enum State {
        Roaming,
        Attacking
    }

    // Configuration
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private bool stopMovingWhileAttacking = false;
    [SerializeField] private MonoBehaviour enemyType;
    // Components
    private EnemyPathfinding enemyPathfinding;
    // State
    private State? state;
    private Coroutine stateCoroutine;

    // Unity Lifecycle
    private void Awake() {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    private void Start() {
        ChangeState(State.Roaming);
    }

    private void Update() {
        UpdateState();
    }

    // State Management
    private void UpdateState() {
        float distanceToPlayer = Vector2.Distance(
            transform.position,
            PlayerController.Instance.transform.position
        );

        if (distanceToPlayer <= attackRange) {
            ChangeState(State.Attacking);
        } else {
            ChangeState(State.Roaming);
        }
    }

    private void ChangeState(State newState) {
        if (state == newState) {
            return;
        }

        if (stateCoroutine != null) {
            StopCoroutine(stateCoroutine);
        }

        state = newState;

        switch (state) {
            case State.Roaming:
                stateCoroutine = StartCoroutine(RoamCoroutine());
                break;

            case State.Attacking:
                stateCoroutine = StartCoroutine(AttackCoroutine());
                break;
        }
    }

    // State Behaviors
    private IEnumerator RoamCoroutine() {
        while (state == State.Roaming) {
            Vector2 roamDirection = GetRoamingDirection();

            enemyPathfinding.SetTargetPosition(roamDirection);

            yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator AttackCoroutine() {
        if (stopMovingWhileAttacking) {
            enemyPathfinding.StopMoving();
        }

        while (state == State.Attacking) {
            yield return (enemyType as IEnemy).Attack();
        }
    }

    private Vector2 GetRoamingDirection() {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
