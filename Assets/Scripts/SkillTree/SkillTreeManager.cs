using UnityEngine;
using TMPro;

public class SkillTreeManager : MonoBehaviour
{
    public SkillSlot[] skillSlots;
    public TextMeshProUGUI skillPointText;
    public int availableSkillPoints;

    private void Start()
    {
        foreach (SkillSlot slot in skillSlots)
        {
            slot.skillButton.onClick.AddListener(slot.TryUpgradeSkill);
        }

        UpdateAbilityPoints(0);
    }

    public void UpdateAbilityPoints(int amount)
    {
        availableSkillPoints += amount;
        skillPointText.text = ": " + availableSkillPoints.ToString();
    }

}
