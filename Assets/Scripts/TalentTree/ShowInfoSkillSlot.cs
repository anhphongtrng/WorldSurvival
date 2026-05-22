using UnityEngine;
using UnityEngine.EventSystems;

public class ShowInfoSkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] protected SkillSlot skillSlot;

    private void Awake()
    {
        skillSlot = GetComponent<SkillSlot>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        SkillToolTipUI.instance.Show(skillSlot.skillSO);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SkillToolTipUI.instance.Hide();
    }


}
