using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacterUI : MonoBehaviour
{
    [SerializeField] private Image _characterSelected;
    private void OnEnable()
    {
        CharacterManager.OnCharacterSelected += UpdateCharacterSelected;
    }
    private void OnDisable()
    {
        CharacterManager.OnCharacterSelected -= UpdateCharacterSelected;
    }
    private void UpdateCharacterSelected()
    {
        var character = CharacterManager.Instance.GetCharacterSelected();
        _characterSelected.sprite = character._previewCharacter;
    }
    public void OnClickBack() {
        CharacterManager.Instance.ChangeSelectedCharacter(-1);
    }
    public void OnClickNext()
    {
        CharacterManager.Instance.ChangeSelectedCharacter(1);
    }

}

