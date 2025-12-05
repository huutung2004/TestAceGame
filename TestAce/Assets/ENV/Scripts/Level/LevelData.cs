using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="LevelData", menuName ="new LevelData")]
public class LevelData : ScriptableObject
{
    public int level;
    public int goldReward;
    public GameObject _mapPrefab;
    public int timeLimit;
    public int currentStar;
}
