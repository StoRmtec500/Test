using UnityEngine;
using System.Collections;

public class MkMenuWindow : MonoBehaviour {

    public static bool menuActive;
    public GUIStyle bgStyle;
    public bool showWindow;
    protected float xPosition;
    protected float yPosition;
    protected float xPositionState;
    private Vector2 windowDimension;
    

    static MkMenuWindow()
    {
    }

	// Use this for initialization
    public virtual void Start() 
    {
        MkMenuWindow.menuActive = false;
        this.xPosition = (float)Screen.width;
        this.windowDimension = new Vector2((float)this.bgStyle.normal.background.width, (float)this.bgStyle.normal.background.height);
	}
	
	// Update is called once per frame
    public virtual void Update()
    {
        this.xPositionState = Mathf.Clamp01(this.xPositionState + (!this.showWindow ? -Time.deltaTime : Time.deltaTime));
        this.xPosition = (float)Screen.width - (float)((double)Screen.width * 0.5 + (double)this.windowDimension.x * 0.5);
        this.yPosition = (float)((double)Screen.height * 0.5 - (double)this.windowDimension.y * 0.5);
        MkMenuWindow.menuActive = MkMenuWindow.menuActive | this.showWindow;
    }

    public void LateUpdate()
    {
        MkMenuWindow.menuActive = false;
    }

    public void OnGUI()
    {
        if (this.showWindow == true)
        {
            GUI.depth = 2;
            Rect position = new Rect(this.xPosition, this.yPosition, this.windowDimension.x, this.windowDimension.y);
            GUI.Label(position, string.Empty, this.bgStyle);
            if ((double)this.xPositionState < 0.0500000007450581)
                return;
            GUI.BeginGroup(position);
            this.drawWindowContent((int)position.width, (int)position.height);
            GUI.EndGroup();
        }
    }

    public virtual void drawWindowContent(int width, int height)
    {

    }
}
