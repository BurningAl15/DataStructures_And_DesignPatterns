using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private int score = 0;

    void Start()
    {
        EventManager.AddListener_AddPoints(AddScore);
        scoreText.text = "Score: " + score;

    }

    public void AddScore(int _score)
    {
        score += _score;
        scoreText.text = "Score: " + score;
    }
}
