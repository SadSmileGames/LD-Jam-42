using System.Collections;
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
        EnemyHealth.OnDeath += OnKill;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDamage -= Damaged;
        EnemyHealth.OnDeath -= OnKill;
    }

    void Start()
    { 
        killCountText = textBoxes[0].GetComponent<Text>();
        //fader = blackBox.GetComponent<BlackFader>();
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

    public void gameOver()
    {
        disableText();
        //fader.fadeIn();
        scoreText.text = ("Score: " + killCount);
        textBoxes[3].SetActive(true);
        textBoxes[2].SetActive(true);
    }

    //public void resetGame()
    //{
    //    disableText();
    //    yield return new WaitForSeconds(1);
    //    textBoxes[1].SetActive(true);
    //}

    public void startGame()
    {
        disableText();
        textBoxes[0].SetActive(true);
        //fader.fadeOut();
    }

}
