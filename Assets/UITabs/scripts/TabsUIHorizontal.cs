using UnityEngine;
using UnityEngine.UI;
using EasyUI.Tabs;
using Unity.VisualScripting;
using DG.Tweening.Core.Easing;
using EnhancedUI.EnhancedScroller;

public class TabsUIHorizontal : TabsUI
{
    #if UNITY_EDITOR
    private void Reset() {
        OnValidate();
    }
    private void OnValidate() {
        base.Validate(TabsType.Horizontal);
    }
#endif
    private static TabsUIHorizontal instance;
    public static TabsUIHorizontal Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TabsUIHorizontal>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    //public NumberLevelManager numberLevelManager;
    //public GameObject NumberLevel;

    public GameObject NewPanelLevel;
    public GameObject PreTarget;
    //public EnhancedScrollerDemos.GridSelection.ControllerScrollview controllerScrollview;
    public EnhancedScroller enhancedscroller_Level;
    //private void Awake()
    //{
    //    //numberLevelManager = transform.GetComponent<NumberLevelManager>();
    //    OnTabButtonClicked2(0);
    //}

    private void OnEnable()
    {
        //tabContent[current].GetComponent<TabContain>().Setupunder();
        //GetIndexJumpLevel(current);
        ////numberLevelManager.ActiveLoadLevel(current);
        //controllerScrollview.MaxCellLevel = GetMaxCell(current);
        ////GetIndexJumpLevel(tabIndex);
        //PreTarget = tabContent[current];
    }
    // private void Awake() {
    //      GetTabBtns();
        
    //     OnTabButtonClicked2(0);
    // }

    private void Start()
    {
        GetTabBtns();
        OnTabButtonClicked2(0);
    }

    public override void OnTabButtonClicked(int tabIndex)
    {
        //Debug.Log("chienthagn1");
      
        if (current != tabIndex)
        {
            if (OnTabChange != null)
                OnTabChange.Invoke(tabIndex);
            //if (controllerScrollview.transform.parent.gameObject.activeInHierarchy)
            //{
            //    controllerScrollview.transform.parent.gameObject.SetActive(false);
            //    //Debug.Log(controllerScrollview.transform.parent.gameObject.name);
            //}
            

            previous = current;
            current = tabIndex;

            tabContent[previous].SetActive(false);
            tabContent[current].SetActive(true);

            tabBtns[previous].uiImage.color = tabColorInactive;
            tabBtns[current].uiImage.color = tabColorActive;

            tabBtns[previous].uiButton.interactable = true;
            tabBtns[current].uiButton.interactable = false;

          //  controllerScrollview.MaxCellLevel = GetMaxCell(tabIndex);

           
            GetIndexJumpLevel(tabIndex);


            tabBtns[previous].uiButton.GetComponent<TabButtonUI>().DisActive();
            tabBtns[current].uiButton.GetComponent<TabButtonUI>().Active();
            //tabContent[current].GetComponent<TabContain>().SetUp();

            //PreTarget = tabContent[current];
            ////Debug.Log(tabBtns[current].gameObject.name);
            //switch (tabBtns[current].gameObject.name)
            //{
            //    case "easy":
            //        Controller.Instance.lastscreendependentLevel = "tab1";
            //        break;
            //    case "medium":
            //        Controller.Instance.lastscreendependentLevel = "tab2";
            //        break;
            //    case "hard":
            //        Controller.Instance.lastscreendependentLevel = "tab3";
            //        break;
            //}

        }
    }

