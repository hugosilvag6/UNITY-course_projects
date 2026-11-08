using UnityEngine;

public class AudioManager : MonoBehaviour {
    [Header("Shooting SFX")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0, 1)] float shootingVolume = 0.05f;

    [Header("Damage SFX")]
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0, 1)] float damageVolume = 0.05f;

    // Singleton handling
    public static AudioManager instance;

    void ManageSingleton() {
        // int instanceCount = FindObjectsByType<AudioManager>(FindObjectsSortMode.None).Length;
        // if (instanceCount > 1) {
        if (instance != null) {
            // setactive tá aqui pra evitar que seja possível usar esse objeto antes de destruir.
            // O Destroy não destrói sempre no momento que chama. pode levar algum tempo,
            // pois ele na verdade "agenda" a destruição pro fim do frame
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Awake() {
        ManageSingleton();
    }

    // Actions
    public void PlayShootingSFX() {
        if (shootingClip == null) { return; }

        AudioSource.PlayClipAtPoint(
            shootingClip,
            Camera.main.transform.position,
            shootingVolume
        );
    }

    public void PlayDamageSFX() {
        if (damageClip == null) { return; }

        AudioSource.PlayClipAtPoint(
            damageClip,
            Camera.main.transform.position,
            damageVolume
        );
    }
}
