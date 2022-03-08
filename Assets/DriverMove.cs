using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverMove : MonoBehaviour
{
    [SerializeField] float initSpeed = 15f;
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float boostSpeed = 10f;
    [SerializeField] float moveRotation = 1500f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;
        float moveSpeedAmount = Input.GetAxis("Vertical") * moveSpeed * deltaTime;
        float moveRotationAmount = Input.GetAxis("Horizontal") * moveRotation * deltaTime;

        transform.Translate(0, moveSpeedAmount, 0);
        
        //For reverse invert controllers
        moveRotationAmount = moveSpeedAmount >= 0 ? -moveRotationAmount : moveRotationAmount;    
        transform.Rotate(0,0, moveRotationAmount);
    }

    void OnCollisionEnter2D (Collision2D other){
        if (other.gameObject.tag != "Package") {
            moveSpeed = initSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "WaterBody") {
            moveSpeed = initSpeed - boostSpeed;
        }
        else if(other.tag == "Boost"){
            moveSpeed += boostSpeed;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.tag == "WaterBody") {
            moveSpeed = initSpeed;
        }
    }

}
