using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeNetwork : JoinRoom {

    // Use this for initialization
    void Start () {
        player = (GameObject) Instantiate(Resources.Load("PlayerObject", typeof(GameObject)), SpawnPoint.position, SpawnPoint.rotation);
        gc.NewPlayer(player.GetComponent<PlayerSync>().SetupReal());
        player.GetComponent<PlayerSync>().enabled = false;
    }
	
}
