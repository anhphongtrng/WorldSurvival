using UnityEngine;
using TMPro;

public class PlayerHeathUI : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI playerHealthText;

    protected void Update()
    {
        playerHealthText.text = "HP: " + StatsController.instance.currentHealth + "/" + StatsController.instance.maxHealth;
    }
}
