using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(MaskableGraphic))]

public class Shade : MonoBehaviour {

    public float m_startAlpha = 1f;
    public float m_targetAlpha = 0f;
    public float m_startDelay = 0f;
    public float m_timeToFade = 1f;

    float m_inc;
    MaskableGraphic m_graphic;
    float m_currentAlpha;
    Color m_originalColor;
    int i = 0;

    // Use this for initialization
    void Start () {

        m_graphic = GetComponent<MaskableGraphic>();

        Debug.Log("m_startAlpha =" + m_startAlpha);
        Debug.Log("m_targetAlpha =" + m_targetAlpha);
        Debug.Log("m_timeToFade =" + m_timeToFade);


        //m_inc = (m_startAlpha - m_targetAlpha) / m_timeToFade * Time.deltaTime;

        m_inc = Mathf.Abs( (m_targetAlpha - m_startAlpha) / m_timeToFade * Time.deltaTime);

        m_originalColor = m_graphic.color; 

        m_currentAlpha = m_startAlpha;


        Debug.Log("m_inc =" + m_inc);

        StartCoroutine("FadeRoutine");
    }

    IEnumerator FadeRoutine()
    {

        yield return new WaitForSeconds(m_startDelay);

        //while(m_currentAlpha > 0.01f )
        //{
        //    yield return new WaitForEndOfFrame();
        //    m_currentAlpha = Mathf.Abs(m_currentAlpha - m_inc);
        //    Debug.Log("m_currentAlpha =" + m_currentAlpha);
        //    m_graphic.color = new Color(m_originalColor.r, m_originalColor.g,m_originalColor.b , m_currentAlpha);

        //}

        m_graphic.CrossFadeAlpha(m_targetAlpha, m_timeToFade, true);

        Debug.Log("YEAH 3 sec after");
    }




}
