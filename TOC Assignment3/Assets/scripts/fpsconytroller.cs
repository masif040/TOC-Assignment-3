using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpsconytroller : MonoBehaviour
{
    // Start is called before the first frame update
    public enum States { NORMAL, WEAK, POWER, DEAD }
    public States currentState = States.NORMAL;

    public enum Action { CUBE, SYLENDER, CAPSULE }



    private GameObject controler = null;
    private Transform cube, cylender, capsule;
    void Start()
    {
        controler = GameObject.FindGameObjectWithTag("control");
        GameObject[] cubelist = GameObject.FindGameObjectsWithTag("cube");
        cube = cubelist[0].GetComponent<Transform>();
        GameObject[] capsulelist = GameObject.FindGameObjectsWithTag("capsule");
        capsule = capsulelist[0].GetComponent<Transform>();

        GameObject[] cylenderlist = GameObject.FindGameObjectsWithTag("cylender");
        cylender = cylenderlist[0].GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(controler.transform.position, (cube.position - controler.transform.
            position).normalized,
            out hit, 1))
        {
            currentState = checkstate(currentState, Action.CUBE);

        }
        if (Physics.Raycast(controler.transform.position, (cylender.position - controler.transform.
            position).normalized,
            out hit, 1))
        {

            currentState = checkstate(currentState, Action.SYLENDER);

        }
        if (Physics.Raycast(controler.transform.position, (capsule.position - controler.transform.
            position).normalized,
            out hit, 1))
        {

            currentState = checkstate(currentState, Action.CAPSULE);

        }
    }
    States checkstate(States currentState, Action action)
    {
        States afterresult = currentState;
        if (currentState == States.NORMAL)
        {
            if (action == Action.CAPSULE)
            {
                afterresult = States.POWER;
            }
            else if (action == Action.CUBE)
            {
                afterresult = States.NORMAL;
            }
            else if (action == Action.SYLENDER)
            {
                afterresult = States.WEAK;
            }
        }

        if (currentState == States.POWER)
        {
            if (action == Action.SYLENDER)
            {
                afterresult = States.NORMAL;
            }
            else if (action == Action.CUBE)
            {
                afterresult = States.POWER;
            }
            else if (action == Action.CAPSULE)
            {
                afterresult = States.POWER;
            }
        }
        if (currentState == States.WEAK)
        {
            if (action == Action.CAPSULE)
            {
                afterresult = States.NORMAL;
            }
            else if (action == Action.CUBE)
            {
                afterresult = States.DEAD;
            }
            else if (action == Action.SYLENDER)
            {
                afterresult = States.DEAD;
            }
        }


        return afterresult;

    }
}
