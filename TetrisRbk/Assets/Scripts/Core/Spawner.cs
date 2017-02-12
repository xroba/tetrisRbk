using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Spawner : MonoBehaviour {

    public Shape[] m_allShapes;
    Board m_board;
    List<Shape> allNextShape;

    public GameObject queueSpace1;
    public GameObject queueSpace2;
    public GameObject queueSpace3;


    // Use this for initialization
    void Start () {
        this.transform.position =  VectorF.RoundVector(this.transform.position);
        allNextShape = new List<Shape>();


    }

    void Awake()
    {
        m_board = GameObject.FindObjectOfType<Board>();
        allNextShape = new List<Shape>();
        GenerateNextShape();
    }

    // Update is called once per frame
    void Update () {
	   
	}

    public Shape SpawnShape()
    {

        Shape shape = null;
        shape = Instantiate(GetNextShape(), transform.position, Quaternion.identity) as Shape;

        return shape;

    }

    void GenerateNextShape()
    {
        while (allNextShape.Count < 3)
        {
            allNextShape.Add(GetRandomShape());

        }
        //fill it on the 3 xform
        Instantiate(allNextShape[0], queueSpace1.transform.position, Quaternion.identity);
        Instantiate(allNextShape[1], queueSpace2.transform.position, Quaternion.identity);
        Instantiate(allNextShape[2], queueSpace3.transform.position, Quaternion.identity);
    }

    Shape GetNextShape()
    {
        Shape nextShape = allNextShape.First();

        allNextShape.RemoveAt(0);
        GenerateNextShape();

        return nextShape;
    }

    private Shape GetRandomShape()
    {
        int index = Random.Range(0, m_allShapes.Length);
        Shape shape = m_allShapes[index];
        return shape;
    }

}
