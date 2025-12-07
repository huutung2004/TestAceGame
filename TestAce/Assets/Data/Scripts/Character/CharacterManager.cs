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
    public int price;
    public bool IsLock;

}
public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; }
    [SerializeField] private List<Character> characters;
    [SerializeField] private Character _characterSelected;
    [SerializeField] private Character _defaultCharacter;
    //event
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
        LoadCharacters();
        if (_characterSelected._previewCharacter == null)
        {
            _characterSelected = _defaultCharacter;
        }
    }
    public void CreateCharacter(Character _character)
    {
        characters.Add(_character);
    }
    public void SelectCharacter(Character _character)
    {
        _characterSelected = _character;
        OnCharacterSelected?.Invoke();
    }
    public Character GetCharacterSelect()
    {
        return _characterSelected;
    }
    public Character GetCharacterSelected()
    {
        if (_characterSelected.IsLock)
        {
            foreach (Character character in characters)
            {
                if (!character.IsLock) return character;
            }
        }
        return _characterSelected;
    }
    public void ChangeSelectedCharacter(int step)
    {
        int currentIndex = characters.IndexOf(_characterSelected);
        int newIndex = (currentIndex + step + characters.Count) % characters.Count;
        _characterSelected = characters[newIndex];
        OnCharacterSelected?.Invoke();
    }
    public void UnlockSelectedCharacter()
    {
        int index = characters.IndexOf(_characterSelected);
        if (index < 0) return;

        Character c = characters[index];
        c.IsLock = false;
        characters[index] = c;
        _characterSelected = c;
        OnCharacterSelected?.Invoke();
    }
    public void SaveCharacters()
    {
        List<CharacterSaveData> saveList = new List<CharacterSaveData>();

        foreach (var c in characters)
        {
            saveList.Add(new CharacterSaveData()
            {
                name = c._nameofCharacter,
                isLock = c.IsLock
            });
        }

        string json = JsonUtility.ToJson(new Wrapper<CharacterSaveData>(saveList));
        PlayerPrefs.SetString("Characters", json);

        // Save selected character index
        int selectedIndex = characters.IndexOf(_characterSelected);
        PlayerPrefs.SetInt("SelectedCharacterIndex", selectedIndex);

        PlayerPrefs.Save();

        Debug.Log("Character Saved!");
    }

    public void LoadCharacters()
    {
        if (!PlayerPrefs.HasKey("Characters"))
        {
            Debug.Log("No save found — using default character list.");
            return;
        }

        string json = PlayerPrefs.GetString("Characters");
        var wrapper = JsonUtility.FromJson<Wrapper<CharacterSaveData>>(json);

        foreach (var saved in wrapper.items)
        {
            int index = characters.FindIndex(c => c._nameofCharacter == saved.name);
            if (index >= 0)
            {
                Character temp = characters[index];
                temp.IsLock = saved.isLock;
                characters[index] = temp;
            }
        }

        int selectedIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", 0);
        selectedIndex = Mathf.Clamp(selectedIndex, 0, characters.Count - 1);

        _characterSelected = characters[selectedIndex];

        Debug.Log("Character Loaded!");
    }

}
