using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _trippleshotprefab;
    private float _firerate = 0.5f;
    private float _canfire = -.5f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTrippleShotActive = false;

    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new postion (0,0,0)

        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("The spawn manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Calculate_movement();

        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)
        {
            FireLaser();
        }

    }

    void Calculate_movement()
    {
        // new Vector3(1, 0, 0) is equiavalent to Vector3.right//1 unit in unity --- 1 meter in real world //thus by mutiplying it converts 60 m per frame to 1 m per frame.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -5.020648f)
        {
            transform.position = new Vector3(transform.position.x, -5.020648f, 0);
        }

        if (transform.position.x >= 12.10332f)
        {
            transform.position = new Vector3(-6.284234f, transform.position.y, 0);
        }
        else if (transform.position.x <= -6.284234f)
        {
            transform.position = new Vector3(12.10332f, transform.position.y, 0);
        }
    }
     
    void FireLaser()
    {
        _canfire = Time.time + _firerate;
        if(_isTrippleShotActive==true)
        {
            Instantiate(_trippleshotprefab, transform.position , Quaternion.identity); ;
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
       
    }

    public void Damage()
    {
        _lives--;
        if(_lives<1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
