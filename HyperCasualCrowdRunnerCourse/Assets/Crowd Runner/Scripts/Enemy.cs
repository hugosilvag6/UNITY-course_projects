using System;
using UnityEngine;

public class Enemy : MonoBehaviour {
    enum State { Idle, Running }

    [Header("Settings")]
    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;
    private State state;
    private Transform targetRunner;

    [Header("Events")]
    public static Action onRunnerDied;

    private void Update() {
        ManageState();
    }

    private void ManageState() {
        switch (state) {
            case State.Idle:
                SearchForTarget();
                break;
            case State.Running:
                RunTowardsTarget();
                break;
        }
    }

    private void SearchForTarget() {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, searchRadius);

        for (int i = 0; i < detectedColliders.Length; i++) {
            if (detectedColliders[i].TryGetComponent(out Runner runner)) {
                if (runner.IsTarget()) { continue; }

                runner.SetTarget();
                targetRunner = runner.transform;

                StartRunningTowardsTarget();
            }
        }
    }

    private void StartRunningTowardsTarget() {
        state = State.Running;
        GetComponent<Animator>().Play("Run");
    }

    private void RunTowardsTarget() {
        if (targetRunner == null) { return; }

        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetRunner.position) < .1f) {
            onRunnerDied?.Invoke();
            Destroy(targetRunner.gameObject);
            Destroy(gameObject);
        }
    }
}
