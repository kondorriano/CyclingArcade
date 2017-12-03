using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    CapsuleCollider col;
    public Animator anim;

    GameController gc;

    float cadence = 10;
    int gear = 1;

    int currentCurve = 0;
    float duration = 3f;
    float progress = 0;

    public void Init(GameController gameC)
    {
        gc = gameC;
        currentCurve = 0;
    }
    // Use this for initialization
    void Start()
    {
        col = GetComponent<CapsuleCollider>();
    }

    public void TeleportTo(Vector3 feetPos)
    {
        transform.position = feetPos + new Vector3(0, transform.localScale.y * col.height * .5f, 0);
    }

    float speedHorizontal = 3.0f;
    Vector3 dir;
    Vector3 position;
    float offsetY = 0;
    float offsetX = 0;

    // Update is called once per frame
    void Update()
    {
        //dir = Input.GetAxisRaw("Vertical1") * transform.forward;
        //dir += Input.GetAxisRaw("Horizontal1") * transform.right;
        //transform.Translate(dir * Time.deltaTime * speedHorizontal);

        float rv = Input.GetAxisRaw("RVertical1");
        float rh = Input.GetAxisRaw("RHorizontal1");
        float pv = Input.GetAxisRaw("PadVertical1");
        float ph = Input.GetAxisRaw("PadHorizontal1");

        bool xBut = Input.GetButtonDown("X1");
        bool aBut = Input.GetButtonDown("A1");
        bool bBut = Input.GetButtonDown("B1");
        bool yBut = Input.GetButtonDown("Y1");
        bool lBut = Input.GetButtonDown("L1");
        bool rBut = Input.GetButtonDown("R1");

        float lt = Input.GetAxisRaw("LTrigger1");
        float rt = Input.GetAxisRaw("RTrigger1");

        if (Mathf.Abs(rv) > .1f || Mathf.Abs(rh) > .1f)
            Debug.Log("RStick " + rv + " " + rh);

        if (pv != 0 || ph != 0)
            Debug.Log("Pad " + pv + " " + ph);

        if (xBut || aBut || bBut || yBut)
            Debug.Log("X " + xBut + " Y " + yBut + " A " + aBut + " B " + bBut);

        if (lBut || rBut)
        {
            Debug.Log("L " + lBut + " R " + rBut);
        }

        if (lt == 1 || rt == 1)
        {
            if(lt == 1) cadence -= 1 * Time.deltaTime;
            if(rt == 1) cadence += 1 * Time.deltaTime;
            cadence = Mathf.Clamp(cadence, 0, 10);
            //Debug.Log(cadence);
        }


        anim.speed = cadence;

        progress += Time.deltaTime / duration;
        if (progress > 1f)
        {
            progress = 0;
            ++currentCurve;
            if (currentCurve >= gc.stage.CurveCount) currentCurve = 0;
        }
        position = gc.stage.GetCurvePoint(currentCurve, progress);
        transform.Translate(position-transform.position, Space.World);
        transform.LookAt(position + gc.stage.GetCurveDirection(currentCurve, progress));
        /*
        position += dir.normalized * Time.deltaTime * cadence;
        if ((position-startCheckpoint.position).sqrMagnitude >= dir.sqrMagnitude)
        {
            newSection(((position - startCheckpoint.position).sqrMagnitude / dir.sqrMagnitude)-1f);
        }

        transform.Translate(position - transform.position,Space.World);
        transform.rotation = Quaternion.Lerp(startCheckpoint.rotation, endCheckpoint.rotation, ((position - startCheckpoint.position).sqrMagnitude / dir.sqrMagnitude));
        */
    }
    /*
    void newSection(float offset)
    {
        if (++checkPointIndex >= gc.checkpoints.Count) checkPointIndex = 0;
        startCheckpoint = gc.checkpoints[checkPointIndex].GetChild(0);
        endCheckpoint = gc.checkpoints[checkPointIndex].GetChild(1);
        float oldMagnitude = dir.sqrMagnitude;
        dir = endCheckpoint.position - startCheckpoint.position;
        position = Vector3.Lerp(startCheckpoint.position, endCheckpoint.position, offset*(oldMagnitude/dir.sqrMagnitude));
    }*/
}
