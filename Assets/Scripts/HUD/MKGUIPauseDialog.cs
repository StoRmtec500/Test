using UnityEngine;
using System.Collections;

public class MKGUIPauseDialog : MonoBehaviour {

    private Rect drawAreaRect = new Rect(Screen.width / 2, Screen.height / 2, 500, 300);
    public float backgroundFadingSpeed = 50f;
    public float fadeOutValue = 6f;
    private float sizeFactor = 1f;
    public GUIStyle bgStyle;
    public GUIStyle labelStyle;
    public bool activate = false;
    public static MKGUIPauseDialog instance;
    public string Label1;
    private float xOffset;
    public float xOffsetDirection;
    private Vector2 backgroundImageSize;
    public string instructionText = "Das ist der erste Instruction Test";

    public enum Menu
    {
        Pause,InGame,GameOver
    }
    
    public Menu menuD;

    void Start()
    {
        MKGUIPauseDialog.instance = this;
       // Label1 = "Test";
       this.calculateBackgroundImage(Screen.width, Screen.height);
    }

    public void calculateBackgroundImage(int width, int height)
    {
        this.backgroundImageSize = new Vector2(1024, 768);
        this.xOffset = (float)(((double)width - (double)this.backgroundImageSize.x) * 0.5);
    }
    
    public void Update()
    {
        MkMenuWindow.menuActive = false;
        //this.xOffsetDirection = MkMenuWindow.menuActive || (double)Input.mousePosition.x >= (double)Screen.width * 0.25 ? (MkMenuWindow.menuActive || (double)Input.mousePosition.x <= (double)Screen.width * 0.75 ? Mathf.Lerp(this.xOffsetDirection, 0.0f, Time.deltaTime) : Mathf.Lerp(this.xOffsetDirection, -this.backgroundFadingSpeed, Time.deltaTime)) : Mathf.Lerp(this.xOffsetDirection, this.backgroundFadingSpeed, Time.deltaTime);
        this.xOffset = Mathf.Clamp(this.xOffset + this.xOffsetDirection * (Time.deltaTime * 40f / this.sizeFactor), (float)Screen.width - this.backgroundImageSize.x, 0.0f);
        updateMenu();
    }

    void OnGUI()
    {

        
        myGUIPauseMenu();
    }

    public void myGUIPauseMenu()
    {
        if (!activate == false)
        {
            Rect position = this.drawAreaRect;
            GUI.BeginGroup(position);
            GUI.Box(new Rect(0,0,500,300), string.Empty, this.bgStyle);
            GUI.Label(new Rect(0f, 0f, 20f, 10f), Label1, this.labelStyle);
            GUI.depth = 3;
            if (GUI.Button(new Rect(100, 100, 100, 100), "Credits") && !this.isWindowActive())
            {
                MkCreditsWindow.instance.showWindow = !MkCreditsWindow.instance.showWindow;
            }
            if (GUI.Button(new Rect(150, 150, 100, 100), "Optiones") && !this.isWindowActive())
            {
                MkOptionWindow.instance.showWindow = !MkOptionWindow.instance.showWindow;
                MkInGameGUI.showInfoBox(MkTextProvider.getTextFromId("gravel_pit_start"), false, 10f);
            }
            if(GUI.Button(new Rect(200,200,100,100),"Profile"))
            {
                MkUserInterface.instance.showScreen(MkUserInterface.MkScreenType.ProfileChooser);
            }
            GUI.EndGroup();
        }
    }

    public void updateMenu()
    {
        switch (menuD)
        {
            case Menu.Pause:
                Label1 = "Pause";
                break;
            case Menu.InGame:
                Label1 = "InGame";
                break;
        }
    }

    public bool isWindowActive()
    {
        if (!MkCreditsWindow.instance.showWindow && !MkOptionWindow.instance.showWindow)
            return false;
        else
            return true;
    }
}
