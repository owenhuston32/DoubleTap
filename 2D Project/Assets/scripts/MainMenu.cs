using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject musicCrossOutImage;
    public AudioSource clickSound;
    public AudioSource music;
    public GameObject mainMenu;
    public GameObject input;
    public GameObject bluePlayer;
    public GameObject orangePlayer;
    public GameObject bluePosition;
    public GameObject orangePosition;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void playPress()
    {
        GameObject.Find("bottom bar").GetComponent<Collider2D>().enabled = true;
        Image bar = GameObject.Find("bottom bar").GetComponent<Image>();
        bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, 1);
        clickSound.Play();
        mainMenu.SetActive(false);
        bluePlayer.SetActive(true);
        bluePlayer.transform.position = bluePosition.transform.position;
        orangePlayer.SetActive(true);
        orangePlayer.transform.position = orangePosition.transform.position;
        bluePlayer.GetComponent<Shoot>().startShooting();
        orangePlayer.GetComponent<Shoot>().startShooting();
        input.GetComponent<SpawnEnemies>().playing = true;
        input.GetComponent<SpawnEnemies>().StartCoroutine("Spawn");
    }
    public void MusicPress()
    {
        clickSound.Play();
        if (music.isPlaying)
        {
            musicCrossOutImage.SetActive(false);
            music.Stop();
        }
        else
        {
            musicCrossOutImage.SetActive(true);
            music.Play();
        }
    }
}
