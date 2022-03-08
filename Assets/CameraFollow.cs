using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Object to follow
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject packageObject;
    Vector3 packagePosition;

    private void Start() {
        packagePosition = packageObject.transform.position;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        float xDiff = Mathf.Abs(packageObject.transform.position.x - packagePosition.x);
        float yDiff = Mathf.Abs(packageObject.transform.position.y - packagePosition.y);
        if (xDiff > 10 ||  yDiff > 10){
            packagePosition = packageObject.transform.position;
            transform.position = packageObject.transform.position + new Vector3 (0,0,-6f);
            transform.GetComponent<Camera>().orthographicSize = 20;          
            StartCoroutine(Wait());
            transform.GetComponent<Camera>().orthographicSize = 10;
        } else {
            transform.position = playerObject.transform.position + new Vector3 (0,0,-6f);
        }
    }

    IEnumerator Wait(){
        enabled = false;
        yield return new WaitForSeconds(2);
        enabled = true;
    }
}
