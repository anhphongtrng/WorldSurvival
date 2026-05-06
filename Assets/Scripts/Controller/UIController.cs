using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textTimer;
    [SerializeField] protected Timer timer;
    [SerializeField] protected Canvas skillsCanvas;

    protected void Start()
    {
        skillsCanvas.enabled = false;
    }

    protected void Update()
    {
        textTimer.text = string.Format("{0:00}:{1:00}", timer.minutes, timer.seconds);
        if(timer.IsOverTime())
        {
            skillsCanvas.enabled = true;
        }
    }
}
