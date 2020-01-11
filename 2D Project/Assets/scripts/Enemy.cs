using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private int health;
    private ParticleSystem[] blueEnemyParticle = new ParticleSystem[4];
    private ParticleSystem[] orangeEnemyParticle = new ParticleSystem[4];

    // Use this for initialization
    void Start () {

        blueEnemyParticle[0] = GameObject.Find("blue enemy particle0").GetComponent<ParticleSystem>();
        blueEnemyParticle[1] = GameObject.Find("blue enemy particle1").GetComponent<ParticleSystem>();
        blueEnemyParticle[2] = GameObject.Find("blue enemy particle2").GetComponent<ParticleSystem>();
        blueEnemyParticle[3] = GameObject.Find("blue enemy particle3").GetComponent<ParticleSystem>();

        orangeEnemyParticle[0] = GameObject.Find("orange enemy particle0").GetComponent<ParticleSystem>();
        orangeEnemyParticle[1] = GameObject.Find("orange enemy particle1").GetComponent<ParticleSystem>();
        orangeEnemyParticle[2] = GameObject.Find("orange enemy particle2").GetComponent<ParticleSystem>();
        orangeEnemyParticle[3] = GameObject.Find("orange enemy particle3").GetComponent<ParticleSystem>();

        spawn();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void takeDamage(int damage)
    {
        health-=damage;
        if(health <=0)
        {
            if (gameObject.CompareTag("blue enemy"))
            {
                int count = 0;
                bool found = false;
                while (count < blueEnemyParticle.Length && !found)
                {
                    if (!blueEnemyParticle[count].isPlaying)
                    {
                        blueEnemyParticle[count].transform.position = gameObject.transform.position;
                        blueEnemyParticle[count].Play();
                        found = true;
                    }
                    count++;
                }
            }
            if (gameObject.CompareTag("orange enemy"))
            {
                int count = 0;
                bool found = false;
                while (count < orangeEnemyParticle.Length && !found)
                {
                    if (!orangeEnemyParticle[count].isPlaying)
                    {
                        orangeEnemyParticle[count].transform.position = gameObject.transform.position;
                        orangeEnemyParticle[count].Play();
                        found = true;
                    }
                    count++;
                }
            }
            if (gameObject.name.Equals("blue shooter") || gameObject.name.Equals("orange shooter"))
            {
                gameObject.GetComponent<Shoot>().StopAllCoroutines();
            }
            gameObject.SetActive(false);
        }
    }
    public void spawn()
    {
        if (gameObject.name.Equals("blue blade") || gameObject.name.Equals("orange blade"))
        {
            health = 5;
        }
        if (gameObject.name.Equals("blue spike") || gameObject.name.Equals("orange spike"))
        {
            health = 3;
        }
        if (gameObject.name.Equals("blue shooter") || gameObject.name.Equals("orange shooter"))
        {
            health = 2;
        }
    }
}
