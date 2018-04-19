using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird {

    private List<Pig> blocks = new List<Pig>();

    /// <summary>
    /// 进入触发器
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            blocks.Add(collision.gameObject.GetComponent<Pig>());
        }
    }

    /// <summary>
    /// 离开触发器
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blocks.Remove(collision.gameObject.GetComponent<Pig>());
        }
    }

    public override void ShowSkill()
    {
        base.ShowSkill();
        Clean();
        if (blocks.Count > 0 && blocks != null)
        {
            for(int i = 0; i < blocks.Count; i++)
            {
                blocks[i].Dead();
            }
        }

        
    }

    /// <summary>
    /// 处理当黑鸟爆炸后的事情
    /// </summary>
    private void Clean()
    {
        m_Rigidbody.velocity = Vector3.zero;
        GameObject.Instantiate(boom, transform.position, Quaternion.identity);
        m_SpriteRenderer.enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        m_Trail.TrailsClear();
    }

    protected override void Next()
    {
        GameManager._instance.birds.Remove(this);
        GameObject.Destroy(gameObject);
        GameManager._instance.NextBird();
    }
}
