using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text ScoreUI;
    private int _score = 0;


    void Start()
    {
        SubscribeToAllItems();
    }
    

    void ScoreItem(Item _item)
    {
        _score += _item.GetScoreValue();
        UpdateScoreUI(_score);
    }

    void UpdateScoreUI(int _score)
    {
        ScoreUI.text = "Score: " + _score.ToString();
    }

    void SubscribeToAllItems()
    {
        var _list = FindObjectsOfType<LocalItem>();
        foreach(LocalItem _local in _list)
        {
            _local.ReturnedItem += ScoreItem;
        }
    }
}
