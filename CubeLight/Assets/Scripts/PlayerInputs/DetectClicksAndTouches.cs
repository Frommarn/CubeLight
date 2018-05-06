using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClicksAndTouches : MonoBehaviour {

    public Camera detectionCamera;

    // This variable adds a Debug.Log call to show what was touched
    public bool debug = false;

    // This is the actual camera we reference in the update loop, set in Start()
    private Camera _camera;

	// Use this for initialization
	void Start ()
	{
        if (detectionCamera != null)
        {
            _camera = detectionCamera;
        }
        else
        {
            _camera = Camera.main;
        }
	}
	
	// Update is called once per frame
	void Update ()
	{
        Ray ray;
        RaycastHit hit;

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    ray = _camera.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        if (debug)
                        {
                            Debug.Log("You touched " + hit.collider.gameObject.name, hit.collider.gameObject);
                        }
                        hit.transform.gameObject.SendMessage("Clicked", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
        else // We are on a computer (more than likely)
        {
            if (Input.GetMouseButtonDown(0)) // Check to see if we've clicked
            {
                // Set up our ray from screen to scene
                ray = _camera.ScreenPointToRay(Input.mousePosition);

                // If we hit...
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    // Tell the system what we clicked something if in debug
                    if (debug)
                    {
                        Debug.Log("You clicked " + hit.collider.gameObject.name, hit.collider.gameObject);
                    }

                    // Run the Clicked() function on the clicked object
                    hit.transform.gameObject.SendMessage("Clicked", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }

            if (Input.GetMouseButtonDown(1)) // Check to see if we've clicked
            {
                // Set up our ray from screen to scene
                ray = _camera.ScreenPointToRay(Input.mousePosition);

                // If we hit...
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    // Tell the system what we clicked something if in debug
                    if (debug)
                    {
                        Debug.Log("You right clicked " + hit.collider.gameObject.name, hit.collider.gameObject);
                    }

                    // Run the Clicked() function on the clicked object
                    hit.transform.gameObject.SendMessage("RightClicked", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
	}
}