    public void OnTabButtonClicked2(int tabIndex)
    {
        if (OnTabChange != null)
            OnTabChange.Invoke(tabIndex);
        //if (controllerScrollview.transform.parent.gameObject.activeInHierarchy)
        //{
        //    Debug.Log(controllerScrollview.transform.parent.gameObject.name);
        //    controllerScrollview.transform.parent.gameObject.SetActive(false);
        //}     
        
        previous = current;
        current = tabIndex;

        tabContent[previous].SetActive(false);
        tabContent[current].SetActive(true);

        tabBtns[previous].uiImage.color = tabColorInactive;
        tabBtns[current].uiImage.color = tabColorActive;

        tabBtns[previous].uiButton.interactable = true;
        tabBtns[current].uiButton.interactable = false;

        //numberLevelManager.ActiveLoadLevel(tabIndex);
        //controllerScrollview.MaxCellLevel = GetMaxCell(tabIndex);

       
        GetIndexJumpLevel(tabIndex);


        tabBtns[previous].uiButton.GetComponent<TabButtonUI>().DisActive();
        tabBtns[current].uiButton.GetComponent<TabButtonUI>().Active();
       // tabContent[current].GetComponent<TabContain>().SetUp();

        PreTarget = tabContent[current];
        ////numberLevelManager.gameObject.SetActive(false);
        ////controllerScrollview.transform.parent.gameObject.SetActive(false);
        ////Debug.Log(tabBtns[current].gameObject.name);
        //switch (tabBtns[current].gameObject.name)
        //{
        //    case "easy":
        //        Controller.Instance.lastscreendependentLevel = "tab1";
        //        break;
        //    case "medium":
        //        Controller.Instance.lastscreendependentLevel = "tab2";
        //        break;
        //    case "hard":
        //        Controller.Instance.lastscreendependentLevel = "tab3";
        //        break;
        //}
        ////Controller.Instance.lastscreendependentLevel = tabBtns[current].gameObject.name;
        ////TabsUIHorizontal.Instance.NewPanelLevel.gameObject.SetActive(false);
        ////TabsUIHorizontal.Instance.NewPanelLevel.gameObject.SetActive(true);
    }

    public int GetMaxCell(int level)
    {
        //if (level == 0)
        //{
        //    Controller.Instance.DiffirentGame = DiffirentEnum.EASY;
        //}
        //else if (level == 1)
        //{
        //    Controller.Instance.DiffirentGame = DiffirentEnum.MEDIUM;
        //}
        //else if (level == 2)
        //{
        //    Controller.Instance.DiffirentGame = DiffirentEnum.HARD;
        //}        
        //return Controller.Instance.constantsDiffical[Controller.Instance.DiffirentGame];
        return 1;
    }

    public void GetIndexJumpLevel(int level)
    {       
        //int m = 0;
        //if (level == 0)
        //{      
        //    m = (int)Mathf.Floor(PlayerPrefs.GetInt("Easylevel") / 3);
        //    if (PlayerPrefs.GetInt("Easylevel") % 3 != 0)
        //    {

        //    }
        //    else
        //    {
        //        if (m > 0)
        //        {
        //            m -= 1;
        //        }
        //    }
        //}
        //else if (level == 1)
        //{
        //    m = (int)Mathf.Floor((PlayerPrefs.GetInt("Mediumlevel") - Controller.Instance.constantsDiffical[DiffirentEnum.EASY]) / 3);
        //    if ((PlayerPrefs.GetInt("Mediumlevel") - Controller.Instance.constantsDiffical[DiffirentEnum.EASY]) % 3 != 0)
        //    {

        //    }
        //    else
        //    {
        //        if (m > 0)
        //        {
        //            m -= 1;
        //        }
        //    }
        //}
        //else if (level == 2)
        //{
        //    m = (int)Mathf.Floor((PlayerPrefs.GetInt("Hardlevel") - (Controller.Instance.constantsDiffical[DiffirentEnum.EASY]+ Controller.Instance.constantsDiffical[DiffirentEnum.MEDIUM])) / 3);
        //    if ((PlayerPrefs.GetInt("Hardlevel") - (Controller.Instance.constantsDiffical[DiffirentEnum.EASY] + Controller.Instance.constantsDiffical[DiffirentEnum.MEDIUM])) % 3 != 0)
        //    {

        //    }
        //    else
        //    {
        //        if (m > 0)
        //        {
        //            m -= 1;
        //        }
        //    }
        //}
        //enhancedscroller_Level.jumpdesire = m;
        ////enhancedscroller_Level.JumpHieu();
    }
}
