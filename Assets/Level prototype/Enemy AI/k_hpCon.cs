using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class k_hpCon : MonoBehaviour
{
    public float m_hp = 100;

    public void DecreaseHealth(float value)
    {
        m_hp = m_hp - value;
    }

}
