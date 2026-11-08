using UnityEngine;

public class CoinManager : MonoBehaviour {
    // References
    [SerializeField] AudioClip coinPickupSFX;

    // State
    bool wasCollected = false; // adicionado só pra evitar double-pickups

    private void OnTriggerEnter2D(Collider2D other) {
        if (wasCollected) { return; }

        int enemyIndex = LayerMask.NameToLayer("Player");
        if (other.gameObject.layer == enemyIndex) {
            wasCollected = true;

            // Audio:
            // Poderíamos fazer um .Play() em uma referência do componente AudioSource desse GameObjec, mas o som pararia assim que destruíssemos o objeto.
            // var audioSource = GetComponent<AudioSource>();
            // audioSource.Play();

            // Aqui o que estamos fazendo é acessando um método static de AudioSource. PlayClipAtPoint não depende de uma instância.
            AudioSource.PlayClipAtPoint(coinPickupSFX, transform.position, 1f);

            FindAnyObjectByType<GameSession>().ProccessCoinCollected();

            Destroy(gameObject);
        }
    }
}
