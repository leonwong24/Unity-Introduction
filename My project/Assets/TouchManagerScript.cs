using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManagerScript : MonoBehaviour
{
    float timer = 0f;
    private bool hasMoved;
    Ray screenPointToRay;


    private ITouchable selectedObject = null;
    private ITouchable prevSelectedObject = null;

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

                    /*
                     * Begin Drag movement
                     */
                    if(selectedObject != null)
                    {
                        screenPointToRay = Camera.main.ScreenPointToRay(touchPosition);
                        selectedObject.OnDrag(screenPointToRay);
                    }

                    /*
                     * End of drag movement
                     */
                    print("finger moved");
                    break;

                case TouchPhase.Stationary:
                    break;

                case TouchPhase.Ended:
                    print(timer);
                    if ((timer <= 0.3f) && !hasMoved){   //tapped
                        print("tapped");
                        screenPointToRay = Camera.main.ScreenPointToRay(touchPosition);

                        Debug.DrawRay(screenPointToRay.origin, 45 * screenPointToRay.direction, Color.red);
                        RaycastHit hitInfo;

                        if (Physics.Raycast(screenPointToRay, out hitInfo)){
                            //print("hit something!");
                            ITouchable touchedObject = hitInfo.transform.GetComponent<ITouchable>();

                            if (touchedObject != null){
                                //touchedObject.OnTap();
                                if (touchedObject == selectedObject){   //unselect the same object
                                    prevSelectedObject = selectedObject;
                                    selectedObject = null;
                                }

                                else{  // select other object
                                    prevSelectedObject = selectedObject;
                                    selectedObject = touchedObject;
                                    touchedObject.selected();
                                }
                            }
                        }
                        else{
                            prevSelectedObject = selectedObject;
                            selectedObject = null;
                            print("no object is selected");
                        }
                        if (prevSelectedObject != selectedObject && prevSelectedObject != null)
                            prevSelectedObject.unselected();
                    }

                    else{   // hold & release
                        print("too long");
                        
                    }
                    break;
            }
        }
    }
}
