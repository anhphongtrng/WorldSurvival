using UnityEngine;
using UnityEngine.UI;

public class NextStageButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(() =>
        {
            GameController.instance.LoadNextStage();
        });
    }
}