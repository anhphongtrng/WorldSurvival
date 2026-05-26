using TMPro;
using UnityEngine;

public class StageResultUI : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI worldLevelText;
    [SerializeField] protected TextMeshProUGUI enemiesKilledText;
    [SerializeField] protected TextMeshProUGUI miniBossKilledText;

    private void Update()
    {
        worldLevelText.text = "World Level: " + GameController.instance.worldLevel.ToString();
        enemiesKilledText.text = "Enemies Killed: " + EnemySpawner.instance.enemiesKilled.ToString();
        miniBossKilledText.text = "Mini Boss Killed: " + GameController.instance.miniBossesKilled.ToString();
    }

    public void OnUpgrade()
    {
        UIController.instance.SetStageResultPanel(false);
        UIController.instance.SetTalentsCanvas(true);
    }

}
