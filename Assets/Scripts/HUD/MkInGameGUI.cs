using UnityEngine;
using System.Collections;

public class MkInGameGUI : MonoBehaviour {

    public bool guiEnabled = true;
    public string placeName = string.Empty;
    private string instructionText = string.Empty;
    public static MkInGameGUI instance;
    public float instructionFadeOutValue;
    public float time;
    public bool timeActive;


    public void Awake()
    {
        MkInGameGUI.instance = this;
    }

	// Use this for initialization
	void Start () {
        this.timeActive = false;
	}
	
	// Update is called once per frame
	void Update () {
        this.instructionFadeOutValue = Mathf.Clamp(this.instructionFadeOutValue - Time.deltaTime, 0.0f, 1000f);
        if ((double)this.instructionFadeOutValue <= 0.0099999977648258)
            this.instructionText = string.Empty;
        this.time += Time.deltaTime;
	}

    public void OnGUI()
    {
        if (!this.guiEnabled)
            return;
        GUI.depth = 4;
        this.drawInstruction();
    }

    public void drawInstruction()
    {
        GUI.Label(new Rect(100, 700, 200, 100), this.instructionText);
    }

    public static void showInfoBox(string infoText, bool playSound, float time)
    {
        MkInGameGUI.instance.instructionText = infoText;
        MkInGameGUI.instance.instructionFadeOutValue = time;
    }
}
