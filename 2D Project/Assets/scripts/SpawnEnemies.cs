using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {


    public Transform[] transforms;
    public GameObject[] blueEnemies0;
    public GameObject[] blueEnemies1;
    public GameObject[] blueEnemies2;
    public GameObject[] orangeEnemies0;
    public GameObject[] orangeEnemies1;
    public GameObject[] orangeEnemies2;
    private float enemySpeed = -5;
    public bool playing = false;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator Spawn()
    {
        while(playing)
        {
            int random = Random.Range(0, 6);

            if(random == 0)
            {
                enemySpawn(blueEnemies0);
            }
            else if(random ==1)
            {
                enemySpawn(blueEnemies1);
            }
            else if(random == 2)
            {
                enemySpawn(blueEnemies2);
            }
            else if(random == 3)
            {
                enemySpawn(orangeEnemies0);
            }
            else if(random == 4)
            {
                enemySpawn(orangeEnemies1);
            }
            else if(random == 5)
            {
                enemySpawn(orangeEnemies2);
            }


            if (enemySpeed > -6)
            {
                enemySpeed -= .05f;
            }
            yield return new WaitForSeconds(1);
        }
    }
    public void despawn()
    {
        for (int i = 0; i < blueEnemies0.Length; i++)
        {
            blueEnemies0[i].SetActive(false);
        }
        for(int i = 0; i < blueEnemies1.Length; i++)
        {
            blueEnemies1[i].SetActive(false);
        }
        for (int i = 0; i < blueEnemies2.Length; i++)
        {
            blueEnemies2[i].SetActive(false);
        }
        for (int i = 0; i < orangeEnemies0.Length; i++)
        {
            orangeEnemies0[i].SetActive(false);
        }
        for (int i = 0; i < orangeEnemies1.Length; i++)
        {
            orangeEnemies1[i].SetActive(false);
        }
        for (int i = 0; i < orangeEnemies2.Length; i++)
        {
            orangeEnemies2[i].SetActive(false);
        }
    }
    public void enemySpawn(GameObject[] enemies)
    {
        int randomPosition = Random.Range(0, 8);
        float randomX = Random.Range(-5, 5);
        int count = 0;
        bool found = false;
            while (count < enemies.Length && !found)
            {
                if (!enemies[count].activeSelf)
                {
                    enemies[count].SetActive(true);
                    enemies[count].GetComponent<Enemy>().spawn();
                    enemies[count].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    enemies[count].transform.position = transforms[randomPosition].position;
                    enemies[count].GetComponent<Rigidbody2D>().AddForce(new Vector2(randomX, enemySpeed), ForceMode2D.Impulse);
                    
                    if(enemies[count].GetComponent<Shoot>() != null)
                    {
                        enemies[count].GetComponent<Shoot>().startShooting();
                    }

                    found = true;
                }
                count++;
            }
    }
}
