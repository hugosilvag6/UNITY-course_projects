using System.Collections;
using UnityEngine;

public class MagicLaser : MonoBehaviour {
    // Configuration
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private float growthSpeed = 30f;
    // Components
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D myCollider;
    private SpriteFadeAndDestroy spriteFadeAndDestroy;
    // State
    private int damage;
    private float maxRange;

    // Unity Lifecycle
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<CapsuleCollider2D>();
        spriteFadeAndDestroy = GetComponent<SpriteFadeAndDestroy>();
    }

    // Events
    private void OnTriggerEnter2D(Collider2D other) {
        EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();
        Destructible destructible = other.gameObject.GetComponent<Destructible>();
        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();

        if (enemy) {
            enemy.TakeDamage(damage);
        }

        if (destructible) {
            destructible.DestroySelf();
        }

        if (indestructible || destructible || enemy) {
            Vector3 position = new Vector3(
                transform.position.x + transform.right.x * spriteRenderer.size.x,
                transform.position.y + transform.right.y * spriteRenderer.size.x,
                transform.position.z + transform.right.z * spriteRenderer.size.x
            );
            Instantiate(
                particleOnHitPrefabVFX,
                position,
                transform.rotation
            );
            spriteFadeAndDestroy.FadeAndDestroy();
        }
    }

    // Private Methods
    private IEnumerator AnimateLaser() {
        // // Deixei o código abaixo pra explicar pq ele não funciona:
        // // Sem a Coroutuine, a Unity Engine tenta fazer isso aqui tudo em um frame só. 
        // // Diminuir o multiplicador (de 0.1f pra 0.01f por exemplo) só vai fazer com que a Unity tenha que multiplicar milhares
        // // (ou até milhões de vezes) pra alcançar o resultado que queremos em um frame.
        // // 
        // // E é aí que entra o yield return null da coroutine.
        // private void AnimateLaser() {
        //     while (spriteRenderer.size.x < maxRange) {
        //         spriteRenderer.size = new Vector2(
        //             spriteRenderer.size.x + 0.1f * Time.deltaTime,
        //             spriteRenderer.size.y
        //         );
        //     }
        // }
        while (spriteRenderer.size.x < maxRange) {
            float newHorizontalSize = spriteRenderer.size.x + growthSpeed * Time.deltaTime;
            spriteRenderer.size = new Vector2(newHorizontalSize, spriteRenderer.size.y);
            myCollider.size = new Vector2(newHorizontalSize, myCollider.size.y);
            myCollider.offset = new Vector2(newHorizontalSize / 2, myCollider.offset.y);

            // O yield return null pausa a coroutine nesse ponto e agenda a continuação para o próximo frame.
            yield return null;
        }

        yield return new WaitForSeconds(0.3f);

        spriteFadeAndDestroy.FadeAndDestroy();
    }

    // Public Methods
    public void Initialize(int damage, float maxRange) {
        this.damage = damage;
        this.maxRange = maxRange;

        StartCoroutine(AnimateLaser());
    }
}
