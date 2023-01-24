using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManagerScript : MonoBehaviour
{
    float timer = 0f;
    private bool hasMoved;

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
            //print(touchPosition);

            timer += Time.deltaTime;

            switch (myFirstTouch.phase)
            {
                case TouchPhase.Began:
                    timer = 0f;
                    hasMoved = false;
                    break;

                case TouchPhase.Moved:
                    hasMoved = true;
                    print("finger moved");
                    break;

                case TouchPhase.Stationary:
                    break;

                case TouchPhase.Ended:
                    print(timer);
                    if ( (timer <= 0.3f) && !hasMoved)   //tapped
                    {
                        print("tapped");
                        Ray screenPointToRay = Camera.main.ScreenPointToRay(touchPosition);

                        Debug.DrawRay(screenPointToRay.origin, 45 * screenPointToRay.direction, Color.red);
                        RaycastHit hitInfo;

                        if (Physics.Raycast(screenPointToRay, out hitInfo))
                        {
                            //print("hit something!");
                            ITouchable touchedObject = hitInfo.transform.GetComponent<ITouchable>();

                            if (touchedObject != null)
                                touchedObject.OnTap();

                        }
                        else
                        {
                            //print("miss hit");
                        }
                    }
                        
                    else
                        print("too long");
                    break;
            }
        }
    }
}
