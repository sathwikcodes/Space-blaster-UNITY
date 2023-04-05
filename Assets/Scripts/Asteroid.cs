using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 20.0f;
    [SerializeField]
    private GameObject _explosionprefab;
    private Spawn_Manager _spawnmanager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnmanager = GameObject.Find("SpawnManager").GetComponent<Spawn_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Laser")
        {
            Instantiate(_explosionprefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnmanager.startSpawning();
            Destroy(this.gameObject, 0.25f);
        }
    }

}
