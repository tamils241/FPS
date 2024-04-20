using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour

{
    //handle to Text
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _livesSprites;
    // Start is called before the first frame update
    void Start()
    {
        
        _scoreText.text = "Score :" + 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateScore(int playerScore)
    {
        _scoreText.text = "Score :" + playerScore.ToString();
    }
    public void updateLives(int currentLives)
    {
        //access display images sprite
        // give new one based on current Index
        _livesImage.sprite = _livesSprites[currentLives];
    }
}
