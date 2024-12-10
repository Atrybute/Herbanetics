using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Custom/LevelData")]
public class LevelData : ScriptableObject
{
    public int levelIndex;
    public string sceneName;
    public string levelName;
}
