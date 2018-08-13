﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehavior : MonoBehaviour {

    public GameObject[] textBoxes;
    public GameObject[] health;
    public GameObject blackBox;

    private Text killCountText;
    private Text scoreText;
    private int killCount;
    //private BlackFader fader;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDamage += Damaged;
        PlayerHealth.OnPlayerDeath += GameOver;
        EnemyHealth.OnDeath += OnKill;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDamage -= Damaged;
        PlayerHealth.OnPlayerDeath -= GameOver;
        EnemyHealth.OnDeath -= OnKill;
    }

    void Start()
    { 
        killCountText = textBoxes[0].GetComponent<Text>();
        scoreText = textBoxes[3].GetComponent<Text>();
    }

    private void disableText()
    {
        foreach(GameObject box in textBoxes)
        {
            box.SetActive(false);
        }
    }

    public void OnKill()
    {
        killCount++;
        killCountText.text = ("Kills: " + killCount);
        scoreText.text = ("KILLS: " + killCount);
    }

    public void Damaged()
    {
        if (health[4].activeSelf)
        {
            health[4].SetActive(false);
        }
        else if (health[3].activeSelf)
        {
            health[3].SetActive(false);
        }
        else if (health[2].activeSelf)
        {
            health[2].SetActive(false);
        }
        else if (health[1].activeSelf)
        {
            health[1].SetActive(false);
        }
        else if (health[0].activeSelf)
        {
            health[0].SetActive(false);
        }
    }

    public void GameOver()
    {
        disableText();
        textBoxes[0].SetActive(false);
        textBoxes[3].SetActive(true);
        textBoxes[2].SetActive(true);
        textBoxes[4].SetActive(true);
    }

    public void startGame()
    {
        disableText();
        textBoxes[0].SetActive(true);
    }

}
