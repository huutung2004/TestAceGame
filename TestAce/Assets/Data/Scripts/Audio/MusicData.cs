using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Music", menuName = "New Music")]
public class MusicData : ScriptableObject
{
    [System.Serializable]
    public class MusicEntry
    {
        public string _name;
        public AudioClip _clip;
        public MusicType _type;
    }
    public List<MusicEntry> audioEntries = new List<MusicEntry>();

    public AudioClip GetClip(string name)
    {
        var entry = audioEntries.Find(e => e._name == name);
        return entry != null ? entry._clip : null;
    }

    public MusicType GetType(string name)
    {
        var entry = audioEntries.Find(e => e._name == name);
        return entry != null ? entry._type : MusicType.SoundEffect;
    }

}
