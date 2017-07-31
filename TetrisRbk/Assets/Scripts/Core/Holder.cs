using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour {

    private float scale = 0.4f;
    public Transform xFormHolder;
    public Shape holdShape;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool HasShape() {
        return holdShape != null ? true : false;
    }

    public void PutShapeOnHold(Shape shape)
    {
        holdShape = shape;
        holdShape.transform.position = xFormHolder.transform.position;
        holdShape.transform.localScale = new Vector3(scale,scale,scale);

    }

    public Shape RetrieveHoldShape()
    {
        return holdShape;
    }


}
