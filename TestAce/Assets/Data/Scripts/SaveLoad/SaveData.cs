using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class LevelSaveData
{
    public int level;
    public int currentStar;
}
[System.Serializable]
public class CharacterSaveData
{
    public string name;
    public bool isLock;
}

[System.Serializable]
public class Wrapper<T>
{
    public List<T> items;
    public Wrapper(List<T> items) { this.items = items; }
}
