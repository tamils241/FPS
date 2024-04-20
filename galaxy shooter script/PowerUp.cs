using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed=3.0f;
    // Start is called before the first frame update
    [SerializeField]
    private int powerUpId;
    [SerializeField]
    private AudioClip _audioClip;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        movePowerUp();
    }
    private void movePowerUp() 
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -5.81)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.tag=="Player")
        {
           
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
           
            if(player !=null)
            {
                
                switch (powerUpId)
                    {
                    case 0:
                        player.trippleShotActive();
                        break;
                    case 1:
                        player.speedPowerUpActive();
                        
                        break;
                    case 2:
                        player.shieldActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }


            }
            Destroy(this.gameObject);
        }
    }

    
}
