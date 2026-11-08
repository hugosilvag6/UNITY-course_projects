using TMPro;
using UnityEngine;

public enum BonusType { Addition, Subtraction, Multiplication, Division }

public class Doors : MonoBehaviour {

    [Header("Elements")]
    [SerializeField] private SpriteRenderer leftDoorSpriteRenderer;
    [SerializeField] private TextMeshPro leftDoorText;
    [SerializeField] private SpriteRenderer rightDoorSpriteRenderer;
    [SerializeField] private TextMeshPro rightDoorText;
    [SerializeField] private Collider myCollider;

    [Header("Settings")]
    [SerializeField] private BonusType leftDoorBonusType;
    [SerializeField] private int leftDoorBonusAmount;
    [SerializeField] private BonusType rightDoorBonusType;
    [SerializeField] private int rightDoorBonusAmount;
    [SerializeField] private Color positiveColor;
    [SerializeField] private Color negativeColor;

    private void Start() {
        ConfigureDoors();
    }

    private void ConfigureDoors() {
        switch (leftDoorBonusType) {
            case BonusType.Addition:
                leftDoorSpriteRenderer.color = positiveColor;
                leftDoorText.text = "+" + leftDoorBonusAmount.ToString();
                break;
            case BonusType.Subtraction:
                leftDoorSpriteRenderer.color = negativeColor;
                leftDoorText.text = "-" + leftDoorBonusAmount.ToString();
                break;
            case BonusType.Multiplication:
                leftDoorSpriteRenderer.color = positiveColor;
                leftDoorText.text = "x" + leftDoorBonusAmount.ToString();
                break;
            case BonusType.Division:
                leftDoorSpriteRenderer.color = negativeColor;
                leftDoorText.text = "÷" + leftDoorBonusAmount.ToString();
                break;
        }

        switch (rightDoorBonusType) {
            case BonusType.Addition:
                rightDoorSpriteRenderer.color = positiveColor;
                rightDoorText.text = "+" + rightDoorBonusAmount.ToString();
                break;
            case BonusType.Subtraction:
                rightDoorSpriteRenderer.color = negativeColor;
                rightDoorText.text = "-" + rightDoorBonusAmount.ToString();
                break;
            case BonusType.Multiplication:
                rightDoorSpriteRenderer.color = positiveColor;
                rightDoorText.text = "x" + rightDoorBonusAmount.ToString();
                break;
            case BonusType.Division:
                rightDoorSpriteRenderer.color = negativeColor;
                rightDoorText.text = "÷" + rightDoorBonusAmount.ToString();
                break;
        }
    }

    public int GetBonusAmount(float xPosition) {
        return xPosition > 0 ? rightDoorBonusAmount : leftDoorBonusAmount;
    }

    public BonusType GetBonusType(float xPosition) {
        return xPosition > 0 ? rightDoorBonusType : leftDoorBonusType;
    }

    public void Disable() {
        myCollider.enabled = false;
    }
}