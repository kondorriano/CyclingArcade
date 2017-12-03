using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Transform tempMainCamera;
    //public Transform tempStage;//OLD

    JoinRoom networking;
    PlayerScript player;

    [HideInInspector]
    public BezierSpline stage;
    //[HideInInspector]
    //public List<Transform> checkpoints;//OLD



    bool initialized = false;


    // Use this for initialization
    void Start () {
        RunGame();
	}

    void RunGame()
    {
        StartCoroutine(RunGameCoroutine());
    }

    IEnumerator RunGameCoroutine()
    {
        ClearGame();
        InitializePrefabs();

        yield return null;

    }

    void ClearGame()
    {
        //nothing yet
    }

    void InitializePrefabs()
    {
        if (!initialized)
        {
            GameObject networkClone = (GameObject) Instantiate(Resources.Load("Networking", typeof(GameObject)));
            networkClone.transform.SetParent(transform, true);
            networking = networkClone.GetComponent<JoinRoom>();
            networking.Init(this);

            GameObject stageClone = (GameObject)Instantiate(Resources.Load("StageSpline", typeof(GameObject)));
            stageClone.transform.SetParent(transform, true);
            stage = stageClone.GetComponent<BezierSpline>();

            /*foreach (Transform child in tempStage)
            {
                checkpoints.Add(child);
            }*/

            initialized = true;
        }
    }

    public void NewPlayer(GameObject newPlayer)
    {
        player = newPlayer.GetComponent<PlayerScript>();
        tempMainCamera.SetParent(player.transform, false);
        player.Init(this);    
    }
}
