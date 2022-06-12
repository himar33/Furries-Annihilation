using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitManager : Enemy
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void StopWalk()
    {
        agent.isStopped = true;
    }

    public void ContinueWalk()
    {
        agent.isStopped = false;
    }

    public void Steps()
    {

    }
}
