using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Character
{
    public string _nameofCharacter;
    public Sprite _previewCharacter;
    public GameObject _characterPrefabs;
    public string _description;

}
public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; }
    [SerializeField] private List<Character> characters;
    [SerializeField] private Character _characterSelected;
    [SerializeField] private Character _defaultCharacter;
    //event
    public static event Action OnCharacterUnlocked;
    public static event Action OnCharacterSelected;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
           Destroy(this.gameObject);
           return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        if(_characterSelected._previewCharacter == null)
        {
            _characterSelected = _defaultCharacter;
        }
    }
    public void UnlockCharacter(Character _character)
    {
        characters.Add(_character);
    }
    public void SelectCharacter(Character _character)
    {
        _characterSelected = _character;
        OnCharacterSelected?.Invoke();
    }
    public Character GetCharacterSelected() {
        return _characterSelected;
    }
    public void ChangeSelectedCharacter(int step)
    {
        int currentIndex = characters.IndexOf(_characterSelected);
        //if((currentIndex == 0 && step == -1) || (currentIndex == characters.Count && step ==1)) {
        //    return;
        //}
        int newIndex = (currentIndex + step + characters.Count) % characters.Count;
        _characterSelected = characters[newIndex];
        OnCharacterSelected?.Invoke();
    }
}
