﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Board : MonoBehaviour {

    public Transform m_emptySprite;
    public GameObject grid;
    public int m_width = 10;
    public int m_height = 30;
    public int m_header = 8;	// Use this for initialization
    public int m_completedRows = 0;
    public Text m_scoreTxtComponent;
    public Transform m_rowGlowFx;
    public Transform m_squareGlowfx;

    public Transform[,] m_grid;

    void Awake()
    {
        m_grid = new Transform[m_width, m_height];
    }
    void Start () {
        DrawEmptyCells();
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    public void StoreShapeGridPosition(Shape shape)
    {
        if(shape == null)
        {
            return;
        }

        foreach(Transform child in shape.transform)
        {
            Vector2 pos = VectorF.RoundVector(child.position);
            m_grid[(int)pos.x, (int)pos.y] = child;
        }
    }

    bool isWithinBoard(int x, int y)
    {
        return (x >= 0 && x < m_width && y >= 0);
    }

    bool IsOccupied(int x, int y, Shape shape)
    {
        //Debug.Log("IsOccupied request : x = " + x + " y =" + y);

        //if(m_grid[x, y] == null)
        //{
        //    Debug.Log("false");
        //} else
        //{
        //    Debug.Log("TRUE OCCUPIED");
        //    //throw new System.Exception("NOOOO");
        //}

       

        return (m_grid[x, y] != null && m_grid[x, y].parent != shape.transform);
    }

    public bool IsValidPosition (Shape shape)
    {
        foreach( Transform child in shape.transform)
        {
            Vector2 checkPos = VectorF.RoundVector(child.position); 

            if(!isWithinBoard( (int)checkPos.x, (int)checkPos.y))
            {
                return false;
            }

            if(IsOccupied( (int)checkPos.x, (int)checkPos.y, shape))
            {
                return false;
            }

           
        }
        return true;
    }



    void DrawEmptyCells()
    {
        for(int x = 0; x < m_width; x++)
        {
            for (int y = 0; y < m_height - m_header; y++)
            {
                var emptySprite = Instantiate(m_emptySprite);
                emptySprite.position = new Vector3(x, y, 0);
                emptySprite.name = "esq_" + x + "_" + y;
                emptySprite.parent = grid.transform;

            }

        }
    }

    public bool IsCompleteLine(int y)
    {
       // Debug.Log("check on y = " + y);
        for(int x = 0;  x < m_width; x++)
        {
            if (m_grid[x,y] == null)
            {
                return false;
            }
        }
        return true;
    }

    void ClearRow(int y)
    {
        for (int x = 0; x < m_width; x++)
        {
            if ( m_grid[x,y] != null ){
                Destroy(m_grid[x, y].gameObject);        
            }

            m_grid[x, y] = null;
        }
       
    }

    void ShiftOneRowDown(int y)
    {
        for (int x = 0; x < m_width; x++)
        {
            if (m_grid[x, y] != null)
            {
                m_grid[x, y - 1] = m_grid[x, y];
                m_grid[x, y] = null;
                m_grid[x, y-1].position += new Vector3(0, -1, 0); 
            }     
        }
    }

    void ShiftRowsDown(int startY)
    {
        for(int y = startY; y < m_height; y++)
        {
            ShiftOneRowDown(y);
        }
    }

    public IEnumerator ClearAllRows()
    {
        m_completedRows = 0; //for scoring :-)

        PlayRowDeleteFx();
        yield return new WaitForSeconds(0.3f);

        for (int y = 0; y < m_height; y++)
        {
            if (IsCompleteLine(y))
            {

                ClearRow(y);
                m_completedRows++;
                ShiftRowsDown(y + 1);
                yield return new WaitForSeconds(0.2f);
                y--;

            }
        }



    }

    //public void ClearAllRows()
    //{
    //    Debug.Log("Delete Line");
    //    m_completedRows = 0; //for scoring :-)
    //   // PlayRowDeleteFx();

    //    for (int y = 0; y < m_height; y++)
    //    {
    //        if (IsCompleteLine(y))
    //        {
    //            ClearRow(y);
    //            m_completedRows++;
    //            ShiftRowsDown(y + 1);
    //            y--;

    //        }
    //    }
    //}

    public bool IsOverLimit(Shape shape)
    {
        foreach(Transform child in shape.transform)
        {
            if(child.position.y >= (m_height - m_header - 1))
            {
                return true;
            }
        }
        return false;
    }

    public void PlayRowDeleteFx()
    {

        for(int y = 0; y < m_height; y++){

            if (IsCompleteLine(y))
            {
               // Transform rowGlowFx = Instantiate(m_rowGlowFx);
                GameObject mrowGlowFx = Instantiate(Resources.Load("GlowingRowFx")) as GameObject ;

                mrowGlowFx.transform.position = new Vector3(0f, y, -1.1f);
                Debug.Log("Play row fx on y = " + y);
                Particles particles = mrowGlowFx.GetComponent<Particles>();

                particles.PlayParticle();
            }
        }
    }

    public void PlayLandingShapeGlowFx(Shape shape)
    {
        foreach(Transform square in shape.transform)
        {
          Transform SquareFx = Instantiate(m_squareGlowfx, new Vector3(square.position.x,square.position.y + 1 ,-1.1f), Quaternion.identity ) ;

            SquareFx.GetComponent<Particles>().PlayParticle();
        }



    }
}
