using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int PowerupId;
    [SerializeField]
    private AudioClip _clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other ){
        {
            if(other.tag == "Player"){
                Player player = other.transform.GetComponent<Player>();
                AudioSource.PlayClipAtPoint(_clip, transform.position);
                if(player != null)
                {
                    switch(PowerupId)
                    {
                        case 0:
                        player.TripleShot();
                        break;
                        case 1:
                        player.SpeedBoost();
                        break;
                        case 2:
                        player.ShieldActive();
                        break;
                        default:
                        Debug.Log("Default Values");
                        break;

                    }
                    
                }
                 Destroy(this.gameObject);
            }
        }
    }
}
