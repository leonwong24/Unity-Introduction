using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiloScript : MonoBehaviour, ITouchable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(Ray collidingRay)
    {
        throw new System.NotImplementedException();
    }

    public void OnTap()
    {
        throw new System.NotImplementedException("Silo tapped code here");
    }

    public void selected()
    {
        changeColor(Color.green);
        //throw new System.NotImplementedException("Silo selected");
    }

    public void unselected()
    {
        changeColor(Color.white);
        //throw new System.NotImplementedException("Silo unselected");
    }

    private void changeColor(Color newColor)
    {
        GetComponent<Renderer>().material.color = newColor;
    }
}
