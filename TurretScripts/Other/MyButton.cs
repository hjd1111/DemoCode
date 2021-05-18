using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyButton : Button
{
    private GameObject txt_Describe;
    protected override void DoStateTransition(SelectionState state, bool instant)

    {

        base.DoStateTransition(state, instant);

        switch (state)

        {

            case SelectionState.Disabled:
                break;

            case SelectionState.Highlighted:
                txt_Describe = transform.Find("Describe").gameObject;
                txt_Describe.SetActive(true);
                switch (transform.name)
                {
                    case "Cannon":
                        EventCenter.Broadcast<int>(EventDefine.ShowFateTurret, 1);
                        break;
                    case "Catapult":
                        EventCenter.Broadcast<int>(EventDefine.ShowFateTurret, 2);
                        break;
                    case "Missile":
                        EventCenter.Broadcast<int>(EventDefine.ShowFateTurret, 3);
                        break;
                    case "Barracks":
                        EventCenter.Broadcast<int>(EventDefine.ShowFateTurret, 4);
                        break;
                    case "Up":
                        EventCenter.Broadcast<int>(EventDefine.ShowFateTurret, 5);
                        break;
                    default:
                        break;
                }
                break;

            case SelectionState.Normal:
                //Debug.Log("wssb");
                if (txt_Describe != null)
                {
                    txt_Describe.SetActive(false);
                }
                EventCenter.Broadcast(EventDefine.DestoryFadeTurret);
                break;

            case SelectionState.Pressed:

                if (txt_Describe != null)
                {
                    txt_Describe.SetActive(false);
                }
                break;

            default:

                break;

        }

    }

}
