﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

/// <summary>
/// Singleton behaviour class, used for components that should only have one instance
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static event Action InitComplte;

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                T _temp = (T)GameObject.FindObjectOfType (typeof (T));
                if (_temp != null)
                {
                    instance = _temp;
                }
                else
                {
                    GameObject obj = new GameObject (typeof (T).Name);
                    instance = obj.AddComponent<T> ();
                }
            }
            return instance;
        }
    }

    /// <summary>
    /// Returns whether the instance has been initialized or not.
    /// </summary>
    public static bool IsInitialized
    {
        get
        {
            return instance != null;
        }
    }

    /// <summary>
    /// Base awake method that sets the singleton's unique instance.
    /// </summary>
    protected virtual void Awake()
    {
        if (instance != null && instance.gameObject != gameObject)
        {
            Debug.LogErrorFormat("{0}为单例对象，但是场景中存在多个{0}，已删除本对象", GetType().Name);
            Destroy(gameObject);
        }
        else
        {
            instance = (T)this;
            if (InitComplte != null)
            {
                InitComplte();
            }
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
        InitComplte = null;
    }
}
