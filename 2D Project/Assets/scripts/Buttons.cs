using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    private int velocity;
    public GameObject analog;
    public GameObject fakeanalog;
    private int finger;
    public GameObject player;
    private bool drag = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!drag)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    public void began(string name, int fingerId)
    {
        if (name == gameObject.name)
        {
            finger = fingerId;
            drag = true;
        }
    }
    public void move(Vector3 ray, int fingerId)
    {
        if (finger == fingerId)
        {
            if (gameObject.name == "big analog left" || gameObject.name == "big analog right")
            {
                if (drag)
                {
                    var allowedPos = ray - analog.transform.position;
                    allowedPos = Vector3.ClampMagnitude(allowedPos, 2f);
                    analog.transform.position = fakeanalog.transform.position + allowedPos * 2;
                    player.GetComponent<Rigidbody2D>().MovePosition(new Vector2(player.transform.position.x + (allowedPos.x * 15 * Time.deltaTime), player.transform.position.y));
                }
            }
        }

    }
    public void end(int fingerId)
    {
        if (finger == fingerId)
        {
            if (gameObject.name == "big analog left" || gameObject.name == "big analog right")
            {
                if (drag)
                {
                    analog.transform.position = fakeanalog.transform.position;
                    analog.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            drag = false;
        }
    }
}