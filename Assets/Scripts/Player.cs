using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    private float _speedMultiplier = 2;
    [SerializeField]
    public int _lives = 3;
    [SerializeField]
    private GameObject laserprefab;
    [SerializeField]
    private GameObject TripleShotPrefab;
    [SerializeField]
    private float _firerate = 0.2f;
    private float _canfire = -1f;
    private Spawn_Manager _spawnManager;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject shieldvisual;
    [SerializeField]
    private int _score;
    private uimanager _uiMananger;
    [SerializeField]
    private GameObject _leftengine;
    [SerializeField]
    private GameObject _rightengine;
    [SerializeField]
    private AudioClip _laserSound;
    [SerializeField]
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<Spawn_Manager>();
        _uiMananger = GameObject.Find("Canvas").GetComponent<uimanager>();
        _audioSource = GetComponent<AudioSource>();
        if(_spawnManager == null){
            Debug.LogError("SpawnManager is not Connected");
        }
        if(_uiMananger == null){
            Debug.LogError("UI Manager is not Connected");
        }
        if(_audioSource == null)
        {
            Debug.LogError("AudioSource on the player is NULL");
        }
        else
        {
            _audioSource.clip = _laserSound;
        }
            _rightengine.SetActive(false);
            _leftengine.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();

        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire) {
            firelaser();
        }


    }
    void calculateMovement(){
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput);

        if(_isSpeedBoostActive == false)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        else{
            transform.Translate(direction * (_speed * _speedMultiplier) * Time.deltaTime);
        }

        if( transform.position.y >= 0){
            transform.position = new Vector3(transform.position.x , 0 , 0);
        }
        else if(transform.position.y <= -4.65f){
            transform.position = new Vector3(transform.position.x ,-4.65f, 0);
        }
        if(transform.position.x > 9){
            transform.position = new Vector3(9, transform.position.y, 0);
        }
        else if(transform.position.x < -9){
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
    }
    void firelaser(){
            _canfire = Time.time + _firerate;


            if(_isTripleShotActive == true){
                Instantiate(TripleShotPrefab, transform.position , Quaternion.identity);
            }else{
                Instantiate(laserprefab, transform.position + new Vector3(0, 1.04f, 0), Quaternion.identity);
            }       
             _audioSource.Play();
    }
    public void Damage(){

        if(_isShieldActive == true){
            shieldvisual.SetActive(false);
            _isShieldActive = false;
            return;
        }
        _lives--;

        if(_lives ==2){
            _leftengine.SetActive(true);
        }
        else if(_lives == 1)
        {
            _rightengine.SetActive(true);
            _leftengine.SetActive(true);
        }

        _uiMananger.UpdateLives(_lives);
        if(_lives == 0) {

            _spawnManager.onPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void TripleShot(){
        _isTripleShotActive = true;
        StartCoroutine(tripleShotActive());
    }
    IEnumerator tripleShotActive()
    {
       yield return new WaitForSeconds(5.0f);
       _isTripleShotActive = false;
    }

    public void SpeedBoost(){
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostActive());
    }
    IEnumerator SpeedBoostActive()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldActive(){
        _isShieldActive = true;
        shieldvisual.SetActive(true);
    }
    public void AddScore(int points){
        _score += points;
        _uiMananger.UpdateScore(_score);
    }
}
    
