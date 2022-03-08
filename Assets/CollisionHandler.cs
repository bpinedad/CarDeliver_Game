using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    //Store sprite renderer
    SpriteRenderer spriteRenderer;
    [SerializeField] GameObject bridgeObject;
    [SerializeField] GameObject bridgeUpObject;
    [SerializeField] GameObject bridgeDownObject;
    [SerializeField] GameObject bridgeLeftObject;
    [SerializeField] GameObject bridgeRightObject;
    [SerializeField] GameObject package;

    bool hasPackage = false;
    bool inBridge = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 3;
    }

    //On Collision with solid object
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Package" && !hasPackage) {                    
            package.GetComponent<Renderer>().enabled = false;
            package.GetComponent<BoxCollider2D>().isTrigger = true; 
            hasPackage = true;
        }   
    }

    //if outside bridge, go to all triggers
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Bridge") {           
            inBridge = false;
            bridgeUpObject.GetComponent<BoxCollider2D>().isTrigger = true; 
            bridgeDownObject.GetComponent<BoxCollider2D>().isTrigger = true; 
            bridgeLeftObject.GetComponent<BoxCollider2D>().isTrigger = true; 
            bridgeRightObject.GetComponent<BoxCollider2D>().isTrigger = true; 

            bridgeUpObject.transform.localScale = new Vector3(19f, 0.2f, 0f); 
            bridgeDownObject.transform.localScale  = new Vector3(19f, 0.2f, 0f);
            bridgeLeftObject.transform.localScale = new Vector3(0.2f, 8f, 0f);
            bridgeRightObject.transform.localScale  = new Vector3(0.2f, 8f, 0f);
        } 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Handle bridge
        if (other.tag == "BridgeUp") {           
            InBridge(true);
        } 
        else if (other.tag == "BridgeDown") {
            InBridge(false);
        } 

        //Handle package pickup
        if (other.tag == "DeliveryPoint" && hasPackage) {         
            //Create random coors inside world
            Debug.Log("Delivered");
            NewPackage();
            hasPackage = false;
            Destroy(other.gameObject, 0.2f);
        }
    }

    private void InBridge(bool above){
        //Entered bridge from above
        if (above) {
            bridgeObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            //Togle up and down
            bridgeUpObject.GetComponent<BoxCollider2D>().isTrigger = inBridge; 
            bridgeDownObject.GetComponent<BoxCollider2D>().isTrigger = inBridge;
        }
        else if(!above) {
            bridgeObject.GetComponent<SpriteRenderer>().sortingOrder = 4;
            //Togle left and right
            bridgeLeftObject.GetComponent<BoxCollider2D>().isTrigger = inBridge; 
            bridgeRightObject.GetComponent<BoxCollider2D>().isTrigger = inBridge; 
        }

        bridgeUpObject.transform.localScale += above ? new Vector3(3f, 0f, 0f) : new Vector3(-3f, 0f, 0f); 
        bridgeDownObject.transform.localScale  += above ? new Vector3(3f, 0f, 0f) : new Vector3(-3f, 0f, 0f); 
        bridgeLeftObject.transform.localScale += above ? new Vector3(0f, -5f, 0f) : new Vector3(0f, 5f, 0f); 
        bridgeRightObject.transform.localScale  += above ? new Vector3(0f, -5f, 0f) : new Vector3(0f, 5f, 0f); 

        inBridge = !inBridge;

    }

    private void NewPackage(){
        float x = Random.Range(-30f, 30f);
        float y = Random.Range(-15f, 25f);
        package.transform.position = new Vector3(x, y , 0);
        
        package.GetComponent<BoxCollider2D>().isTrigger = false; 
        //package.GetComponent<Rigidbody2D>().isDynamic = true;
        package.GetComponent<Renderer>().enabled = true;
    }
}
