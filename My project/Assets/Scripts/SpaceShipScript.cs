using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipScript : MonoBehaviour, ITouchable
{
    Vector3 acceleration, velocity;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //  acceleration = 9.8f * Vector3.down;                 // F = ma

        if (Input.touchCount > 0)
        {
            //  acceleration += 15 * Vector3.up;
        }

        velocity += acceleration * Time.deltaTime;           // v = u + at;
        transform.position += velocity * Time.deltaTime;   // s = ut
    }

    void changeColor(Color newColor)
    {
        GetComponent<Renderer>().material.color = newColor;
    }

    public void OnTap()
    {
        changeColor(Color.green);
        //throw new NotImplementedException("Spaceship tapped here");
    }

    public void OnDrag(Ray collidingRay)
    {
        
        transform.position = collidingRay.GetPoint(distance);

    }

    public void selected()
    {
        distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        changeColor(Color.green);
        //throw new NotImplementedException("Spaceship selected");
    }

    public void unselected()
    {
        distance = 0f;
        changeColor(Color.white);
        //throw new NotImplementedException("Spaceship unselected");
    }
}
