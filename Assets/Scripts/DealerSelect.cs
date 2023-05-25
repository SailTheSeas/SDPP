using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DealerSelect : NetworkBehaviour
{
    GameObject dealer;
    GameObject playerView;
    // Start is called before the first frame update
    void Start()
    {
        //dealer = this.transform.GetChild(1).GetChild(1).gameObject;
       // dealer.SetActive(false);
    }
    public override void OnStartAuthority()
    {

        playerView = this.transform.GetChild(1).gameObject;
        playerView.SetActive(true);
        if (this.gameObject.CompareTag("Player1"))
        {
            dealer = this.transform.GetChild(1).GetChild(1).gameObject;
            dealer.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
