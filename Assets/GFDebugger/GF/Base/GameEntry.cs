using UGF.Runtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GF
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public static class GameEntry
    {
        private static readonly GFLinkedList<UGFComponent> s_GameFrameworkComponents = new GFLinkedList<UGFComponent> ();

        /// <summary>
        /// 游戏框架所在的场景编号。
        /// </summary>
        internal const int GameFrameworkSceneId = 0;

        /// <summary>
        /// 获取游戏框架组件。
        /// </summary>
        /// <typeparam name="T">要获取的游戏框架组件类型。</typeparam>
        /// <returns>要获取的游戏框架组件。</returns>
        public static T GetComponent<T>() where T : UGFComponent
        {
            return (T)GetComponent(typeof(T));
        }

        /// <summary>
        /// 获取游戏框架组件。
        /// </summary>
        /// <param name="type">要获取的游戏框架组件类型。</param>
        /// <returns>要获取的游戏框架组件。</returns>
        public static UGFComponent GetComponent (Type type)
        {
            LinkedListNode<UGFComponent> current = s_GameFrameworkComponents.First;
            while (current != null)
            {
                if (current.Value.GetType() == type)
                {
                    return current.Value;
                }

                current = current.Next;
            }

            return null;
        }

        /// <summary>
        /// 获取游戏框架组件。
        /// </summary>
        /// <param name="typeName">要获取的游戏框架组件类型名称。</param>
        /// <returns>要获取的游戏框架组件。</returns>
        public static UGFComponent GetComponent (string typeName)
        {
            LinkedListNode<UGFComponent> current = s_GameFrameworkComponents.First;
            while (current != null)
            {
                Type type = current.Value.GetType();
                if (type.FullName == typeName || type.Name == typeName)
                {
                    return current.Value;
                }

                current = current.Next;
            }

            return null;
        }

//        /// <summary>
//        /// 关闭游戏框架。
//        /// </summary>
//        /// <param name="shutdownType">关闭游戏框架类型。</param>
//        public static void Shutdown(ShutdownType shutdownType)
//        {
//            Log.Info("Shutdown Game Framework ({0})...", shutdownType);
//            BaseComponent baseComponent = GetComponent<BaseComponent>();
//            if (baseComponent != null)
//            {
//                baseComponent.Shutdown();
//                baseComponent = null;
//            }

//            s_GameFrameworkComponents.Clear();

//            if (shutdownType == ShutdownType.None)
//            {
//                return;
//            }

//            if (shutdownType == ShutdownType.Restart)
//            {
//                SceneManager.LoadScene(GameFrameworkSceneId);
//                return;
//            }

//            if (shutdownType == ShutdownType.Quit)
//            {
//                Application.Quit();
//#if UNITY_EDITOR
//                UnityEditor.EditorApplication.isPlaying = false;
//#endif
//                return;
//            }
//        }

        /// <summary>
        /// 注册游戏框架组件。
        /// </summary>
        /// <param name="gameFrameworkComponent">要注册的游戏框架组件。</param>
        internal static void RegisterComponent( UGFComponent gameFrameworkComponent )
        {
            if (gameFrameworkComponent == null)
            {
                Debug.LogError ("Game Framework component is invalid.");
                return;
            }

            Type type = gameFrameworkComponent.GetType();

            LinkedListNode<UGFComponent> current = s_GameFrameworkComponents.First;
            while (current != null)
            {
                if (current.Value.GetType() == type)
                {
                    Debug.LogError ("Game Framework component type '{0}' is already exist." + type.FullName);
                    return;
                }

                current = current.Next;
            }

            s_GameFrameworkComponents.AddLast(gameFrameworkComponent);
        }
    }
}
