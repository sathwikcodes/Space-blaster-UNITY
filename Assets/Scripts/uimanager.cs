using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uimanager : MonoBehaviour
{

    [SerializeField]
    private Text _scoretext;
    [SerializeField]
    private Image _liveImage;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _Restarttext;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
       _scoretext.text = "Score : " + 0;
       _gameOverText.gameObject.SetActive(false);
       _Restarttext.gameObject.SetActive(false);
       _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

       if(_gameManager==null)
       {
        Debug.LogError("Game Manager is not Connected");
       }
    }

    public void UpdateScore(int playerscore){
        _scoretext.text = "Score : " + playerscore.ToString();
    }

    public void UpdateLives(int Currentlives){
        _liveImage.sprite = _liveSprites[Currentlives];
        if(Currentlives == 0)
        {
            GAmeOverSequence();

        }
    }

    void GAmeOverSequence(){
        
            _gameManager.GameOver();
            _gameOverText.gameObject.SetActive(true);
            _Restarttext.gameObject.SetActive(true);
            StartCoroutine(GAmeOverFlicker()); 
    }

    IEnumerator GAmeOverFlicker()
    {
        while(true){
        _gameOverText.text = "GAME OVER";
        yield return new WaitForSeconds(0.5f);
        _gameOverText.text = "";
        yield return new WaitForSeconds(0.5f);
    }
    }
}
