using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;
#if UNITY_WEBGL && !UNITY_EDITOR
using UnityEngine;
using UnityEngine.Scripting;

[assembly: AlwaysLinkAssembly]
#endif
namespace WebGLUtils
{
    public static class WebApplication
    {
        /// <remarks>
        /// Has delay, use <see cref="InBackgroundChanged"/> instead
        /// </remarks>
        public static bool InBackground => InBackgroundInternal();

        public static event Action<bool> InBackgroundChanged;

        /// <summary>
        /// Use it to check whether you're running the game in the Editor or another platform.
        /// </summary>
        public static bool IsWebGL
        {
            get
            {
#if UNITY_WEBGL && !UNITY_EDITOR
                return true;
#else
                return false;
#endif
            }
        }
        
        public static bool IsMobile
        {
            get
            {
                if (IsWebGL)
                    return IsMobileInternal();

                return SystemInfo.deviceType == DeviceType.Handheld;
            }
        }


        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Unity InitializeOnLoadMethod")]
#if UNITY_WEBGL && !UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
#endif
        private static void Initialize()
            => InitializeInternal(InvokeInBackgroundChanged);

        [MonoPInvokeCallback(typeof(Action<bool>))]
        private static void InvokeInBackgroundChanged(bool hidden) 
            => InBackgroundChanged?.Invoke(hidden);

        

        [DllImport("__Internal")]
        private static extern bool InitializeInternal(Action<bool> onInBackgroundChange);

        [DllImport("__Internal")]
        private static extern bool InBackgroundInternal();

        [DllImport("__Internal")]
        private static extern bool IsMobileInternal();
    }
}
