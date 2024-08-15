
using System;
using System.Collections;
using Core.Core;
using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : Singleton<DialogManager>
{
    [Header("对话组件")]
    public GameObject dialogPanel;
    public TextMeshProUGUI text;
    public TextAnimatorPlayer textAnimPlayer;
    public TextAnimator textAnimator;
    public TextMeshProUGUI nameText;
    public Image dialogImage;
    private bool isDialoging;
    public bool canNext;
    [Header("测试用SO数据")]
    public DialogContent testDialogContent;

    [Header("头像")]
    public Sprite Old,Young;
    private bool isPlaying;
   [HideInInspector]
    [Header("是否是小孩")] 
    public bool isYoung;
    
    [Header("提示用SO数据")] public DialogContent nothingDialogContent,
        notEnoughRiceDialogContent,
        haveFornitureDialogContent,
        perfectDialogContent,
        winDialogContent,
        loseDialogContent;
    protected override void Awake()
    {
        base.Awake();
        textAnimPlayer = text.gameObject.GetComponent<TextAnimatorPlayer>();
        textAnimator = text.gameObject.GetComponent<TextAnimator>();
        AudioSource audioSource = new AudioSource();
    }

    private void Start()
    {
        dialogImage.gameObject.SetActive(false);
        nameText.text = "";
        text.text = "";
    }

    private void OnEnable()
    {
        textAnimator.onEvent += OnEvent;
        EventHandler.OnGameStop += OnGameStop;
        EventHandler.OnGameContinue += OnGameContinue;
        EventHandler.OnSceneLoadBegin += OnSceneLoadBegin;
        EventHandler.OnSceneLoadEnd += OnSceneLoadEnd;
    }
    
    private void OnDisable()
    {
        textAnimator.onEvent -= OnEvent;
        EventHandler.OnGameStop -= OnGameStop;
        EventHandler.OnGameContinue -= OnGameContinue;
        EventHandler.OnSceneLoadBegin -= OnSceneLoadBegin;
        EventHandler.OnSceneLoadEnd -= OnSceneLoadEnd;
    }

    private void OnSceneLoadEnd(string obj)
    {
        canSkip = false;
    }

    private void OnSceneLoadBegin(string obj)
    {
        canSkip = true; 
        ExitDialog();
    }

    private bool isStop=false;
    private void OnGameContinue()
    {
        isStop = false;
    }

    private void OnGameStop()
    {
        isStop = true;
    }

    void OnEvent(string message)
    {
        switch (message)
        {
            case"BeginGame":
                Debug.Log(message);
                ExitDialog();
                EventHandler.CallOnGameBegin(GameManager.Instance.GameCountTime);  
               // StopAllCoroutines();
                break;
            case"StopGame":
                Debug.Log(message);
                ExitDialog();
                TimeCountManager.Instance.TotalTime = 0;
                break;
            case "NextLevel":
                StartCoroutine(NextLevelAction());
                break;
            case "ReloadLevel":
                StartCoroutine(ReloadLevelAction());
                break;
            case "MoreGrain":
                //TODO:加米
                var riceCreator=FindObjectOfType<RiceCreator>();
                riceCreator.CreateRice(riceCreator.addRiceCount);
                break;
            case "Continue":
                
                break;
        }
    }

    IEnumerator ReloadLevelAction()
    {
        ExitDialog(); 
        FindObjectOfType<LevelSceneLoad>().LoadNextLevel();
        yield return null;
    }

    IEnumerator NextLevelAction()
    {
        ExitDialog();
        FindObjectOfType<LevelSceneLoad>().LoadNextLevel();
        yield return null;
    }

    #region 能否进行下一对话函数
    public void SetCanNextTrue(bool next)
    {
        canNext = next;
    }
    #endregion

    public void  BeginDialog(DialogContent content)
    {
        StartCoroutine(StartDialog(content));
    }

    public bool canSkip;

    public void CanSkip()
    {
        canSkip = true;
    }

    public IEnumerator StartDialog(DialogContent content)
    {
        text.text = "";
        dialogImage.gameObject.SetActive(false);
        nameText.text = "";
        dialogPanel.SetActive(true);
        GameManager.Instance.DetectRice();
        if (GameManager.Instance.NowGameState==GameState.Playing)
        {
            UIManager.Instance.skipButton.SetActive(false);
        }
        else
        {
            UIManager.Instance.skipButton.SetActive(true);
        }
        dialogPanel.GetComponent<Animator>().Play("GoIn");
        //对话开始，呼叫所有其他管理层
        isDialoging = true;
        canNext = true;
        yield return new WaitForSeconds(1f);
        yield return null;
        EventHandler.CallOnDialog(isDialoging);
        Debug.Log("对话开始");
        yield return null;

        #region 对传空判断(点击人物状态判断或者获胜失败判断，前者游戏中，后者其他情况)

        if (content == null && GameManager.Instance.NowGameState == GameState.Playing)
        {
            switch (GameManager.Instance.NowDectectState)
            {
                case DectectState.Nothing:
                    content = nothingDialogContent;
                    break;
                case DectectState.NotEnoughRice:
                    content = notEnoughRiceDialogContent;
                    break;
                case DectectState.HaveForniture:
                    content = haveFornitureDialogContent;
                    break;
                case DectectState.Perfect:
                    content = perfectDialogContent;
                    break;
             
            }
            isPlaying = true;
        }
        else if(content==null&& GameManager.Instance.NowGameState != GameState.Playing)
        {
            if (GameManager.Instance.NowDectectState == DectectState.Perfect)
                content = winDialogContent;
            else
                content = loseDialogContent;
            isPlaying = false;
        }
        else
        {
            isPlaying=false;
        }

        #endregion
        
        //对话细节
        int i = 0;
        while (i<content.dialogList.Count)
        {
            yield return null;
            if(Input.anyKeyDown||canSkip)
                Skip();
            if (!canNext|| isStop)
                continue;
            if (i == 0 || Input.anyKeyDown||canSkip)
            {
                PlayWord(content.dialogList[i]);
                i++;
            }
        }
        yield return null;
        
        while (!Input.anyKeyDown||canSkip)
        {
            yield return null;
        }
        
        if (GameManager.Instance.NowGameState == GameState.Animation)
        {
            if (GameManager.Instance.NowDectectState == DectectState.Perfect)
            {
                FindObjectOfType<LevelSceneLoad>().JudgyAndSwitchLevel();
                ExitDialog();
            }
            else
            {
                FindObjectOfType<LevelSceneLoad>().JudgyAndSwitchLevel();
                ExitDialog();
            }
        }
        else if(GameManager.Instance.NowGameState==GameState.Playing)
            ExitDialog();

    }

    public void ExitDialog()
    {
        StopAllCoroutines();
        //对话结束，呼叫所有管理层
        isDialoging = false;
        EventHandler.CallOnDialog(isDialoging);
        Debug.Log("对话结束");
        text.text = "";
        //TODO:对话框移出
        dialogPanel.GetComponent<Animator>().Play("GoOut");
        canSkip = false;
    }

    public void PlayWord(Dialog dialog)
    {
        canNext = false;
        if(dialog.dialogImage==null&& !isPlaying)
            dialogImage.gameObject.SetActive(false);
        else if (isPlaying)
        {
            dialogImage.gameObject.SetActive(true);
            if (!isYoung)
            {
                dialogImage.sprite = Old;
            }
            else
            {
                dialogImage.sprite = Young;
            }
        }
        else
        {
            dialogImage.gameObject.SetActive(true);
            dialogImage.sprite = dialog.dialogImage;
        }
        if(dialog.dialogName=="")
            nameText.text = "";
        else if(!isPlaying)
            nameText.text = dialog.dialogName;
        else
        {
            if(!isYoung) 
                nameText.text = "Old Farmer";
            else
                nameText.text = "Young";
        }
        textAnimPlayer.ShowText(dialog.dialogText);
    }
    public void Skip()
    {
        textAnimPlayer.SkipTypewriter();
    }
    
    
}
