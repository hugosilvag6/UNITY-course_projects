using UnityEngine;

// Setado em um enemy específico
public class PathFinding : MonoBehaviour {
    WaveConfigSO waveConfig;
    Transform[] waypoints;
    int waypointIndex = 0;

    public void Init(WaveConfigSO waveConfig) {
        this.waveConfig = waveConfig;
        // Salva os pontos do path
        waypoints = waveConfig.GetWaypoints();
    }

    void Update() {
        // Força o seguimento do path a cada frame
        FollowPath();
    }

    void FollowPath() {
        // Valida se o ponto atual é menor que o path length
        if (waypointIndex < waypoints.Length) {
            // Pega a posição do target atual
            Vector3 targetPosition = waypoints[waypointIndex].position;
            // Pega move speed
            float moveDelta = waveConfig.GetEnemyMoveSpeed() * Time.deltaTime;
            // Move o ponto atual (param 1) em direção ao alvo (param 2) com a moveSpeed definida (param 3)
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveDelta);

            // Se cheguei, muda pro próximo target
            if (transform.position == targetPosition) {
                waypointIndex++;
            }
        } else {
            Destroy(gameObject);
        }
    }
}
