using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject[] bullets;
    private int bulletSpeed;
    private float reloadSpeed;
    private int bulletint = 0;
    public bool shooting = true;
    public Transform bulletPosition;


    // Use this for initialization
    void Start()
    {
        //StartCoroutine("shoot");
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator shoot()
    {
        while (true)
        {
            if (shooting)
            {
                if (bulletint == bullets.Length)
                {
                    bulletint = 0;
                }
                bullets[bulletint].SetActive(true);
                bullets[bulletint].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                bullets[bulletint].transform.position = bulletPosition.position;
                bullets[bulletint].transform.up = gameObject.transform.up;
                bullets[bulletint].GetComponent<Rigidbody2D>().AddForce(bullets[bulletint].transform.up * bulletSpeed, ForceMode2D.Impulse);
                bulletint++;
                yield return new WaitForSeconds(reloadSpeed);
            }
            yield return new WaitForEndOfFrame();
        }
    }
    public void startShooting()
    {
        if (gameObject.name.Equals("blue player") || gameObject.name.Equals("orange player"))
        {
            bulletSpeed = 40;
            reloadSpeed = .2f;
        }
        if (gameObject.CompareTag("blue enemy") || gameObject.CompareTag("orange enemy"))
        {
            bulletSpeed = 25;
            reloadSpeed = 2;
        }
        StartCoroutine("shoot");
    }
}