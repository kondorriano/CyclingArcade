using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public bool useNetwork = true;
    public Transform tempMainCamera;
    //public Transform tempStage;//OLD

    JoinRoom networking;
    PlayerScript player;

    [HideInInspector]
    public BezierSpline stage;
    [HideInInspector]
    public List<float> curveLenght;



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
            GameObject networkClone = (GameObject) Instantiate(Resources.Load((useNetwork) ? "Networking" : "FakeNetworking", typeof(GameObject)));
            networkClone.transform.SetParent(transform, true);
            networking = networkClone.GetComponent<JoinRoom>();
            networking.Init(this);

            GameObject stageClone = (GameObject)Instantiate(Resources.Load("StageSpline", typeof(GameObject)));
            stageClone.transform.SetParent(transform, true);
            stage = stageClone.GetComponent<BezierSpline>();

            //CurveLenght
            for (int i = 0; i < stage.CurveCount; ++i)
            {
                float lenght = 0;
                Vector3 firstPos = stage.GetCurvePoint(i, 0);
                Vector3 secondPos;
                for(int j = 1; j <= 10; ++j)
                {
                    secondPos = stage.GetCurvePoint(i, j*.1f);
                    lenght += (firstPos - secondPos).magnitude;
                    firstPos = secondPos;
                }
                curveLenght.Add(lenght);
                Debug.Log("Curve " + i + ": " +lenght);
            }

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
