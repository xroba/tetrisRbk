using UnityEngine;
using System.Collections;

public class Shape : MonoBehaviour {

   public bool m_canRotate = true;
    Board m_board;
    // Use this for initialization

    public void Move(Vector3 direction)
    {
        this.transform.position += direction;
    }

    public void Rotate(Vector3 rotation)
    {
        this.transform.Rotate(rotation);
    }


    public void MoveLeft()
    {
       Move(new Vector3(-1,0,0));
    }

    public void MoveRight()
    {
        Move(new Vector3(1, 0, 0));
    }

    public void MoveDown()
    {
        Move(new Vector3(0, -1, 0));
    }

    public void Moveup()
    {
        Move(new Vector3(0, 1, 0));
    }

    public void RotateLeft()
    {
        if(m_canRotate)
            Rotate(new Vector3(0, 0, 90));
    }

    public void RotateRight()
    {
        if (m_canRotate)
            Rotate(new Vector3(0, 0, -90));
    }

    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
      
    }


}
