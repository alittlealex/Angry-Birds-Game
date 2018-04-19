using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : Bird {

    public override void ShowSkill()
    {
        base.ShowSkill();
        Vector3 speed = m_Rigidbody.velocity;
        speed.x *= -1;
        m_Rigidbody.velocity = speed;
    }
}
