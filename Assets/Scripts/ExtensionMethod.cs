// Type: ExtensionMethods
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Users\kuenz_ma\Downloads\Rescue_2013_Helden_des_Alltags_Demo\$_OUTDIR\Rescue2013Demo_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;

public static class ExtensionMethods
{
  private static string Decimals = "[^0-9.,-]";

  static ExtensionMethods()
  {
  }

  public static Transform FindByName(this Transform transform, string name, bool includeCurrentTransform = true, bool isExact = false)
  {
    if (isExact)
    {
      if (includeCurrentTransform && transform.name.Equals(name))
        return transform;
    }
    else if (includeCurrentTransform && transform.name.Contains(name))
      return transform;
    return ExtensionMethods.RecursiveFind(transform, name, isExact);
  }

  private static Transform RecursiveFind(Transform transform, string name, bool isExact = false)
  {
    foreach (Transform transform1 in transform)
    {
      if (isExact)
      {
        if (transform1.name.Equals(name))
          return transform1;
        Transform transform2 = ExtensionMethods.RecursiveFind(transform1, name, true);
        if ((UnityEngine.Object) transform2 != (UnityEngine.Object) null)
          return transform2;
      }
      else
      {
        if (transform1.name.Contains(name))
          return transform1;
        Transform transform2 = ExtensionMethods.RecursiveFind(transform1, name, false);
        if ((UnityEngine.Object) transform2 != (UnityEngine.Object) null)
          return transform2;
      }
    }
    return (Transform) null;
  }

  public static void DestroyComponents<T>(this Transform transform) where T : Component
  {
    foreach (T obj in transform.GetComponents<T>())
    {
      if ((UnityEngine.Object) obj != (UnityEngine.Object) null)
        UnityEngine.Object.Destroy((UnityEngine.Object) obj);
    }
  }

  public static void DestroyComponents(this Transform transform, System.Type type)
  {
    foreach (Component component in transform.GetComponents(type))
    {
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        UnityEngine.Object.Destroy((UnityEngine.Object) component);
    }
  }

  public static T FindComponent<T>(this Transform transform, string name, bool isExact = false) where T : Component
  {
    Transform byName = ExtensionMethods.FindByName(transform, name, false, isExact);
    if ((UnityEngine.Object) byName != (UnityEngine.Object) null)
      return byName.GetComponent<T>();
    else
      return (T) null;
  }

  public static T GetComponentInParents<T>(this Transform transform) where T : Component
  {
    T component = transform.GetComponent<T>();
    if ((UnityEngine.Object) component != (UnityEngine.Object) null || (UnityEngine.Object) transform.parent == (UnityEngine.Object) null)
      return component;
    else
      return ExtensionMethods.GetComponentInParents<T>(transform.parent);
  }

  public static Component GetComponentInParents(this Transform transform, System.Type type)
  {
    Component component = transform.GetComponent(type);
    if ((UnityEngine.Object) component != (UnityEngine.Object) null || (UnityEngine.Object) transform.parent == (UnityEngine.Object) null)
      return component;
    else
      return ExtensionMethods.GetComponentInParents(transform.parent, type);
  }

  public static T GetComponentInHierarchy<T>(this Transform transform) where T : Component
  {
    T obj = transform.GetComponentInChildren<T>();
    if ((UnityEngine.Object) obj == (UnityEngine.Object) null)
      obj = ExtensionMethods.GetComponentInParents<T>(transform);
    return obj;
  }

  public static Component GetComponentInHierarchy(this Transform transform, System.Type type)
  {
    Component component = transform.GetComponentInChildren(type);
    if ((UnityEngine.Object) component == (UnityEngine.Object) null)
      component = ExtensionMethods.GetComponentInParents(transform, type);
    return component;
  }

  public static Vector3 FromString(this Vector3 v, string value)
  {
    string str = new Regex(ExtensionMethods.Decimals).Replace(value, string.Empty);
    char[] chArray = new char[1];
    int index = 0;
    int num = 44;
    chArray[index] = (char) num;
    string[] strArray = str.Split(chArray);
    if (strArray.Length != 3)
    {
      UnityEngine.Debug.LogError((object) "Parameter \"value\" is incorrectly formatted.");
      return Vector3.zero;
    }
    else
    {
      try
      {
        v.x = float.Parse(strArray[0]);
        v.y = float.Parse(strArray[1]);
        v.z = float.Parse(strArray[2]);
        return v;
      }
      catch (ArgumentNullException ex)
      {
        UnityEngine.Debug.LogError((object) "Parameter \"value\" is contains a null field");
      }
      catch (FormatException ex)
      {
        UnityEngine.Debug.LogError((object) "Parameter \"value\" is not formatted correctly!");
      }
      catch (OverflowException ex)
      {
        UnityEngine.Debug.LogError((object) "Parameter \"value\" contains a value that is too big (or small) to be stored as a float!");
      }
      return Vector3.zero;
    }
  }

  public static Quaternion FromString(this Quaternion q, string value)
  {
    string str = new Regex(ExtensionMethods.Decimals).Replace(value, string.Empty);
    char[] chArray = new char[1];
    int index = 0;
    int num = 44;
    chArray[index] = (char) num;
    string[] strArray = str.Split(chArray);
    if (strArray.Length != 4)
    {
      UnityEngine.Debug.LogError((object) "Parameter \"value\" is incorrectly formatted.");
      return Quaternion.identity;
    }
    else
    {
      try
      {
        q.x = float.Parse(strArray[0]);
        q.y = float.Parse(strArray[1]);
        q.z = float.Parse(strArray[2]);
        q.w = float.Parse(strArray[3]);
        return q;
      }
      catch (ArgumentNullException ex)
      {
        UnityEngine.Debug.LogError((object) "Parameter \"value\" is contains a null field");
      }
      catch (FormatException ex)
      {
        UnityEngine.Debug.LogError((object) "Parameter \"value\" is not formatted correctly!");
      }
      catch (OverflowException ex)
      {
        UnityEngine.Debug.LogError((object) "Parameter \"value\" contains a value that is too big (or small) to be stored as a float!");
      }
      return Quaternion.identity;
    }
  }

  public static bool IsBetween<T>(this T item, T start, T end)
  {
    if (Comparer<T>.Default.Compare(start, end) == 0)
      return Comparer<T>.Default.Compare(item, start) == 0;
    int num1 = Comparer<T>.Default.Compare(item, start);
    int num2 = Comparer<T>.Default.Compare(item, end);
    if (num1 >= 0)
      return num2 < 0;
    else
      return false;
  }

  public static bool IsValid(this Vector3 v)
  {
    if (!float.IsNaN(v.x) && !float.IsNaN(v.y))
      return !float.IsNaN(v.z);
    else
      return false;
  }

  public static void AddOrSet<T, U>(this Dictionary<T, U> dictionary, T key, U valueToAdd)
  {
    if (dictionary.ContainsKey(key))
      dictionary[key] = valueToAdd;
    else
      dictionary.Add(key, valueToAdd);
  }

  public static bool IsAlmostEqual(this float a, float b, float precision)
  {
    return (double) Mathf.Abs(a - b) <= (double) precision;
  }
}
