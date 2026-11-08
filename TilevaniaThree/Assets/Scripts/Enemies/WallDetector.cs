using UnityEngine;

public class WallDetector : MonoBehaviour {
    // References
    EnemyController enemyController;

    void Awake() {
        // Usamos o código abaixo, e ele quebrou o jogo.
        // Pegávamos qualquer um, então quando um colidia, bugava outro.
        // enemyController = FindAnyObjectByType<EnemyController>();

        // Passamos a buscar o pai DESSE WallDetector em específico, assim cada WallDetector em um pefab procura seu pai, e não "Qualquer pai"
        enemyController = GetComponentInParent<EnemyController>();

        // Outra forma seria transformar o EnemyController enemyController em [SerializeField], e configurar o PREFAB corretamente, linkando o pai ao filho.
    }

    void Update() { }

    private void OnTriggerEnter2D(Collider2D other) {
        int layerIndex = LayerMask.NameToLayer("Ground");
        if (other.gameObject.layer == layerIndex) {
            enemyController.ChangeDirection();
        }
    }
}
