using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillSlot : MonoBehaviour
{
    [SerializeField] protected SkillSO skillSO;

    public int currentLevel;
    public bool isUnlocked;

    public Image skillIcon;
    public TextMeshProUGUI skillLevelText;
    public Button skillButton;

    private void OnValidate()
    {
        if(skillSO != null && skillLevelText != null)
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        skillIcon.sprite = skillSO.skillIcon;

        if(isUnlocked)
        {
            skillButton.interactable = true;
            skillLevelText.text = currentLevel.ToString() + "/" + skillSO.maxLevel.ToString();
            skillIcon.color = Color.white; // Set to normal color when unlocked
        }
        else
        {
            skillButton.interactable = false;
            skillLevelText.text = "Locked";
            skillIcon.color = Color.gray; // Set to gray when locked
        }
    }

    public void TryUpgradeSkill()
    {
        if (isUnlocked && currentLevel < skillSO.maxLevel)
        {
            // Here you would typically check if the player has enough skill points
            // and then deduct them before upgrading the skill.
            currentLevel++;
            UpdateUI();
        }
    }
}
