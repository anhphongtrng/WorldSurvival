using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textTimer;
    [SerializeField] protected Timer timer;

    protected void Update()
    {
        textTimer.text = string.Format("{0:00}:{1:00}", timer.minutes, timer.seconds);
    }
}
