using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState { PLAY, PAUSE }

    public GameState gameState = GameState.PLAY;

    [SerializeField] private Text ScoreUI;
    private int _score = 0;

    public RectTransform panel;

    int OneStar = 100;
    int TwoStars = 170;
    int ThreeStars = 230;
    [SerializeField] private float _delayFinalScore = 5f;

    public Image[] listPanelOsso;
    public Text panelScore;

    void Start()
    {
        gameState = GameState.PLAY;
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
        gameState = GameState.PAUSE;
        Time.timeScale = 0;
        PainelOsso();
    }

    void PainelOsso()
    {
        panel.gameObject.SetActive(true);
        if (StarsFromScore() > 0){
            for(var i = 0; i <= StarsFromScore()-1; i++)
            {
                var cor = listPanelOsso[i].color;
                cor.a = 1;
                listPanelOsso[i].color = cor;
            }

        }
        panelScore.text = "Score: " + _score.ToString();
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
    public void RestartMap()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    IEnumerator EndMapScoreUI()
    {
        print("Stars: " + StarsFromScore().ToString());
        yield return new WaitForSecondsRealtime(_delayFinalScore);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
