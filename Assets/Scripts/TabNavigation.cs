using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class TabNavigation : MonoBehaviour
{
    [Header("Input Fields")]
    public TMP_InputField[] tabOrder;

    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            MoveToNextField();
        }
    }

    private void MoveToNextField()
    {
        for (int i = 0; i < tabOrder.Length; i++)
        {
            if (tabOrder[i].isFocused)
            {
                int nextIndex = (i + 1) % tabOrder.Length; // het field cuoi thi quay lai field dau
                tabOrder[nextIndex].Select();
                tabOrder[nextIndex].ActivateInputField();
                return;
            }
        }

        // Neu chua field nao dang focus, mac dinh focus vao field dau tien
        if (tabOrder.Length > 0)
        {
            tabOrder[0].Select();
            tabOrder[0].ActivateInputField();
        }
    }
}