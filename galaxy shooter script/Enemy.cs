using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private Animator _anim;
    private PlayerMovement _player;
    private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerMovement>();
         
        _audioSource = GetComponent<AudioSource>();
        if(_player == null)
        {
            Debug.LogError("Null Object");
        }

        _anim=GetComponent<Animator>();
        if(_anim == null)
        {
            Debug.LogError("Animotr is null");
        }

    }

    // Update is called once per frame
    void Update()
    {
       
        movingEnemy();
    }
    private void movingEnemy()
    {
        transform.Translate(Vector3.down * _speed*Time.deltaTime);
        if(transform.position.y <= -5.5)
        {
            transform.position = new Vector3(Random.Range(-9.5f,9.5f),7.0f,0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            // damage Player -lives system
            PlayerMovement player = other.transform.GetComponent<PlayerMovement>();
            if (player != null)
            {

                player.damagePlayer();

            }
            _anim.SetTrigger("onEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject,2.8f);
        }
    
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            _anim.SetTrigger("onEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            if(_player!=null)
            {
                _player.addScore(10);
            }
            // add 10 to score
           
            Destroy(this.gameObject,2.8f);

        }
    }

}
