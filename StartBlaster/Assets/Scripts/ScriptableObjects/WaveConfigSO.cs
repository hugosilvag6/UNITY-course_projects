using UnityEngine;

// Waves seguem um path específico com uma moveSpeed específica
[CreateAssetMenu(fileName = "WaveConfigSO", menuName = "Scriptable Objects/WaveConfigSO")]
public class WaveConfigSO : ScriptableObject {
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float enemyMoveSpeed = 5f;
    float timeBetweenEnemySpawns = 1f;
    float enemySpawnVariance = 0.3f;

    public Transform GetStartingWaypoint() {
        return pathPrefab.GetChild(0);
    }

    public float GetEnemyMoveSpeed() {
        return enemyMoveSpeed;
    }

    public Transform[] GetWaypoints() {
        Transform[] waypoints = new Transform[pathPrefab.childCount];
        for (int i = 0; i < pathPrefab.childCount; i++) {
            waypoints[i] = pathPrefab.GetChild(i);
        }
        return waypoints;
    }

    public int GetEnemyCount() {
        return enemyPrefabs.Length;
    }

    public GameObject GetEnemyPrefab(int index) {
        return enemyPrefabs[index];
    }

    public float GetRandomEnemySpawnTime() {
        float spawnTime = Random.Range(
            timeBetweenEnemySpawns - enemySpawnVariance,
            timeBetweenEnemySpawns + enemySpawnVariance
        );
        return Mathf.Max(spawnTime, 0.2f); // não queremos tempo negativo nem 0, se nao fica um inimigo em cima do outro
    }
}
