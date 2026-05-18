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
        Debug.Log("Pointer entered skill slot: " + skillSlot.name);
        SkillToolTipUI.instance.Show(skillSlot.skillSO);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer exited skill slot: " + skillSlot.name);
        SkillToolTipUI.instance.Hide();

    }


}
