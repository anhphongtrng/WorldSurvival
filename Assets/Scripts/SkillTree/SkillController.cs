using UnityEngine;

public class SkillController : MonoBehaviour
{
    protected void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += SkillPointSpentFor;
    }

    protected void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= SkillPointSpentFor;
    }

    public void SkillPointSpentFor(SkillSlot skillSlot)
    {
        string skillName = skillSlot.skillSO.skillName;

        switch (skillName)
        {
            case "Max Health Boost":
                StatsController.instance.UpdateMaxHealth(1);
                break;
            case "Attack Boost":
                StatsController.instance.UpdateBeamDamage(1);
                break;
            case "Speed Boost":
                StatsController.instance.UpdateMoveSpeed(1f);
                break;
        }
    }
}
