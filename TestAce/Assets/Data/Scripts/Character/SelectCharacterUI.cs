using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class SelectCharacterUI : MonoBehaviour
{
    [SerializeField] private Image _characterSelected;
    [SerializeField] private TMP_Text _nameOfCharacter;
    [SerializeField] private Button _buttonPrice;
    [SerializeField] private TMP_Text _priceText;
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
        var character = CharacterManager.Instance.GetCharacterSelect();
        _characterSelected.sprite = character._previewCharacter;
        _nameOfCharacter.text = character._nameofCharacter;
        if (character.IsLock)
        {
            _buttonPrice.gameObject.SetActive(true);
            _priceText.text = character.price.ToString();
        }
        else _buttonPrice.gameObject.SetActive(false);

    }
    public void OnClickBack()
    {
        MusicManager.Instance.PlayMusic("tap");

        CharacterManager.Instance.ChangeSelectedCharacter(-1);
    }
    public void OnClickNext()
    {
        MusicManager.Instance.PlayMusic("tap");

        CharacterManager.Instance.ChangeSelectedCharacter(1);
    }
    public void OnBuyCharacter()
    {
        MusicManager.Instance.PlayMusic("tap");

        var character = CharacterManager.Instance.GetCharacterSelect();
        if (GoldManager.Instance.GetCurrentGold() < character.price)
            return;
        GoldManager.Instance.ChangeCurrentGold(-character.price);
        CharacterManager.Instance.UnlockSelectedCharacter();
        CharacterManager.Instance.SaveCharacters();
    }

}

