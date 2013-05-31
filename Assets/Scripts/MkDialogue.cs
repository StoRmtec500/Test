using UnityEngine;
using System.Collections;

public class MkDialogue : MonoBehaviour {

    private string dialogueText = string.Empty;
    private static MkDialogue instance;
    public GUIStyle applyButtonStyle;
    public GUIStyle cancleButtonStyle;
    public GUIStyle bgStyle;
    private bool dialogueEnabled;
    private MkDialogue.MkApplyCallback applyCallback;
    private MkDialogue.MkApplyCallback cancleCallback;
    private bool showCancleButton;
    private float dialogueHeigtState;
    private float dialogueHeight;
    private Vector2 dialogueDimension;
    private bool applyBtnDown;
    private bool cancleBtnDown;
    public bool escapeEnabled;

    public void Awake()
    {
        MkDialogue.instance = this;
        this.dialogueDimension = new Vector2((float)this.bgStyle.normal.background.width, (float)this.bgStyle.normal.background.height);
        this.dialogueHeight = -this.dialogueDimension.y;
        DontDestroyOnLoad(this);
    }

    public void Update()
    {
        this.dialogueHeigtState = Mathf.Clamp01(this.dialogueHeigtState + (!this.dialogueEnabled ? -Time.deltaTime : Time.deltaTime));
        //this.applyBtnDown = Input.GetButtonDown("Fire1");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escapeEnabled = true;
            if (escapeEnabled)
            {
                showDiag(MkTextProvider.getTextFromId("exit_game_question"));
            }
            else
            {
                MkDialogue.instance.dialogueEnabled = false;
            }
        }
    }

    public void OnGUI()
    {
        if (dialogueEnabled && escapeEnabled)
        {
            // if ((double)this.dialogueHeight == -(double)this.dialogueDimension.y)
            //     return;
            GUI.BeginGroup(new Rect(Screen.width / 2 - dialogueDimension.x / 2, Screen.height / 2 - dialogueDimension.y / 2, dialogueDimension.x, dialogueDimension.y));
            GUI.Label(new Rect(0.0f, 0.0f, this.dialogueDimension.x, this.dialogueDimension.y), this.dialogueText, this.bgStyle);
            if (GUI.Button(new Rect(219f, 149f, 100f, 40f), MkTextProvider.getTextFromId("yes"), applyButtonStyle) || this.applyBtnDown)
            {
                this.applyBtnDown = false;
                MkDialogue.instance.dialogueEnabled = false;
                if (this.applyCallback != null)
                   this.applyCallback();
            }
            GUI.EndGroup();
        }
    }

    public static void showDialogue(string text, MkDialogue.MkApplyCallback applyCallback, MkDialogue.MkApplyCallback cancelCallback, bool showCancleButton)
    {
        MkDialogue.instance.dialogueEnabled = true;
        MkDialogue.instance.applyCallback = applyCallback;
        MkDialogue.instance.showCancleButton = showCancleButton;
        MkDialogue.instance.dialogueText = text;
    }

    public void showDiag(string text)
    {
        MkDialogue.instance.dialogueEnabled = true;
        MkDialogue.instance.dialogueText = text;
        Application.Quit();
    }

    public static bool isDialogueEnabled()
    {
        return MkDialogue.instance.dialogueEnabled;
    }

    public delegate void MkApplyCallback();

    public delegate void MkCancleCallback();
}
