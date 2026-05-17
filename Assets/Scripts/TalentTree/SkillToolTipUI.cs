using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillToolTipUI : MonoBehaviour
{
    public static SkillToolTipUI instance;

    [SerializeField] private GameObject panel;

    [SerializeField] private TextMeshProUGUI skillNameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI cooldownText;

    [SerializeField] private Image icon;

    private void Awake()
    {
        instance = this;

        Hide();
    }

    public void Show(SkillSO skill)
    {
        panel.SetActive(true);

        skillNameText.text = skill.skillName;
        descriptionText.text = skill.description;
        cooldownText.text =
            "Cooldown: " + skill.cooldown + "s";

        icon.sprite = skill.skillIcon;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}