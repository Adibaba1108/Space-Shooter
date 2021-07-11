using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 2.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if (transform.position.y <= -5.020648f)
        {
            float randomX = Random.Range(-5.8f, 11.3f);
            transform.position = new Vector3(randomX, 4.8f, 0);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if other is player 


        if(other.tag == "Player")
        {
            //damage the player

            Player player = other.transform.GetComponent<Player>();
            if(player!= null)
            {
                player.Damage();
            }

            //damage us(enemy)
            Destroy(this.gameObject);
        }

        //if other is laser
        //laser //then us(enemy)

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
