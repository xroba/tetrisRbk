using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    Board m_board;
    Spawner m_spawner;
    Shape m_activeShape;
    bool m_gameOver = false;
    public GameObject m_gameOverPanel;
    public float m_dropInterval = 0.5f;
    float m_timeDropShape = 1f;
   // float m_timeToNextKey;

  //  [Range(0.02f, 1f)]
  //  public float m_keyRepeatRate = 0.25f;

    float m_timeToNextLeftRightKey;
    [Range(0.02f, 1f)]
    public float m_LeftRighRepeatRate = 0.25f;

    float m_timeToRotate;
    [Range(0.02f, 1f)]
    public float m_RotateRepeatRate = 0.25f;

    float m_timeToDropDownKey;
    [Range(0.01f, 1f)]
    public float m_DropDownRepeatRate = 0.01f;




    // Use this for initialization
    void Start () {
        m_board = GameObject.FindObjectOfType<Board>();
        m_spawner = GameObject.FindObjectOfType<Spawner>();
        m_timeToNextLeftRightKey = Time.time;
        m_timeToRotate = Time.time;
        m_timeToDropDownKey = Time.time;

        if (m_activeShape == null)
        {
            m_activeShape = m_spawner.SpawnShape();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_gameOver)
        {
            return;
        }
      //  FakeGravity();
        PlayerInput();

    }

    //private void FakeGravity()
    //{
    //    if (m_activeShape)
    //    {
    //        if (Time.time > m_timeDropShape)
    //        {
    //            m_timeDropShape = m_dropInterval + Time.time;
    //            m_activeShape.MoveDown();

    //            if (!m_board.IsValidPosition(m_activeShape))
    //            {
    //                //Debug.Log("MOVE UP as invalid position is = " + m_activeShape.transform.position);
    //                m_activeShape.Moveup();
    //                m_board.StoreShapeGridPosition(m_activeShape);
    //                m_activeShape = m_spawner.SpawnShape();
    //            }
    //        }

    //    }
    //}

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Q) || Input.GetButton("MoveLeft") && Time.time > m_timeToNextLeftRightKey || Input.GetButtonDown("MoveLeft"))
        {

            m_activeShape.MoveLeft();
            m_timeToNextLeftRightKey = Time.time + m_LeftRighRepeatRate;

            if (!m_board.IsValidPosition(m_activeShape))
            {
                m_activeShape.MoveRight();
            }
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetButton("MoveRight") && Time.time > m_timeToNextLeftRightKey)
        {
            m_activeShape.MoveRight();
            m_timeToNextLeftRightKey = Time.time + m_LeftRighRepeatRate;

            if (!m_board.IsValidPosition(m_activeShape))
            {
                m_activeShape.MoveLeft();
            }
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("Rotate") && Time.time > m_timeToRotate) 
        {
            m_timeToRotate = Time.time + m_RotateRepeatRate;
            m_activeShape.RotateRight();

            if (!m_board.IsValidPosition(m_activeShape))
            {
                m_activeShape.RotateLeft();
            }     
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetButton("MoveDown") && Time.time > m_timeToDropDownKey || Time.time > m_timeDropShape)
        {
            m_timeToDropDownKey = Time.time + m_DropDownRepeatRate ;
            m_timeDropShape = m_dropInterval + Time.time;

            m_activeShape.MoveDown();

            if (!m_board.IsValidPosition(m_activeShape))
            {
                if (m_board.IsOverLimit(m_activeShape))
                {
                    GameOver();
                }
                else
                {
                    LandShape();
                }
                
            }


        }
    }

    private void GameOver()
    {
        m_activeShape.Moveup();
        m_gameOver = true;
        m_gameOverPanel.SetActive(true);

        Debug.Log("GAME OVER");
    }

    private void LandShape()
    {
        m_timeToNextLeftRightKey = Time.time;
        m_timeToDropDownKey = Time.time;
        m_timeToRotate = Time.time;
        m_activeShape.Moveup();
        m_board.StoreShapeGridPosition(m_activeShape);

        m_activeShape = m_spawner.SpawnShape();

        m_board.ClearAllRows();
    }

    public void Restart()
    {
        //Application.LoadLevel(Application.loadedLevel);
        m_gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
