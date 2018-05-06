using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour {

    public float _MinZoom = 0.0f;
    public float _MaxZoom = 10.0f;
    public float _ZoomSpeed = 1.0f;
    public float _CameraMoveSpeed = 10.0f;

    // Use this for initialization
    void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.anyKey)
        {
            MovePlayerCameraWith_WASD_or_Arrow_Keys();
        }
        CameraZoom();
    }

    private void CameraZoom()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        if (zoom == 0.0f) { return; }   // Exit early

        var y = transform.position.y;
        if (_MinZoom <= y && y <= _MaxZoom)
        {
            zoom = GetClampedZoomValue(zoom, y);
            Debug.Log("Zoom: " + zoom);
            transform.position = new Vector3(transform.position.x, y + zoom, transform.position.z - zoom);
        }
    }

    private float GetClampedZoomValue(float zoom, float y)
    {
        if (y + zoom < _MinZoom)      { return zoom - (y + zoom - _MinZoom); }
        else if (y + zoom > _MaxZoom) { return zoom - (y + zoom - _MaxZoom); }
        else                          { return zoom; }
    }

    private void MovePlayerCameraWith_WASD_or_Arrow_Keys()
    {
        float horizontalMovement = 0;
        float verticalMovement = 0;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalMovement += -_CameraMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontalMovement += _CameraMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            verticalMovement += _CameraMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            verticalMovement += -_CameraMoveSpeed * Time.deltaTime;
        }
        transform.position += new Vector3(horizontalMovement, 0.0f, verticalMovement);
    }
}
