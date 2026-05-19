using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonState : MonoBehaviour
{
    [SerializeField] protected Button button;
    [SerializeField] protected Sprite clickButton;
    [SerializeField] protected Sprite idleButton;

    public void ClickButton()
    {
        button.image.sprite = clickButton;
    }

    public void IdleButton()
    {
        button.image.sprite = idleButton;
    }

}
