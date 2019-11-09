// John Knoop - 300835103

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SceneSettings", menuName = "Scene/Settings")]

[System.Serializable]
public class SceneSettings : ScriptableObject


{
    [Header("Scene Config")]
    public Scene scene;
    public SoundClip activeSoundClip;
    [Header("Scoreboard")]
    public bool scoreLabel;
    public bool livesLabel;
    public bool highScoreLabel;
    [Header("Scene Labels")]
    public bool startLabel;
    public bool endLabel;
    [Header("Scene Buttons")]
    public bool startButton;
    public bool restartButton;


}
