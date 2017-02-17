using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Spawner : MonoBehaviour {

    public Shape[] m_allShapes;
    Board m_board;
    public List<Shape> m_AllNextShape;

    public GameObject[] queueSpace;





    void Awake()
    {
        m_board = GameObject.FindObjectOfType<Board>();
        m_AllNextShape = new List<Shape>();
        GenerateNextShape();

    }

    // Use this for initialization
    void Start()
    {
        this.transform.position = VectorF.RoundVector(this.transform.position);
        
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
        int i = 0;
        while (m_AllNextShape.Count < 3)
        {
            Shape randomShape = GetRandomShape();
     
            m_AllNextShape.Add(randomShape);
            Instantiate(randomShape, queueSpace[i].transform.position, Quaternion.identity);

            i++;
        }
        //fill it on the 3 xform

    }

    Shape GetNextShape()
    {
      //  GenerateNextShape();
        Shape nextShape = m_AllNextShape.First();

        m_AllNextShape.RemoveAt(0);
       

        return nextShape;
    }

    private Shape GetRandomShape()
    {
        int index = Random.Range(0, m_allShapes.Length);
        Shape shape = m_allShapes[index];
        return shape;
    }

}
