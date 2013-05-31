using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

public class MkTextProvider : MkXmlParser
{

    public static MkLanguage currentLanguage = MkLanguage.German;
    public static Dictionary<string, string> text = new Dictionary<string, string>();
    public string currentId = string.Empty;
    public static string[] languageShortcuts;
    public static List<string> textIds;

    static MkTextProvider()
    {
        string[] strArray = new string[2];
        int index1 = 0;
        string str1 = "ge";
        strArray[index1] = str1;
        int index2 = 1;
        string str2 = "en";
        strArray[index2] = str2;
        MkTextProvider.languageShortcuts = strArray;
        MkTextProvider.textIds = new List<string>();
    }

    public MkTextProvider(string pathToXmlFile)
        : base(pathToXmlFile)
    {
    }

    public static string getTextFromId(string id)
    {
        string key = id + "_" + MkTextProvider.languageShortcuts[(int)MkTextProvider.currentLanguage];
        if (MkTextProvider.text.ContainsKey(key))
            return MkTextProvider.text[key];
        object[] objArray = new object[4];
        int index1 = 0;
        string str1 = "Error";
        objArray[index1] = (object)str1;
        int index3 = 2;
        string str2 = ": ";
        objArray[index3] = (object)str2;
        int index4 = 3;
        string str3 = id;
        objArray[index4] = (object)str3;
        return string.Concat(objArray);
    }

    public static string[] getLanguage()
    {
        return MkTextProvider.languageShortcuts;
    }

    public override void openElement(string elementName, string parentElementName, Dictionary<string, string> attributes)
    {
        if (!(elementName == "entry"))
            return;
        this.currentId = attributes["key"];
    }

    public override void dataElement(string elementName, string parentElementName, string data, Dictionary<string, string> attributes)
    {
        for (int index = 0; index < MkTextProvider.languageShortcuts.Length; ++index)
        {
            if (elementName == MkTextProvider.languageShortcuts[index])
            {
                try
                {
                    MkTextProvider.textIds.Add(this.currentId);
                    MkTextProvider.text.Add(this.currentId + "_" + MkTextProvider.languageShortcuts[index], data);
                }
                catch (Exception ex)
                {
                   // Debug.Log((object)("Error while parsing Text Key: " + this.currentId + "_" + MkTextProvider.languageShortcuts[index]));
                }
            }
        }
    }

    public override void closeElement(string elementName)
    {
    }

    public static string[] getTextIds()
    {
        return MkTextProvider.textIds.ToArray();
    }
}

