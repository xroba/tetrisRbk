using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    Board m_board;
    Spawner m_spawner;
    Shape m_activeShape;
    Ghost m_ghost;


    bool m_gameOver = false;
    public GameObject m_gameOverPanel;
    public GameObject m_PausePanel;
    public float m_dropInterval = 0.5f;
    float m_dropIntervalModded;
    float m_timeDropShape = 1f;
    SoundManager m_soundManager;
    bool m_isPause = false;
    ScoreManager m_scoreManager;
    Holder m_holder;
    
    public ToggleIcon m_rotateToggleIcon;
    bool m_rotateRight = true;
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
        Time.timeScale = 1f;
        m_board = GameObject.FindObjectOfType<Board>();
        m_spawner = GameObject.FindObjectOfType<Spawner>();
        m_holder = GameObject.FindObjectOfType<Holder>();
        m_timeToNextLeftRightKey = Time.time;
        m_timeToRotate = Time.time;
        m_timeToDropDownKey = Time.time;
        m_soundManager = FindObjectOfType<SoundManager>();
        m_scoreManager = FindObjectOfType<ScoreManager>();
        m_ghost = FindObjectOfType<Ghost>();


        if (m_activeShape == null)
        {
            m_activeShape = m_spawner.SpawnShape();
        }

        if (m_PausePanel.active)
        {
            m_PausePanel.SetActive(false);
        }

        m_dropIntervalModded  = m_dropInterval;

    }

    // Update is called once per frame
    void Update ()
    {
        if (m_gameOver)
        {
            return;
        }
        PlayerInput();
    }

    void LateUpdate()
    {
        if (m_ghost)
        {
            m_ghost.CreateGhost(m_activeShape, m_board);
        }
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
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Q) || Input.GetButton("MoveLeft")) && Time.time > m_timeToNextLeftRightKey || Input.GetButtonDown("MoveLeft"))
        {

            m_activeShape.MoveLeft();
           // m_activeGhost.MoveLeft();

            m_soundManager.PlayFxMove();
            m_timeToNextLeftRightKey = Time.time + m_LeftRighRepeatRate;

            if (!m_board.IsValidPosition(m_activeShape))
            {
                m_activeShape.MoveRight();
                m_soundManager.PlayFxErrorSound();
            }
        }

        else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKey(KeyCode.D) || Input.GetButton("MoveRight")) && Time.time > m_timeToNextLeftRightKey)
        {

            m_activeShape.MoveRight();
           // m_activeGhost.MoveRight();
            m_soundManager.PlayFxMove();
            m_timeToNextLeftRightKey = Time.time + m_LeftRighRepeatRate;

            if (!m_board.IsValidPosition(m_activeShape))
            {
                m_activeShape.MoveLeft();
                m_soundManager.PlayFxErrorSound();
            }
        }

        else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z) || Input.GetKey(KeyCode.Z) || Input.GetButtonDown("Rotate")) && Time.time > m_timeToRotate) 
        {
            m_timeToRotate = Time.time + m_RotateRepeatRate;


            RotateclockWise(m_rotateRight);
           
            m_soundManager.PlayFxMove();

            if (!m_board.IsValidPosition(m_activeShape))
            {
                RotateclockWise(!m_rotateRight);
                m_soundManager.PlayFxErrorSound();
                //m_activeShape.RotateLeft();
            }     
        }

        else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKey(KeyCode.S) ||  Input.GetButton("MoveDown") && Time.time > m_timeToDropDownKey) || Time.time > m_timeDropShape)
        {
            m_timeToDropDownKey = Time.time + m_DropDownRepeatRate;
            m_timeDropShape = m_dropIntervalModded + Time.time;

            m_activeShape.MoveDown();

            if (!m_board.IsValidPosition(m_activeShape))
            {
                if (m_board.IsOverLimit(m_activeShape))
                {
                    GameOver();
                    m_soundManager.PlayFxGameOver();
                }
                else
                {
                    LandShape();
                    m_ghost.Reset();
                }
                
            }


        } else if (Input.GetKeyDown(KeyCode.Space))
        {
            // toggle between current active and toggle if exist, if not just put it on hold
             if(m_holder.HasShape() == true)
            {

                Shape tmpShape = m_holder.RetrieveHoldShape();
                Transform tmpPosition = m_activeShape.transform;
                tmpShape.transform.position = new Vector3(tmpPosition.position.x, tmpPosition.position.y, tmpPosition.position.z);
                tmpShape.transform.localScale = Vector3.one;


                m_holder.PutShapeOnHold(m_activeShape);

                m_activeShape = tmpShape;
                //   m_activeShape.transform.position = tmpPosition.position;
               
                m_ghost.Reset();
            }   else
            {
                m_holder.PutShapeOnHold(m_activeShape);
                m_activeShape = m_spawner.SpawnShape();
                m_ghost.Reset();
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
        m_soundManager.PlayFxLand();

        m_timeToNextLeftRightKey = Time.time;
        m_timeToDropDownKey = Time.time;
        m_timeToRotate = Time.time;
        m_activeShape.Moveup();
        m_board.StoreShapeGridPosition(m_activeShape);

        m_activeShape = m_spawner.SpawnShape();

        m_board.ClearAllRows();

        if (m_board.m_completedRows > 0)
        {
            m_scoreManager.ScoreLines(m_board.m_completedRows);
            
            if(m_scoreManager.m_didLevelUp)
            {
                m_soundManager.PlayVocalLevelUp();
                m_dropIntervalModded -=  0.1f * m_scoreManager.m_level;


            } else
            {
                switch (m_board.m_completedRows)
                {
                    case 2:
                        m_soundManager.PlayVocalDoubleKill();
                        break;
                    case 3:
                        m_soundManager.PlayVocalHattrick();
                        break;
                    case 4:
                        m_soundManager.PlayVocalKillingSpree();
                        break;
                    default:
                        m_soundManager.PlayFxClearRow();
                        break;
                }
            }        
        }
    }

    public void ToggleRotate()
    {
        if (m_rotateRight)
        {
            m_rotateRight = false;
            
        } else
        {
            m_rotateRight = true;
        }

        m_rotateToggleIcon.SetToggleIconOnOff(m_rotateRight);
    }

    public void RotateclockWise(bool clockwise)
    {
        if (clockwise)
        {
            m_activeShape.RotateRight();
        }
        else
        {
            m_activeShape.RotateLeft();
        }
    }

    public void Restart()
    {
        //Application.LoadLevel(Application.loadedLevel);
        m_gameOverPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TogglePause()
    {
        m_isPause = !m_isPause;


        m_PausePanel.SetActive(m_isPause);

        Time.timeScale = (m_isPause) ? 0 : 1;

    }
}
