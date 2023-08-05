using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[DisallowMultipleComponent]
public class RageRandomComposite : Composite
{
    public override async Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {
        int currentRageValue = agent.GetComponent<EnemyMonster>().rageValue;
        int rndMove = Random.Range(0, 101);
        //Debug.Log("rndMove = " + rndMove);

        int firstMove = 0;
        int secondMove = 0;
        // i dont need int thirdMove = 0 

        int child = 0;

        if(currentRageValue >= 0 && currentRageValue <= 20)
        {
            firstMove = 70;
            secondMove = 90;
        }
        else if (currentRageValue >= 21 && currentRageValue <= 40)
        {
            firstMove = 60;
            secondMove = 85;
        }
        else if (currentRageValue >= 41 && currentRageValue <= 60)
        {
            firstMove = 50;
            secondMove = 80;
        }
        else if (currentRageValue >= 61 && currentRageValue <= 80)
        {
            firstMove = 20;
            secondMove = 65;
        }
        else if (currentRageValue >= 81 && currentRageValue < 100)
        {
            firstMove = 10;
            secondMove = 60;
        }
        else if (currentRageValue == 100)
        {
            firstMove = 0;
            secondMove = 40;
        }

        if(rndMove >= 0 && rndMove <= firstMove)
        {
            //Debug.Log("PORCODIO 1");
            // first move
            child = 0;
        }
        if (rndMove > firstMove && rndMove <= secondMove)
        {
            //Debug.Log("PORCODIO 2");
            // second move
            child = 1;
        }
        if (rndMove > secondMove)
        {
            //Debug.Log("PORCODIO 3");
            // third move
            child = 2;
        }

        return await children[child].Run(agent, blackboard);
    }
}
