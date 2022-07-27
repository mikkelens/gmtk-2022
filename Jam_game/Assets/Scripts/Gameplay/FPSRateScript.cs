using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Gameplay
{
    // [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    public class FPSRateScript : MonoBehaviour
    {
        [BoxGroup("Settings")]
        [SerializeField] private double cullDelay = 1.0;
        
        [BoxGroup("Data")]
        [SerializeField] private int averageFPS;
        [BoxGroup("Data")]
        [SerializeField] private double averageDeltaTime;
        [BoxGroup("Data")]
        [SerializeField] private double lowestDeltaTime;
        [BoxGroup("Data")]
        [SerializeField] private double highestDeltaTime;
        [BoxGroup("Data")]
        [SerializeField] private double newestDeltaTime;
        
        [SerializeField] private List<Frame> frames = new List<Frame>();
        
        private Frame _newestFrame;
        
        [Serializable]
        private struct Frame
        {
            public double time;
            public double deltaTime;

            public Frame(double newTime, double newDeltaTime)
            {
                time = newTime;
                deltaTime = newDeltaTime;
            }
        }
        
        private void Update()
        {
            UpdateNewestFrame();
            CullOldTimes(); // cull times too old compared to current (new) time
            if (frames.Count > 0)
            {
                UpdateInfo(); // display previous times' relation to current (new) time
            }
        }
        
        private void UpdateInfo()
        {
            newestDeltaTime = _newestFrame.deltaTime;
            averageDeltaTime = frames.Average(frame => frame.deltaTime);
            highestDeltaTime = frames.Max(frame => frame.deltaTime);
            lowestDeltaTime = frames.Min(frame => frame.deltaTime);
            averageFPS = Mathf.RoundToInt((float)(1.0f / averageDeltaTime));
            UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
        }

        private void UpdateNewestFrame()
        {
            _newestFrame = new Frame(Time.unscaledTimeAsDouble, Time.unscaledDeltaTime);
            frames.Add(_newestFrame); // add current (new) time to list of times to be compared
        }
        
        private void CullOldTimes()
        {
            frames.RemoveAll(frame => frame.time + cullDelay < _newestFrame.time);
        }
    }
}
