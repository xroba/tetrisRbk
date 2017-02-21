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
        initQueue();

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

        Shape nextShape = null;
        nextShape = GetNextShape();
        // Shape shape = Instantiate(nextShape, transform.position, Quaternion.identity) as Shape;

        //shape = GetNextShape();
        nextShape.transform.position = transform.position;
        nextShape.transform.localScale =  Vector3.one;

        return nextShape;

    }


    void initQueue()
    {
        m_AllNextShape.Clear();

        int i = 0;
        while (m_AllNextShape.Count < 3)
        {
            Shape tmpShape = GetRandomShape();
            Shape randomShape = null;
            randomShape = Instantiate(tmpShape, transform.position, Quaternion.identity);
            randomShape.transform.position = queueSpace[i].transform.position + tmpShape.m_queueOffset;
            randomShape.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            m_AllNextShape.Add(randomShape);
            tmpShape = null;
            i++;
        }
    }

    void GenerateNextShape()
    {

        for (int i = 0; i < m_AllNextShape.Count; i++)
        {
            m_AllNextShape[i].transform.position = queueSpace[i].transform.position + m_AllNextShape[i].m_queueOffset;

        }

        //generate last one.
        Shape tmpShape = GetRandomShape();
        Shape newShape = Instantiate(tmpShape, queueSpace[m_AllNextShape.Count].transform.position + tmpShape.m_queueOffset, Quaternion.identity);
        newShape.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        m_AllNextShape.Add(newShape);
    }

    Shape GetNextShape()
    {
      //  GenerateNextShape();
        Shape nextShape = m_AllNextShape.First();
        m_AllNextShape.RemoveAt(0);
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
