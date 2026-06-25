using UnityEngine;

[CreateAssetMenu(fileName = "WorldDataSO", menuName = "Worlds/WorldDataSO")]
public class WorldDataSO : ScriptableObject
{
    [Header("World Data")]
    public int worldIndex;
    public string worldName;
    public string worldDescription;
    public Sprite worldThumbnail;
    public string worldSceneName;

    [Header("Visuals")]
    public Color worldColor = Color.white;
}
