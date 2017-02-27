 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    int m_score = 0;
    int m_lines;
    public int m_level = 1;
    public int m_linesPerLevel = 5;
    public bool m_didLevelUp = false;

    const int m_minLines = 1;
    const int m_maxLines = 4;


    public Text m_scoreTxtComponent;
    public Text m_linesTxt;
    public Text m_levelTxt;

	// Use this for initialization
	void Start () {
        ResetScore();
	}
	

    public void ScoreLines(int value)
    {
        value = Mathf.Clamp(value, m_minLines, m_maxLines);

        m_didLevelUp = false;
        switch (value)
        {

            case 1:
                m_score += 40 * m_level;
                break;
            case 2:
                m_score += 100 * m_level;
                break;
            case 3:
                m_score += 300 * m_level;
                break;
            case 4:
                m_score += 1200 * m_level;
                break;
        }

        m_lines -= value;
        if(m_lines <= 0)
        {
            LevelUp();
            
        }
        UpdateUIText();
    }

    public void ResetScore()
    {
        m_score = 0;
        m_level = 1;
        m_lines = m_linesPerLevel * m_level;
        UpdateUIText();
    }

    public void UpdateUIText()
    {

        if (m_scoreTxtComponent)
        {
            m_scoreTxtComponent.text = PadZero(m_score,5);
        }

        if (m_linesTxt)
        {
            m_linesTxt.text = m_lines.ToString();
        }

        if (m_levelTxt)
        {
            m_levelTxt.text = m_level.ToString();
        }

    }

    string PadZero(int n, int numberOfZero)
    {

        string nStr = n.ToString();


        while (nStr.Length < numberOfZero)
        {
            nStr = "0" + nStr;
        }

        return nStr;
    }

    public void LevelUp()
    {

        m_level++;
        m_lines = m_linesPerLevel * m_level;
        m_didLevelUp = true;
    }
}
