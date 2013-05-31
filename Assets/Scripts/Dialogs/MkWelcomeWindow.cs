using UnityEngine;
using System.Collections;

public class MkWelcomeWindow : MonoBehaviour {

    public float fadeAlpha = 0.7f;
    public UIAtlas atlas;
    public UISprite _fadeBackground;
    public UILabel labelHeadline;

	// Use this for initialization
	void Start () 
    {
        string str1 = MkTextProvider.getTextFromId("last_site");
        UILabel component1 = labelHeadline;
        component1.text = str1;
        if (!(bool)((Object)this.atlas))
            return;
        this._fadeBackground = NGUITools.AddSprite(this.gameObject, this.atlas, "black");
        this._fadeBackground.depth = -1;
        this._fadeBackground.name = "FadeBackground";
        this._fadeBackground.alpha = this.fadeAlpha;
        this._fadeBackground.gameObject.SetActive(this.gameObject.activeSelf);
        this._fadeBackground.gameObject.AddComponent<BoxCollider>();
        this._fadeBackground.transform.localScale = new Vector3(100,200, 1f);
        this._fadeBackground.transform.localPosition = new Vector3(0.0f, 0.0f, -0.25f);
	}
}
