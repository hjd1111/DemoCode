using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(menuName ="CreatManagerVarsContainer")]
public class Manager : ScriptableObject
{
    public static Manager GetManagerVars()
    {
        return Resources.Load<Manager>("ManagerVars");
    }
    public Sprite Unlock;
    public Sprite[] TurretUp;
    public Sprite[] TurretFinal;

    public TurretData CanonData;
    public TurretData CatapultData;
    public TurretData MissileData;
    public TurretData BarracksData;

    public GameObject BuildEffect;
    public GameObject FadeCannon;
    public GameObject FadeCannonUp;
    public GameObject FadeCannonFinal;
    public GameObject FadeCatapult;
    public GameObject FadeCatapultUp;
    public GameObject FadeCatapultFinal;
    public GameObject FadeMissile;
    public GameObject FadeMissileUp;
    public GameObject FadeMissileFinal;
    public GameObject FadeBattacks;
    public GameObject FadeBattacksUp;
    public GameObject FadeBattacksFinal;


    public GameObject Spider;
    public GameObject IronMan;
    public GameObject NomalSoldier;
    public GameObject BetterSoldier;
    public GameObject Fire;


    public Texture2D CursorFire;
    public Texture2D CursorReinforcement;
    public Texture2D CursorTarget;
    

    public AudioClip canon;
    public AudioClip missile;
    public AudioClip catapult;
    public AudioClip soldierded;
    public AudioClip FireBall;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip rward;

    public Sprite SoundOn;
    public Sprite SoundOff;

    public string[] UpDescribe;
    public string[] FinalDescribe;
}
