using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuCancvas : MonoBehaviour
{
    private Button Pause;
    private Button Continue;
    public Button Target;
    public Animator NoMoney;
    private MyButton NextWave;
    private MyButton ReinforcementsButton;
    private MyButton FireBallButton;

    
    private Text Heart;
    private Text TotalMoneyText;
    public EnemySpawner Espa;
    private Text DescribeEnemyWave;

    private bool isClickSkill;
    private bool isFire;
    private bool isRein;
    private bool isTarget;
    private bool isFirstWave = true;
    private bool isNext=true;
    
    
    public static MenuCancvas Instance;



    public int money = 1000;
    private int length;
    private int wavecount = 1;
    private Manager vars;
    private AudioSource Myaudio;


    private void Awake()
    {
        Instance = this;
        vars = Manager.GetManagerVars();
        Myaudio = transform.GetComponent<AudioSource>();
        EventCenter.AddListener(EventDefine.ReduceHeart, ReduceHeart);
        EventCenter.AddListener<int>(EventDefine.ChangeMoney, ChangeMoney);
        EventCenter.AddListener(EventDefine.NoMoney, IFNOMONEY);
        EventCenter.AddListener(EventDefine.isNext, IsNext);
        EventCenter.AddListener<bool>(EventDefine.isMusic, isMusic);
        Init();
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ReduceHeart, ReduceHeart);
        EventCenter.RemoveListener<int>(EventDefine.ChangeMoney, ChangeMoney);
        EventCenter.RemoveListener(EventDefine.NoMoney, IFNOMONEY);
        EventCenter.RemoveListener(EventDefine.isNext,IsNext);
        EventCenter.RemoveListener<bool>(EventDefine.isMusic, isMusic);
    }
    private void Start()
    {
        length = EnemySpawner.WavesLength;
        Continue.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()==false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isRoad = Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Road"));
                if (isRoad && isClickSkill)
                {
                    Vector3 screenPos = Camera.main.WorldToScreenPoint(new Vector3(0,0,0));
                    Vector3 mousePosOnScreen = Input.mousePosition;
                    mousePosOnScreen.z = screenPos.z;
                    Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePosOnScreen);

                    if (isRein)
                    {
                        mousePosInWorld.y = 0.5f;
                        GameObject Reminforcemer = Instantiate(vars.NomalSoldier, mousePosInWorld, Quaternion.identity);
                        ReinforcementsButton.GetComponentInParent<FillAmount>().iscount = true;
                        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                        isClickSkill = false;
                        isRein = false;
                    }
                    if (isFire)
                    {
                        mousePosInWorld.y = 1f;
                        GameObject FireBall = Instantiate(vars.Fire, mousePosInWorld, Quaternion.identity);
                        Myaudio.PlayOneShot(vars.FireBall);
                        FireBallButton.GetComponentInParent<FillAmount>().iscount = true;
                        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                        isClickSkill = false;
                        isFire = false;
                    }
                    if (isTarget)
                    {
                        mousePosInWorld.y = 1f;
                        EventCenter.Broadcast(EventDefine.MoveTarget, mousePosInWorld);
                        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                        isClickSkill = false;
                        isTarget = false;
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            isClickSkill = false;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        
    }
    private void Init()
    {
        Pause = transform.Find("Pause").GetComponent<Button>();
        Continue = transform.Find("Continue").GetComponent<Button>();

        ReinforcementsButton = transform.Find("Reinforcements/Image/ReinButton").GetComponent<MyButton>();
        FireBallButton = transform.Find("Fire/Image/FireButton").GetComponent<MyButton>();
        NextWave = transform.Find("NextWave").GetComponent<MyButton>();

        Pause.onClick.AddListener(OnPauseClick);
        Continue.onClick.AddListener(OnContinueClick);
        ReinforcementsButton.onClick.AddListener(OnReinforcementsClick);
        FireBallButton.onClick.AddListener(OnFireBallClick);
        NextWave.onClick.AddListener(OnNextWaveClick);
        Target.onClick.AddListener(OnTargetButtonClick);

        Heart = transform.Find("Heart/Text").GetComponent<Text>();
        TotalMoneyText = transform.Find("TotalMoney").GetComponent<Text>();
        DescribeEnemyWave = transform.Find("NextWave/Describe").GetComponent<Text>();
    }
    private void isMusic(bool value)
    {
        Myaudio.mute = value;
    }
    private void IsNext()
    {
        isNext = false;
    }
    private void ReduceHeart()
    {
        Heart.text = (int.Parse(Heart.text) - 1).ToString();
        Myaudio.PlayOneShot(vars.soldierded);
        if (int.Parse(Heart.text) <= 0)
        {
            //Debug.Log("wssb");
            Myaudio.PlayOneShot(vars.lose);
            EventCenter.Broadcast(EventDefine.ShowOver);
            EventCenter.Broadcast<int>(EventDefine.StarsNum, int.Parse(Heart.text.ToString()) / 6);
            EventCenter.Broadcast(EventDefine.Save);
        }
    }
    private void ChangeMoney(int change = 0)
    {
        money += change;
        TotalMoneyText.text = "￥" + money;
    }
    private void OnPauseClick()
    {
        Pause.gameObject.SetActive(false);
        Continue.gameObject.SetActive(true);
        EventCenter.Broadcast(EventDefine.ShowOver);
        EventCenter.Broadcast<int>(EventDefine.StarsNum, int.Parse(Heart.text.ToString()) / 6);
        Time.timeScale = 0;
    }
    private void OnContinueClick()
    {
        Pause.gameObject.SetActive(true);
        Continue.gameObject.SetActive(false);
        EventCenter.Broadcast(EventDefine.CloseOver);
        Time.timeScale = 1;
    }
    public void OnNextWaveClick()
    {
        if (isFirstWave)
        {
            InvokeRepeating("CheckForEnemy", 0, 3);
            isFirstWave = false;
        }
        else
        {
            ChangeMoney((int)(NextWave.GetComponent<NextWave>().Reward * 50));
            Myaudio.PlayOneShot(vars.rward);
        }

        DescribeEnemyWave.text = null;
        if (wavecount < EnemySpawner.WavesLength)
        {
            int ironcount = Espa.GetComponent<EnemySpawner>().waves[wavecount].IronmanCount;
            int spidercount = Espa.GetComponent<EnemySpawner>().waves[wavecount].SpiderCount;
            if (spidercount > 0) DescribeEnemyWave.text += (" Spider × " + spidercount+"\n").ToString();
            if (ironcount > 0) DescribeEnemyWave.text += ("Iron × " + ironcount).ToString();
            wavecount++;
        }

        NextWave.GetComponent<NextWave>().Timer = 10f;
        NextWave.gameObject.SetActive(false);
        EventCenter.Broadcast(EventDefine.StartWave);
        if (isNext)
        {
            StartCoroutine(HideNexWave());
        }
    }
    private void OnReinforcementsClick()
    {
        Cursor.SetCursor(vars.CursorReinforcement, Vector2.zero, CursorMode.Auto);
        isClickSkill = true;
        isRein = true;
    }
    private void OnFireBallClick()
    {
        Cursor.SetCursor(vars.CursorFire, Vector2.zero, CursorMode.Auto);
        isClickSkill = true;
        isFire = true;
    }
    private void OnTargetButtonClick()
    {
        Cursor.SetCursor(vars.CursorTarget, Vector2.zero, CursorMode.Auto);
        isClickSkill = true;
        isTarget = true;
    }
    private void CheckForEnemy()
    {
        if (length >= 0) return;
        if (EnemySpawner.CountEnemyAlive == 0)
        {
            length--;
            if (length <= 0)
            {
                Myaudio.PlayOneShot(vars.win);
                EventCenter.Broadcast(EventDefine.ShowOver);
                EventCenter.Broadcast<int>(EventDefine.StarsNum, int.Parse(Heart.text.ToString()) / 6);
                //Debug.Log(int.Parse(Heart.text.ToString()) / 6);
                
                bool heart = int.TryParse(Heart.text.ToString(), out int result1);
                bool name = int.TryParse(transform.parent.name.ToString(), out int result2);
                if (heart && name)
                {
                    //Debug.Log("wssb" + result1 + "  " + result2);
                    if (int.Parse(Heart.text.ToString()) / 6 > Datamanager.Instance.tempStarsNum[int.Parse(transform.parent.name) - 1])
                    {
                        Datamanager.Instance.tempStarsNum[int.Parse(transform.parent.name) - 1] = int.Parse(Heart.text) / 6;
                        EventCenter.Broadcast(EventDefine.Save);
                    }
                }
            }
        }
    }
    private void IFNOMONEY()
    {
        StartCoroutine(IfNoMoney());
    }
    IEnumerator IfNoMoney()
    {
        NoMoney.SetInteger("NoMoney", 1);
        yield return new WaitForSeconds(1.5f);
        NoMoney.SetInteger("NoMoney", 0);
    }
    IEnumerator HideNexWave()
    {
        yield return new WaitForSeconds(8f);
        NextWave.gameObject.SetActive(true);
        NextWave.GetComponent<NextWave>().isNextWave = true;
    }
}
