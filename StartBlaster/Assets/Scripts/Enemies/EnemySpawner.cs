using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] WaveConfigSO[] waveConfigs;
    float timeBetweenWaves = 1f;
    [SerializeField] bool isLooping = true;

    void Start() {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies() {
        while (isLooping) {
            foreach (var wave in waveConfigs) {
                for (int i = 0; i < wave.GetEnemyCount(); i++) {
                    SpawnEnemy(i, wave);
                    yield return new WaitForSeconds(wave.GetRandomEnemySpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
    }

    void SpawnEnemy(int enemyIndex, WaveConfigSO wave) {
        GameObject enemyPrefab = wave.GetEnemyPrefab(enemyIndex);
        GameObject newEnemy = Instantiate(
            enemyPrefab, // quem
            wave.GetStartingWaypoint().position, // onde
            enemyPrefab.transform.rotation, // qual rotation
            transform // dentro de onde (aqui é criado dentro do meu gameObject, como child, pra organizar melhor)
        );
        // O pathFinding usa a currentWave no Update(). Em teoria, pode parecer que ele vai tentar acessar esse parametro antes de tê-lo, mas o ciclo é síncrono:
        // EnemySpawner.SpawnEnemies()
        // ↓
        // Instantiate(...)
        //         ↓
        // Enemy é criado
        //         ↓
        // Awake() do Enemy pode rodar aqui
        //         ↓
        // OnEnable() pode rodar aqui
        //         ↓
        // Instantiate retorna
        //         ↓
        // GetComponent<PathFinding>()
        //         ↓
        // Init(currentWave)
        //         ↓
        // SpawnEnemies termina
        //         ↓
        // ...
        // próximo momento apropriado do PlayerLoop
        //         ↓
        // Update() do PathFinding
        var pathFinding = newEnemy.GetComponent<PathFinding>();
        pathFinding.Init(wave);
    }
}
