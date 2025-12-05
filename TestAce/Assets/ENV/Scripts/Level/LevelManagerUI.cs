using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public struct StarOflevel
{
    public Image _star1;
    public Image _star2;
    public Image _star3;
}
public class LevelManagerUI : MonoBehaviour
{
    [SerializeField] private Transform _listContainer;
    [SerializeField] private List<Button> _listButtonMap = new List<Button>();
    [SerializeField] private List<StarOflevel> _listStarOfLevels = new List<StarOflevel>();

    private int totalMapUnlock;

    private void Start()
    {
        if (_listContainer == null)
        {
            Debug.LogWarning("List Container is missing");
            return;
        }

        _listButtonMap = new List<Button>(_listContainer.GetComponentsInChildren<Button>());
        

        totalMapUnlock = LevelController.Instance.GetTotalMapUnlocked();

        UpdateButtons();

        AddButtonListeners();
    }

    private void UpdateButtons()
    {
        for (int i = 0; i < _listButtonMap.Count; i++)
        {
            Image[] images =_listButtonMap[i].GetComponentsInChildren<Image>();
            _listStarOfLevels.Add(new StarOflevel
            {
                _star1 = images[1],
                _star2 = images[2],
                _star3 = images[3]
            });
            if (i <= totalMapUnlock)
            {
                _listButtonMap[i].interactable = true;

                var colors = _listButtonMap[i].colors;
                colors.normalColor = Color.yellow;
                _listButtonMap[i].colors = colors;
                

            }
            else
            {
                _listButtonMap[i].interactable = false;
                //_listStarOfLevels[i]._star1.color = Color.gray;
                //_listStarOfLevels[i]._star2.color = Color.gray;
                //_listStarOfLevels[i]._star3.color = Color.gray;
            }
        }
    }

    private void AddButtonListeners()
    {
        foreach (Button btn in _listButtonMap)
        {
            Button localBtn = btn; 

            btn.onClick.AddListener(() =>
            {
                string levelStr = localBtn.GetComponentInChildren<TMP_Text>().text;
                int level = int.Parse(levelStr);
                LevelController.Instance.Loadlevel(level);
            });
        }
    }

    public void RefreshUI()
    {
        Debug.Log("Refresh UI Level Manager");

        totalMapUnlock = LevelController.Instance.GetTotalMapUnlocked();
        UpdateButtons();
    }
}
