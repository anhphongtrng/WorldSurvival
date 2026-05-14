using UnityEngine;
using TMPro;

public class SkillTreeController : MonoBehaviour
{
    public static SkillTreeController instance;

    public SkillSlot[] skillSlots;
    public TextMeshProUGUI skillPointText;
    public int availableSkillPoints;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointsSpent;
        SkillSlot.OnSkillMaxed += HandleSkillMaxed;
        EnemySpawner.OnAddSkillPoint += AddAbilityPoint;
    }

    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointsSpent;
        SkillSlot.OnSkillMaxed -= HandleSkillMaxed;
        EnemySpawner.OnAddSkillPoint -= AddAbilityPoint;

    }

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

    public void HandleAbilityPointsSpent(SkillSlot skillSlot)
    {
        if (availableSkillPoints > 0)
        {
            UpdateAbilityPoints(-1); // Decrease available skill points by 1 when a skill is upgraded
        }
    }

    public void HandleSkillMaxed(SkillSlot skillSlot)
    {
        foreach (SkillSlot slot in skillSlots)
        {
            if(!slot.isUnlocked && slot.CanUnlockSkill())
            {
                slot.UnlockSkill(); // Unlock the skill if it meets the prerequisites
            }
        }
    }

    public void AddAbilityPoint(int amount)
    {
        UpdateAbilityPoints(amount); // Increase available skill points by the specified amount
    }
}
