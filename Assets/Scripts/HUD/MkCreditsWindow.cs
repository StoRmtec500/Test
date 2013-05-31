using UnityEngine;
using System.Collections;

public class MkCreditsWindow : MkMenuWindow {

    public static MkCreditsWindow instance;
    public float scrollPosition = -100f;
    public float maxScrollHeight = 100f;
    public GUIStyle headlineStyle;

    public void Awake()
    {
        MkCreditsWindow.instance = this;
    }

    public override void Update()
    {
        base.Update();
        if (!this.showWindow)
            return;
        this.scrollPosition += Time.deltaTime * 15f;
        if ((double)this.scrollPosition <= (double)this.maxScrollHeight)
            return;
        this.scrollPosition = -100f;
    }

    public override void drawWindowContent(int width, int height)
    {
        GUI.Label(new Rect(88f, 48f, (float)width, 30f), "Credits", this.headlineStyle);
        GUI.BeginGroup(new Rect(100f, 100f, 400f, 200f));
        GUI.Label(new Rect(0.0f, -this.scrollPosition, 581f, this.maxScrollHeight), MkTextProvider.getTextFromId("credits_content"));
        this.maxScrollHeight = this.headlineStyle.CalcHeight(new GUIContent(MkTextProvider.getTextFromId("credits_content")), 581f);
        GUI.EndGroup();

        if (!GUI.Button(new Rect(200f, 200f, 40f, 40f), "Ende"))
            return;
        this.showWindow = false;
    }
}
