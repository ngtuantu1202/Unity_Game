using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float move;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        //3 bien: x, y, z...Gioi han khung hinh 60FPS
        transform.position += new Vector3(move, 0f, 0f) * Time.fixedDeltaTime * speed; 
    }
}
