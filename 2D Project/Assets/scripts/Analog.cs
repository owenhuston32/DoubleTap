using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Analog : MonoBehaviour
{

    public Buttons[] buttonsScripts;
    private RaycastHit2D hit;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    Vector3 ray = Camera.main.ScreenToWorldPoint(touch.position);
                    hit = Physics2D.Raycast(Camera.main.transform.position, ray, 100,1);
                    if (hit.collider != null)
                    {
                            hit.collider.gameObject.GetComponent<Buttons>().began(hit.collider.name, touch.fingerId);
                    }
                }
              else if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    Vector3 ray = Camera.main.ScreenToWorldPoint(touch.position);
                    hit = Physics2D.Raycast(Camera.main.transform.position, ray, 100,1);
                    for (int i = 0; i < buttonsScripts.Length; i++)
                    {
                        buttonsScripts[i].move(ray, touch.fingerId);
                    }
                }
                else
                {
                    for (int i = 0; i < buttonsScripts.Length; i++)
                    {
                        buttonsScripts[i].end(touch.fingerId);
                    }
                }
            }
        }
    }
}