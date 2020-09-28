using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    // Variables
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _gameOverTxt;
    [SerializeField] private Text _loadLevelTxt;
    [SerializeField] private Text _ammoCount;

    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private Image _livesImage;
    
    private bool _gameOverTrue = false;

    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if(_gameManager == null)
        {
            Debug.LogError("The Game Manager is NULL");
        }
        
        _scoreText.text = "Score: " + 0;
        _gameOverTxt.gameObject.SetActive(false);
        _loadLevelTxt.gameObject.SetActive(false);
    }

    
    public void UpdateScore(int playerscore)
    {
        _scoreText.text = "Score: " + playerscore.ToString();
    }

    public void UpdateAmmo(int ammocount)
    {
        if(ammocount < 0)
        {
            ammocount = 0;
        }

        _ammoCount.text = "Ammo: " + ammocount.ToString();
    }

    public void UpdateLives(int currentlives)
    {
        _livesImage.sprite = _liveSprites[currentlives];

        if(currentlives <= 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        _gameManager.IsGameOverTrue();
        _gameOverTrue = true;
        _loadLevelTxt.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
    }

    IEnumerator GameOverFlicker()
    {
       while(_gameOverTrue == true)
        {
            _gameOverTxt.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverTxt.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
