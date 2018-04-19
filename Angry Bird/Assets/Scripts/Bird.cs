using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    private bool isClick = false;
    public Transform rightPos;
    public Transform leftPos;
    public float maxDis;
    public float minDis;
    private SpringJoint2D m_SpringJoint;
    protected Rigidbody2D m_Rigidbody;

    public LineRenderer right_Renderer;
    public LineRenderer left_Renderer;

    public GameObject boom;

    protected TestMyTrail m_Trail;
    private bool canMove = true;
    public float smooth = 3;

    public AudioClip audio_select;
    public AudioClip audio_fly;

    private bool isSkill = false;

    public Sprite hurt;
    protected SpriteRenderer m_SpriteRenderer;
    

    private void Awake()
    {
        m_SpringJoint = gameObject.GetComponent<SpringJoint2D>();
        m_Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        m_Trail = gameObject.GetComponent<TestMyTrail>();
        m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (canMove)
        {
            AudioPlay(audio_select);
            isClick = true;
            m_Rigidbody.isKinematic = true;
        }
    }

    private void OnMouseUp()
    {
        if (canMove)
        {
            isClick = false;
            m_Rigidbody.isKinematic = false;
            Invoke("Fly", 0.1f);
            right_Renderer.enabled = false;
            left_Renderer.enabled = false;
            canMove = false;
        }     
    }

    private void Update()
    {
        //画出绑带
        Line();
        //当鼠标按下拖动时调用
        if (isClick) 
        {
            //让小鸟跟随鼠标位置
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);

            //限定最大距离
            if(Vector3.Distance(transform.position,rightPos.position) > maxDis)
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;
                pos *= maxDis;
                transform.position = pos + rightPos.position;
            }
        }

        //相机跟随
        float birdX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(birdX, 0, 10), Camera.main.transform.position.y, Camera.main.transform.position.z), smooth * Time.deltaTime);

        //飞行过程中按下鼠标左键释放技能
        if (isSkill)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowSkill();
            }
        }



    }

    /// <summary>
    /// 松开鼠标的时候延迟一会儿执行
    /// </summary>
    private void Fly()
    {
        isSkill = true;
        AudioPlay(audio_fly);
        m_SpringJoint.enabled = false;
        Invoke("Next", 4.0f);
        m_Trail.TrailsStart();
    }

    /// <summary>
    /// 画出橡皮筋
    /// </summary>
    private void Line()
    {
        right_Renderer.SetPosition(0, rightPos.position);
        right_Renderer.SetPosition(1, transform.position);

        left_Renderer.SetPosition(0, leftPos.position);
        left_Renderer.SetPosition(1, transform.position);
    }

    /// <summary>
    /// 下一只鸟飞出
    /// </summary>
    protected virtual void Next()
    {
        GameManager._instance.birds.Remove(this);
        GameObject.Destroy(gameObject);
        GameObject.Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._instance.NextBird();
    }

    /// <summary>
    /// 碰撞后拖尾消失
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isSkill = false;
        m_Trail.TrailsClear();
        if(collision.gameObject.tag == "Enemy")
        {
            Hurt();
        }
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    private void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    /// <summary>
    /// 释放技能
    /// </summary>
    public virtual void ShowSkill()
    {
        isSkill = false;
    }

    public void Hurt()
    {
        m_SpriteRenderer.sprite = hurt;
    }
}
