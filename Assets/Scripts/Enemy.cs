using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private AudioSource _audiosource;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audiosource = GetComponent<AudioSource>();
        
        _anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed *Time.deltaTime);
        if(transform.position.y < -6f){
        float random = Random.Range(-8f,8.1f);
        transform.position = new Vector3(random ,7.1f,0);
    }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            Player player = other.transform.GetComponent<Player>();
            if (player != null){
                player.Damage();
            }
            _anim.SetTrigger("onEnemyDeath");
            _speed = 0;
            _audiosource.Play();
            Destroy(this.gameObject, 2.8f);
        }
        if(other.tag == "Laser"){
            Destroy(other.gameObject);
            if(_player!= null){
                _player.AddScore(10);
            }
            _anim.SetTrigger("onEnemyDeath");
            _speed = 0;
            _audiosource.Play();
            Destroy(this.gameObject, 2.8f);
        }
    }
    
    
}
