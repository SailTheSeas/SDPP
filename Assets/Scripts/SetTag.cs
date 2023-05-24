using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTag : MonoBehaviour
{
    public string player = "Player1";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Player1").Length > 0)
        {
            player = "Player2";
        }
        this.gameObject.tag = player;
    }
}
