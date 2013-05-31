using UnityEngine;
using System.Collections;

public class MkOptionWindow : MkMenuWindow {

    public static MkOptionWindow instance;
    public float scrollPosition = -100f;
    public float maxScrollHeight = 100f;
    public GUIStyle headlineStyle;
    public Animation exit;

    public void Awake()
    {
        MkOptionWindow.instance = this;
    }

    public override void Update()
    {
        base.Update();
        if (!this.showWindow)
            return;
       // this.scrollPosition += Time.deltaTime * 15f;
        //if ((double)this.scrollPosition <= (double)this.maxScrollHeight)
        //    return;
        //this.scrollPosition = -100f;
    }

    public override void drawWindowContent(int width, int height)
    {
        GUI.Label(new Rect(10f, 48f, (float)width, 100f), MkTextProvider.getTextFromId("show_options_menu"), this.headlineStyle);
        GUI.BeginGroup(new Rect(100f, 100f, 400f, 500f));
        GUI.Label(new Rect(0.0f, -this.scrollPosition, 581f, this.maxScrollHeight), "Optionen");
       // this.maxScrollHeight = this.headlineStyle.CalcHeight(new GUIContent("credits_content"), 581f);
        GUI.EndGroup();

        if (!GUI.Button(new Rect(200f, 200f, 40f, 40f), "Ende"))
            return;
        this.showWindow = false;
    }
}
