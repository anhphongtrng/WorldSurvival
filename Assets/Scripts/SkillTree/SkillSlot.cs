using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using NUnit.Framework;
using System.Collections.Generic;

public class SkillSlot : MonoBehaviour
{
    [SerializeField] protected List<SkillSlot> prerequisiteSkills; // List of prerequisite skills that must be unlocked before this skill can be unlocked
    [SerializeField] protected SkillSO skillSO;

    public int currentLevel;
    public bool isUnlocked;

    public Image skillIcon;
    public TextMeshProUGUI skillLevelText;
    public Button skillButton;

    // Using Observer pattern to notify the SkillTreeManager when a skill is upgraded
    public static event Action<SkillSlot> OnAbilityPointSpent;
    public static event Action<SkillSlot> OnSkillMaxed;

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
            skillIcon.color = Color.white;
        }
        else
        {
            skillButton.interactable = false;
            skillLevelText.text = "Locked";
            skillIcon.color = Color.gray;
        }
    }

    public void TryUpgradeSkill()
    {
        if (isUnlocked && currentLevel < skillSO.maxLevel && SkillTreeManager.instance.availableSkillPoints > 0)
        {
            currentLevel++;
            OnAbilityPointSpent?.Invoke(this); // Notify the SkillTreeManager that a skill point was spent
            if (currentLevel >= skillSO.maxLevel)
            {
                OnSkillMaxed?.Invoke(this); // Notify that the skill has reached max level
            }
            UpdateUI();
        }
    }

    public bool CanUnlockSkill()
    {
        foreach (SkillSlot prerequisite in prerequisiteSkills)
        {
            if (!prerequisite.isUnlocked || prerequisite.currentLevel < prerequisite.skillSO.maxLevel)
            {
                return false;
            }
        }
        return true;
    }

    public void UnlockSkill()
    {
        isUnlocked = true;
        UpdateUI();
    }
}
