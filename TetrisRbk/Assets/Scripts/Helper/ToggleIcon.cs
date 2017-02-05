using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleIcon : MonoBehaviour {

    public Sprite m_iconOn;
    public Sprite m_iconOff;
    bool iconState;
    public Image m_iconImage;


   public void SetToggleIconOnOff(bool state)
    {
        if (state)
        {
            m_iconImage.sprite = m_iconOn;
        }
        else
        {
            m_iconImage.sprite = m_iconOff;
        }
    }
}
