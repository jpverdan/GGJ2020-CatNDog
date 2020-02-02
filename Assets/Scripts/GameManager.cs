using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text ScoreUI;
    private int _score = 0;

    public RectTransform panel;

    int OneStar = 100;
    int TwoStars = 170;
    int ThreeStars = 230;
    [SerializeField] private float _delayFinalScore = 5f;

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

    public void TimeUp()
    {
        Time.timeScale = 0;
        PainelOsso();
    }

    void PainelOsso()
    {
        panel.gameObject.SetActive(true);
        var list = GameObject.FindGameObjectsWithTag("Osso");
        if (StarsFromScore() > 0){
            for(var i = 1; i <= StarsFromScore(); i++)
            {
                list[i].gameObject.SetActive(true);
            }

        }
        panel.Find("Text").GetComponent<Text>().text = "Score: " + _score.ToString();
    }

    int StarsFromScore()
    {
        if (_score < OneStar)
        {
            return 0;
        } else if (_score < TwoStars)
        {
            return 1;
        } else if (_score < ThreeStars)
        {
            return 2;
        } else
        {
            return 3;
        }
    }

    IEnumerator EndMapScoreUI()
    {
        print("Stars: " + StarsFromScore().ToString());
        yield return new WaitForSecondsRealtime(_delayFinalScore);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
