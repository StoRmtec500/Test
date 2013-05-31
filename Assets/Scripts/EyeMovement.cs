using UnityEngine;
using System.Collections;

public class EyeMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        ////////////////////////
        // keyboard scrolling //
        ////////////////////////

        float translationX = Input.GetAxis("Horizontal");
        float translationY = Input.GetAxis("Vertical");
        float fastTranslationX = 2 * Input.GetAxis("Horizontal");
        float fastTranslationY = 2 * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(fastTranslationX + fastTranslationY, 0, fastTranslationY - fastTranslationX);
        }
        else
        {
            transform.Translate(translationX + translationY, 0, translationY - translationX);
        }


        ////////////////////////
        //  mouse scrolling   //
        ////////////////////////

        float mousePosX = Input.mousePosition.x;
        float mousePosY = Input.mousePosition.y;
        int scrollDistance = 5;

        //Horizontal Camera Movement
        //horizontal, left
        if (mousePosX < scrollDistance)
        {
            if(transform.position.x > 40)
            {
            transform.Translate(-1, 0, 1);
            }
        }

        //horizontal, right
        if (mousePosX >= Screen.width - scrollDistance)
        {
            if (transform.position.x < 1024)
            {
                transform.Translate(1, 0, -1);
            }
        }

        // Vertical Camera Movement //
        //vertical, down
        if (mousePosY < scrollDistance)
        {
            if (transform.position.z > 60)
            {
                transform.Translate(-1, 0, -1);
            }
        }

        //vertical, up
        if (mousePosY >= Screen.height - scrollDistance)
        {
            if (transform.position.z < 1024)
            {
                transform.Translate(1, 0, 1);
            }
        }


        ////////////////////////
        //   mouse zooming    //
        ////////////////////////

        GameObject Eye = GameObject.Find("Eye");

        // zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Eye.camera.orthographicSize > 4)
        {
            Eye.camera.orthographicSize = Eye.camera.orthographicSize - 4;
        }

        // zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Eye.camera.orthographicSize < 80)
        {
            Eye.camera.orthographicSize = Eye.camera.orthographicSize + 4;
        }

        // zoom default
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            Eye.camera.orthographicSize = 50;
        }
	}

}
