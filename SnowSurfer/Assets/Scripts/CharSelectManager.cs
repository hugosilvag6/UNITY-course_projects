using UnityEngine;

public class CharSelectManager : MonoBehaviour {
    // Aqui pegamos o GameObject pq só precisamos ativar/desativas-lo, não precisamos de nenhuma informação específica do canvas
    [SerializeField] GameObject scoreCanvas;
    [SerializeField] GameObject dinoSprite;
    [SerializeField] GameObject frogSprite;

    void Start() {
        Time.timeScale = 0;
    }

    void BeginGame() {
        Time.timeScale = 1;
        scoreCanvas.SetActive(true);
        // gameObject existe como source no FiveM. Ele acessa o objeto ao qual esse component(script) tá linkado
        gameObject.SetActive(false);
    }

    public void ChooseDino() {
        dinoSprite.SetActive(true);
        BeginGame();
    }

    public void ChooseFrog() {
        frogSprite.SetActive(true);
        BeginGame();
    }
}
