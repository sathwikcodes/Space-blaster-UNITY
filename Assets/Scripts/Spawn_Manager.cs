using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyprefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] powerUps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void startSpawning()
    {
        StartCoroutine(SpawnEnemyManager());
        StartCoroutine(SpawnPowerupManager());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator SpawnEnemyManager()
    {
        yield return new WaitForSeconds(3.0f);
        while(_stopSpawning == false)
        {
            Vector3 postTospawn = new Vector3(Random.Range(-8f,8.1f), 7.1f, 0);
            GameObject newEnemy =  Instantiate(_enemyprefab, postTospawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform; 
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupManager(){
        while(_stopSpawning == false) {
            {
                Vector3 postTospawn = new Vector3(Random.Range(-8f,8.1f), 7.1f, 0);
                int RandomPowerup = Random.Range(0, 3);
                GameObject newTripleshot =  Instantiate( powerUps[RandomPowerup],postTospawn, Quaternion.identity);
                newTripleshot.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(Random.Range(4,10));
            }
        }
    }

    public void onPlayerDeath(){
        _stopSpawning = true;
    }
}
