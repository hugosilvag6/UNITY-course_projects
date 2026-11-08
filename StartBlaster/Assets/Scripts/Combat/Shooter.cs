using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour {
    [Header("Base Variables")]
    [SerializeField] GameObject projectilePrefab;
    GameObject projectilesContainer;
    float projectileSpeed = 10f;
    float projectileLifetime = 5f;
    [SerializeField] float fireRate = 0.2f;

    [Header("AI Variables")]
    [SerializeField] bool isAI = false;

    [HideInInspector] public bool isFiring; // o hide tá aqui pq o public sempre aparece, queremos esconder.
    Coroutine fireCoroutine;
    AudioManager audioManager;

    void Awake() {
        projectilesContainer = GameObject.Find("Projectiles");
        audioManager = FindFirstObjectByType<AudioManager>();

        if (isAI) {
            isFiring = true;
        }
    }

    private void Update() {
        Fire();
    }

    void Fire() {
        if (isFiring && fireCoroutine == null) {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
    }

    IEnumerator FireContinuously() {
        while (isFiring) {
            GameObject projectile = Instantiate(
                projectilePrefab, // quem
                transform.position, // onde
                transform.rotation, // qual rotation
                projectilesContainer.transform // dentro de onde (aqui é criado dentro do meu container, como child, pra organizar melhor)
            );
            Destroy(projectile, projectileLifetime);

            projectile.transform.rotation = transform.rotation;

            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            // O código abaixo atiraria sempre pra cima
            // projectileRb.linearVelocityY = projectileSpeed;

            // Esse aqui atira "pra frente", pq usa o eixo VERDE no escopo global
            projectileRb.linearVelocity = transform.up * projectileSpeed;

            audioManager.PlayShootingSFX();

            yield return new WaitForSeconds(isAI ? Random.Range(0.5f, 2f) : fireRate);
        }

        StopCoroutine(fireCoroutine);
        fireCoroutine = null;
    }
}
