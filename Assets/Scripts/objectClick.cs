using UnityEngine;
using System.Collections;

public class objectClick : MonoBehaviour {

    public GameObject select;
    public Ray ray;
    public RaycastHit hit;

    // Use this for initialization
	void Start () {
        select = GameObject.FindGameObjectWithTag("haus");
       
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    void fixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, 100f))
            {
                select.tag = "none";
                hit.collider.transform.tag = "haus";
            }
        }
    }
}
