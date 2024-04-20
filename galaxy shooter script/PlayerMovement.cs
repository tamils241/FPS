using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float fireRate = 0.15f;
    private float nextFire = 0.0f;
    private Spawning _spawnManager;
    [SerializeField]
    private bool isTripleshotActive = false;
    [SerializeField]
    private bool isSpeedPowerActive = false;
    [SerializeField]
    private bool _isShieldActive = false;
    private int _speedMultiplier=2;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score = 0;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _rightEngine,_leftEngine;
    private UI_Manager _uimanager;
    
    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // to get the position
        transform.position = new Vector3(0, 0, 0);
       _spawnManager = GameObject.Find("SpawnManager").GetComponent<Spawning>();
        _uimanager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _audioSource =GetComponent<AudioSource>();
       
        if(_spawnManager == null)
        {
            Debug.LogError("The SpwanManager is null");
        }
        if (_uimanager == null)
        {
            Debug.LogError("The UIMnager is null");
        }
        if (_audioSource == null)
        {
            Debug.LogError("The audio source on the palyer is null");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }

       //if(_uimanager )
    }

    // Update is called once per frame
    void Update()
    {
       
        movementPlayer();
        shooting();
        
    }
    private void movementPlayer()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(xInput, yInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (transform.position.y <= -3.9)
        {
            transform.position = new Vector3(transform.position.x, -3.9f, transform.position.z);
        }

        if (transform.position.x >= 10.0)
        {
            transform.position = new Vector3(-10.0f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -10.0)
        {
            transform.position = new Vector3(10.0f, transform.position.y, transform.position.z);
        }
    }
    private void shooting()
    {
       
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
           fireLaser();
        }
       
    }

    private void fireLaser()
    {

        nextFire = Time.time + fireRate;
        if (isTripleshotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position , Quaternion.identity);
        }
        else
        {
            Instantiate(_laser, transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        }
        // play the laser audio Clip
        _audioSource.Play();
       // _audioSource.volume = 0.16f;


    }

    public void damagePlayer()
    {
        
        if(_isShieldActive ==true)
        {
            _isShieldActive = false;
            //disable the shield visualiser
            _shieldVisualizer.SetActive(false);

            return;
           
        }

        _lives--;
        _uimanager.updateLives(_lives);
        if(_lives == 2)
        {
            _leftEngine.SetActive(true);
        }else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }
        
        // if lives is 2 enable rightengine
        // if lives is 1 enable leftEngine

        if (_lives < 1)
        {
             _spawnManager.onPlayerDeath();
             Destroy(this.gameObject);
        }

    }

    public void trippleShotActive()
    {
        isTripleshotActive = true;
        StartCoroutine(powerDownRoutine());
    }

   

    IEnumerator powerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isTripleshotActive = false;
    }

    public void shieldActive()
    
    {
        _isShieldActive = true;
        //enable the visualiser
        _shieldVisualizer.SetActive(true);
        StartCoroutine(sheildPowerActive());
       
    }

    IEnumerator sheildPowerActive()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedPowerActive = false;
        _shieldVisualizer.SetActive(false);
    }

    public void speedPowerUpActive()
    {

        isSpeedPowerActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(speedPowerUpActiveMethod());
    }

    IEnumerator speedPowerUpActiveMethod()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _speedMultiplier;
        isSpeedPowerActive = false;
    }

    // method to add to score 
    // communicate with UI to update score
    public void addScore(int points)
    {
        _score += points;
        _uimanager.updateScore(_score);
    }


}
