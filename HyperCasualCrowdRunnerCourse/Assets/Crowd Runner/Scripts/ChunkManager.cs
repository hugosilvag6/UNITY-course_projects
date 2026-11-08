using UnityEngine;

public class ChunkManager : MonoBehaviour {
    [Header("Elements")]
    [SerializeField] private Chunk finishLinePrefab;
    [SerializeField] private LevelSO[] levels;
    [SerializeField] private bool randomizeChunks;

    private Vector3 chunkPosition;
    private FinishLine finishLine;

    // Singleton
    public static ChunkManager instance;

    // Unity Lifecycle
    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    private void Start() {
        // CreateChunks();
        GenerateLevel();
    }

    // Private Methods
    private void GenerateLevel() {
        int currentLevel = GetLevel();
        currentLevel = currentLevel % levels.Length;
        LevelSO level = levels[currentLevel];

        CreateChunks(level.chunks);
    }

    private void CreateChunks(Chunk[] chunks) {
        chunkPosition = Vector3.zero;

        Chunk firstChunk = Instantiate(chunks[0], chunkPosition, Quaternion.identity, transform);
        chunkPosition.z += firstChunk.GetLength() / 2;

        for (int i = 1; i < chunks.Length; i++) {
            int chunkIndex = randomizeChunks ? Random.Range(1, chunks.Length) : i;
            Chunk chunkToCreate = chunks[chunkIndex];

            if (i > 0) {
                chunkPosition.z += chunkToCreate.GetLength() / 2;
            }

            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);
            chunkPosition.z += chunkInstance.GetLength() / 2;
        }

        CreateFinishLine();
    }

    private void CreateFinishLine() {
        Chunk finishChunk = Instantiate(finishLinePrefab, chunkPosition, Quaternion.identity, transform);
        chunkPosition.z += finishChunk.GetLength() / 2;

        if (finishChunk.TryGetComponent(out FinishLine finishLine)) {
            this.finishLine = finishLine;
        }
    }

    public float GetFinishLineZ() {
        return finishLine.GetFinishLineZPosition();
    }

    public int GetLevel() {
        return PlayerPrefs.GetInt("level", 0);
    }
}
