using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Gameplay
{
    [ExecuteInEditMode]
    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    public class FPSRateScript : MonoBehaviour
    {
        [BoxGroup("Data")]
        [SerializeField] private int averageFPS;
        [BoxGroup("Data")]
        [SerializeField] private int highestFPS;
        [BoxGroup("Data")]
        [SerializeField] private int lowestFPS;
        [BoxGroup("Data")]
        [SerializeField] private double averageFrameTime;
        [BoxGroup("Data")]
        [SerializeField] private double lastFrameTime;
        
        [BoxGroup("Settings")]
        [HorizontalGroup("Settings/CullGroup")]
        [SerializeField] private float cullDelay = 1.0f;
        [HorizontalGroup("Settings/CullGroup")]
        [SerializeField] private int frameCount;
        
        private struct FrameInfo
        {
            public readonly double Time;
            public readonly double DeltaTime;
            public FrameInfo(double time, double deltaTime)
            {
                Time = time;
                DeltaTime = deltaTime;
            }
        }
        private readonly List<FrameInfo> _frames = new List<FrameInfo>();

        private void Update()
        {
            CullOldDeltaTimes();
            FrameInfo newFrame = GetNewestFrame();
            lastFrameTime = newFrame.DeltaTime;
            _frames.Add(newFrame);
            UpdateInfo();
        }
        
        private FrameInfo _lastFrame;
        private FrameInfo GetNewestFrame()
        {
            if (Math.Abs(_lastFrame.Time - GetTime()) < 0.0000000001) return _lastFrame;
            // calculate new frame inf
            double time = GetTime();
            double deltaTime = time - _lastFrame.Time;
            _lastFrame = new FrameInfo(time, deltaTime);
            return _lastFrame;
        }
        
        private static double GetTime() // overwritten in editor class
        {
        #if UNITY_EDITOR
            return EditorApplication.timeSinceStartup;
        #else
            return Time.timeAsDouble;
        #endif
        }

        private void UpdateInfo()
        {
            double totalTime = _frames.Sum(frame => frame.DeltaTime);
            double highestDeltatime = _frames.Max(frame => frame.DeltaTime);
            double lowestDeltatime = _frames.Min(frame => frame.DeltaTime);
            averageFrameTime = totalTime / _frames.Count;
            averageFPS = Mathf.RoundToInt((float)(1.0f / averageFrameTime));
            highestFPS = Mathf.RoundToInt((float)(1.0f / lowestDeltatime)); // lower frame time is higher fps
            lowestFPS = Mathf.RoundToInt((float)(1.0f / highestDeltatime)); // and vice versa
            frameCount = _frames.Count;
        }

        private void CullOldDeltaTimes()
        {
            _frames.RemoveAll(frame => frame.Time + cullDelay < GetTime());
        }
    }
}
