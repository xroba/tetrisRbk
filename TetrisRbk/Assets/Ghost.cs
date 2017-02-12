using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

    Shape m_ghostShape;
    bool m_hitBottom;
    Color GreyColor = new Color(1f, 1f, 1f, 0.2f);

	// Use this for initialization
	void Start () {
        
	}
	
    public void CreateGhost(Shape activeShape, Board board)
    {

        if (!m_ghostShape) {
            m_ghostShape = Instantiate(activeShape, activeShape.transform.position, Quaternion.identity) as Shape;
            m_ghostShape.gameObject.name = "GhostShape";


            SpriteRenderer[] allRenderer = m_ghostShape.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer r in allRenderer)
            {
                r.color = GreyColor;
            }

        } else
        {
            m_ghostShape.transform.position = activeShape.transform.position;
            m_ghostShape.transform.rotation = activeShape.transform.rotation;
        }



        m_hitBottom = false;

        while (!m_hitBottom)
        {
            m_ghostShape.MoveDown();
            if (!board.IsValidPosition(m_ghostShape))
            {
                m_ghostShape.GetComponent<Shape>().Moveup();
                m_hitBottom = true;
            }
        }


        //if (m_ghostShape)
        //{
        //    while (board.IsValidPosition(m_ghostShape.GetComponent<Shape>()))
        //    {
        //        m_ghostShape.GetComponent<Shape>().MoveDown();
        //    }

        //    m_ghostShape.GetComponent<Shape>().Moveup();
        //}


         // return ghostShape.GetComponent<Shape>();
    }

    public void Reset()
    {
        Destroy(m_ghostShape.gameObject);
    }
}
