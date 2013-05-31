using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Net.NetworkInformation;

public class MkSerial : MonoBehaviour {

    public string nextSceneName = string.Empty;
    public string serial = string.Empty;
    public string serialState = string.Empty;
    private string serialBlock1 = string.Empty;
    private string serialBlock2 = string.Empty;
    private string serialBlock3 = string.Empty;
    private string serialBlock4 = string.Empty;
    private string hashBlock = string.Empty;
    public bool showSerialUi;
    public bool enableSerialUI = true;
    public bool showPhoneActivation;
    private bool phoneActivationError;
    public string headlineText = "Bist du bereit zum starten?";
    public string phoneActivationText = "Telefonaktivierung";
    public string offlineText = "Du bist Offline";
    public string enterSerialText = "Bitte gib die Seriennummer ein!";
    public string checkText = "Eingabe Korrekt";
    public string phoneInfoText = "Bitte Kontaktieren sie den Support!";
    public string phoneErrorText = "Ein unbekannter Fehler ist aufgetreten!";
    public GUIStyle bgStyle;
    public GUIStyle headlineStyle;
    public GUIStyle stateStyle;
    public GUIStyle inputGroupStyle;
    public Texture iconNet;
    public Texture iconPhone;

	// Use this for initialization
	void Start () 
    {
        if (this.checkSerial())
        {
            this.continueGame();
        }
        else
        {
            this.showSerialUi = true;
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                this.enableSerialUI = true;
                this.serialState = this.enterSerialText;
            }
            else
            {
                this.enableSerialUI = false;
                this.serialState = this.offlineText;
                this.showPhoneActivation = true;
            }
        }
	}

    private void PopulateString()
    {

    }

    private void continueGame()
    {
        UnityEngine.Debug.Log((object)"Continue Game");
        Application.LoadLevel(this.nextSceneName);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnGUI()
    {
        if (!this.showSerialUi)
            return;
        float height = !this.showPhoneActivation ? 300f : 300f;
        GUI.BeginGroup(new Rect((float)((double)Screen.width * 0.5 - 320.0), (float)((double)Screen.height * 0.5 - (double)height * 0.5), 640f, height));
        GUI.Label(new Rect(0.0f,0.0f,640f, height), string.Empty, this.bgStyle);
        if (!this.showPhoneActivation)
        {
            GUI.Label(new Rect(20f, 20f, 360f, 40f), this.headlineText, headlineStyle);
            GUI.enabled = this.enableSerialUI;
            this.serialBlock1 = GUI.TextField(new Rect(30f, 90f, 80f, 40f), this.serialBlock1, this.inputGroupStyle);
            this.serialBlock1 = this.serialBlock1.Substring(0, Mathf.Min(4, this.serialBlock1.Length)).Trim().ToUpper();
            this.serialBlock2 = GUI.TextField(new Rect(120f, 90f, 80f, 40f), this.serialBlock2, this.inputGroupStyle);
            this.serialBlock2 = this.serialBlock2.Substring(0, Mathf.Min(4, this.serialBlock2.Length)).Trim().ToUpper();
            this.serialBlock3 = GUI.TextField(new Rect(210f, 90f, 80f, 40f), this.serialBlock3, this.inputGroupStyle);
            this.serialBlock3 = this.serialBlock3.Substring(0, Mathf.Min(4, this.serialBlock3.Length)).Trim().ToUpper();
            this.serialBlock4 = GUI.TextField(new Rect(300f, 90f, 80f, 40f), this.serialBlock4, this.inputGroupStyle);
            this.serialBlock4 = this.serialBlock4.Substring(0, Mathf.Min(4, this.serialBlock4.Length)).Trim().ToUpper();
            GUI.enabled = this.enableSerialUI && 16 == this.serialBlock1.Length + this.serialBlock2.Length + this.serialBlock3.Length + this.serialBlock4.Length;
            if (GUI.Button(new Rect(410f, 90f, 200f, 40f), this.checkText))
                Debug.Log("Download Serialinfo");
            GUI.enabled = true;
            if (this.serialState != string.Empty)
                GUI.Label(new Rect(20f, 60f, 400f, 40f), this.serialState, this.stateStyle);
        }
        else
        {
            GUI.Label(new Rect(20f, 20f, 360f, 40f), this.phoneActivationText, this.headlineStyle);
            this.serialBlock1 = GUI.TextField(new Rect(30f, 90f, 80f, 40f), this.serialBlock1, this.inputGroupStyle);
            this.serialBlock1 = this.serialBlock1.Substring(0, Mathf.Min(4, this.serialBlock1.Length)).Trim().ToUpper();
            this.serialBlock2 = GUI.TextField(new Rect(120f, 90f, 80f, 40f), this.serialBlock2, this.inputGroupStyle);
            this.serialBlock2 = this.serialBlock2.Substring(0, Mathf.Min(4, this.serialBlock2.Length)).Trim().ToUpper();
            this.serialBlock3 = GUI.TextField(new Rect(210f, 90f, 80f, 40f), this.serialBlock3, this.inputGroupStyle);
            this.serialBlock3 = this.serialBlock3.Substring(0, Mathf.Min(4, this.serialBlock3.Length)).Trim().ToUpper();
            this.serialBlock4 = GUI.TextField(new Rect(300f, 90f, 80f, 40f), this.serialBlock4, this.inputGroupStyle);
            this.serialBlock4 = this.serialBlock4.Substring(0, Mathf.Min(4, this.serialBlock4.Length)).Trim().ToUpper();
            this.hashBlock = GUI.TextField(new Rect(30f, 140f, 350f, 40f), this.hashBlock);
            this.hashBlock = this.hashBlock.Trim().ToUpper();
            GUI.enabled = this.enableSerialUI && this.serialBlock1.Length + this.serialBlock2.Length + this.serialBlock3.Length + this.serialBlock4.Length == 16 && this.hashBlock.Length > 0;
            if (GUI.Button(new Rect(410f, 90f, 200f, 40f), this.checkText))
            {
                this.serial = this.serialBlock1 + this.serialBlock2 + this.serialBlock3 + this.serialBlock4;
                PlayerPrefs.SetString("Serial", this.serial);
                if (this.checkSerial())
                {
                    this.showSerialUi = false;
                    this.continueGame();
                }
                else
                    this.phoneActivationError = true;
            }   
            GUI.enabled = true;
            Rect position = new Rect(20f, 125f, 450f, 40f);
            string[] strArray = new string[5];
            int index1 = 0;
            string str1 = !this.phoneActivationError ? this.phoneInfoText : this.phoneErrorText;
            strArray[index1] = str1;
            string text = string.Concat(strArray);
            GUI.Label(position,text);
        }
        if (GUI.Button(new Rect(570f, height - 60f, 40f, 40f), this.showPhoneActivation ? this.iconNet : this.iconPhone))
            this.showPhoneActivation = !this.showPhoneActivation;
        GUI.EndGroup();
    }

    private bool checkSerial()
    {
        return PlayerPrefs.HasKey("Serial");
    }

    [ContextMenu("Remove Serial PlayerPrefs")]
    public void removeSerialPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("Serial"))
            PlayerPrefs.DeleteKey("Serial");
    }
}
