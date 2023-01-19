using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch[] touches = Input.touches;    // return list of objects representing status of all touches during last frame
            Touch myFirstTouch = touches[0];

            Vector2 touchPosition = myFirstTouch.position;   //relative to phone resolution
            print(touchPosition);

            Ray screenPointToRay = Camera.main.ScreenPointToRay(touchPosition);

            Debug.DrawRay(screenPointToRay.origin, 15 * screenPointToRay.direction, Color.red);
            RaycastHit hitInfo;

            if (Physics.Raycast(screenPointToRay, out hitInfo))
            {
                SpaceShipScript touchedSpaceScript = hitInfo.transform.GetComponent<SpaceShipScript>();

                touchedSpaceScript.changeColor(Color.green);
            }
            else
            {
                print("miss hit");
            }
        }
    }
}
