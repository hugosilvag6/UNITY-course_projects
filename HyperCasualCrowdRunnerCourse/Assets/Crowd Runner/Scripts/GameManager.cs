using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // Types
    public enum GameState { Menu, Game, LevelComplete, GameOver }
    // State
    private GameState gameState;
    // Singleton
    public static GameManager instance;

    public static Action<GameState> onGameStateChanged;

    // Unity Lifecycle
    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    private void Start() {
        PlayerPrefs.DeleteAll();
    }

    public void SetGameState(GameState gameState) {
        this.gameState = gameState;
        onGameStateChanged?.Invoke(gameState);
    }

    public bool IsGameState() {
        return gameState == GameState.Game;
    }
}
