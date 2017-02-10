using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public Shape[] m_allShapes;
	// Use this for initialization
	void Start () {
        this.transform.position =  VectorF.RoundVector(this.transform.position);

	}
	
	// Update is called once per frame
	void Update () {
	   
	}

    public Shape SpawnShape()
    {

        Shape shape = null;
        shape = Instantiate(GetRandomShape(), transform.position, Quaternion.identity) as Shape;

        //create a ghost
       


        return shape;

    }

    private Shape GetRandomShape()
    {
        int index = Random.Range(0, m_allShapes.Length);
        Shape shape = m_allShapes[index];
        return shape;
    }


}
