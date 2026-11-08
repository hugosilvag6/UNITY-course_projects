using UnityEngine;
using UnityEngine.InputSystem; // Necessário para usar o Keyboard
using TMPro; // Necessário para usar o TextMeshPro

public class Driver: MonoBehaviour {
    bool hasBoost = false;
    [SerializeField] float regularSpeed = 5f;
    [SerializeField] float boostSpeed = 10f;
    [SerializeField] float steerSpeed = 200f;

    [SerializeField] TMP_Text boostText;

    void setBoost(bool value) {
        hasBoost = value;
        boostText.gameObject.SetActive(value);
    }

    void Start() {
        setBoost(false);
    }

    void Update() {
        float finalSpeed = hasBoost ? boostSpeed : regularSpeed;
        // Para acessar inputs é necessário fazer o import
        if (Keyboard.current.wKey.isPressed) {
            transform.Translate(0, finalSpeed * Time.deltaTime, 0);
        } else if (Keyboard.current.sKey.isPressed) {
            transform.Translate(0, -finalSpeed * Time.deltaTime, 0);
        }
        
        if (Keyboard.current.aKey.isPressed) {
            transform.Rotate(0, 0, steerSpeed * Time.deltaTime);
        } else if (Keyboard.current.dKey.isPressed) {
            transform.Rotate(0, 0, -steerSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        setBoost(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Boost") && !hasBoost) {
            setBoost(true);
            Destroy(other.gameObject);
        }
    }
}
