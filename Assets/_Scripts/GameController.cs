// John Knoop - 300835103

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
using System;


public class GameController : MonoBehaviour
{
    [Header("Scene Game Objects")]
    public GameObject cloud;
    public GameObject island;
    public int numberOfClouds;
    public List<GameObject> clouds;

    [Header("Audio Sources")]
    public SoundClip activeSoundClip;
    public AudioSource[] audioSources;

    [Header("Scoreboard")]
    [SerializeField]
    private int _lives;

    [SerializeField]
    private int _score;

    public Text livesLabel;
    public Text scoreLabel;
    public Text highScoreLabel;

    //public GameObject scoreBoard;

    //public HighScoreSO highScoreSO;

    [Header("UI Control")]
    public GameObject startLabel;
    public GameObject startButton;
    public GameObject endLabel;
    public GameObject restartButton;


    [Header("Game Settings")]
    public ScoreBoard scoreBoard;

    [Header("Scene Settings")]
    public SceneSettings activeSceneSettings;
    public List<SceneSettings> sceneSettings;

    // public properties
    public int Lives
    {
        get
        {
            return _lives;
        }

        set
        {
            _lives = value;
            scoreBoard.lives = _lives;
            if (_lives < 1)
            {

                SceneManager.LoadScene("End");
            }
            else
            {
                livesLabel.text = "Lives: " + _lives.ToString();
            }

        }
    }

    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
            scoreBoard.score = _score;


            if (scoreBoard.highScore < _score)
            //if (highScoreSO.score < _score)
            {
                scoreBoard.highScore = _score;
                //highScoreSO.score = _score;
            }
            scoreLabel.text = "Score: " + _score;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObjectInitialization();
        SceneConfiguration();
    }

    private void GameObjectInitialization()
    {

        startLabel = GameObject.Find("StartLabel");
        endLabel = GameObject.Find("EndLabel");
        startButton = GameObject.Find("StartButton");
        restartButton = GameObject.Find("RestartButton");
        //scoreBoard = Resources.FindObjectsOfTypeAll<ScoreBoard>()[0] as ScoreBoard;
        //highScoreSO = Resources.FindObjectsOfTypeAll<HighScoreSO>()[0] as HighScoreSO;
    }


    private void SceneConfiguration()
    {
        var query = from settings in sceneSettings
                    where settings.scene == (Scene)Enum.Parse(typeof(Scene), SceneManager.GetActiveScene().name.ToUpper())

                    select settings;

        activeSceneSettings = query.ToList().First();

        {
            if (activeSceneSettings.scene == Scene.MAIN)
            {
                Lives = 5;
                Score = 0;
            }
            scoreLabel.enabled = activeSceneSettings.scoreLabel;
            livesLabel.enabled = activeSceneSettings.livesLabel;
            highScoreLabel.enabled = activeSceneSettings.highScoreLabel;
            endLabel.SetActive(activeSceneSettings.endLabel);
            restartButton.SetActive(activeSceneSettings.restartButton);
            activeSoundClip = activeSceneSettings.activeSoundClip;
            startButton.SetActive(activeSceneSettings.startButton);
            startLabel.SetActive(activeSceneSettings.startLabel);
            highScoreLabel.text = "High Score: " + scoreBoard.highScore;
            livesLabel.text = "Lives: " + scoreBoard.lives;
            scoreLabel.text = "Score: " + scoreBoard.score;

        }



        if ((activeSoundClip != SoundClip.NONE) && (activeSoundClip != SoundClip.NUM_OF_CLIPS))
        {
            AudioSource activeAudioSource = audioSources[(int)activeSoundClip];
            activeAudioSource.playOnAwake = true;
            activeAudioSource.loop = true;
            activeAudioSource.volume = 0.5f;
            activeAudioSource.Play();
        }



        // creates an empty container (list) of type GameObject
        clouds = new List<GameObject>();

        for (int cloudNum = 0; cloudNum < numberOfClouds; cloudNum++)
        {
            clouds.Add(Instantiate(cloud));
        }

        Instantiate(island);
    }


    // Event Handlers
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }
}
