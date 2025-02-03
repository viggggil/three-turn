using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject selected;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = selected.transform.position;
    }
    public void ChangeSelected(GameObject x)
    {
        selected = x;
    }
}
