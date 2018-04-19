using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {

    public float maxSpeed = 10;
    public float minSpeed = 5;
    private SpriteRenderer renderer;
    public Sprite hurt;
    public GameObject boom;
    public GameObject score;

    public bool isPig = false;

    public AudioClip audio_hurt;
    public AudioClip audio_dead;
    public AudioClip audio_collision;

    private void Awake()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 其他物体击中的时候
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && collision.relativeVelocity.magnitude > minSpeed)
        {
            AudioPlay(audio_collision);
        }

        if(collision.relativeVelocity.magnitude > maxSpeed)
        {
            Dead();
        }
        else if(collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed)
        {
            AudioPlay(audio_hurt);
            renderer.sprite = hurt;
        }
    }

    /// <summary>
    /// 死的时候自己消失并制造爆炸效果
    /// </summary>
    public void Dead()
    {
        if (isPig)
        {
            AudioPlay(audio_dead);
            GameManager._instance.pigs.Remove(this);
            GameObject.Destroy(gameObject);
        }
        else
        {
            AudioPlay(audio_dead);
            GameObject.Destroy(gameObject,0.2f);
        }
        GameObject.Instantiate(boom, transform.position, Quaternion.identity);
        GameObject go = GameObject.Instantiate(score, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        Destroy(go, 1.5f);
    }

    private void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
