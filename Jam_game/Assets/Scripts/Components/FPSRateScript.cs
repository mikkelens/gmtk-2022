using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Components
{
    public class FPSRateScript : MonoBehaviour
    {
        [BoxGroup("Settings")]
        [SerializeField] private float cullDelay = 1.0f;
        
        [BoxGroup("Data")]
        [SerializeField, ReadOnly] private int averageFPS;
        [BoxGroup("Data")]
        [SerializeField, ReadOnly] private float averageDeltaTime;
        [BoxGroup("Data")]
        [SerializeField, ReadOnly] private float lowestDeltaTime;
        [BoxGroup("Data")]
        [SerializeField, ReadOnly] private float highestDeltaTime;
        [BoxGroup("Data")]
        [SerializeField, ReadOnly] private float newestDeltaTime;
        
        private readonly List<Frame> _frames = new List<Frame>();
        
        private Frame _newestFrame;
        
        private struct Frame
        {
            public readonly double Time;
            public readonly float DeltaTime;

            public Frame(double newTime, float newDeltaTime)
            {
                Time = newTime;
                DeltaTime = newDeltaTime;
            }
        }
        
        private void Update()
        {
            UpdateNewestFrame();
            CullOldTimes(); // cull times too old compared to current (new) time
            if (_frames.Count > 0)
            {
                UpdateInfo(); // display previous times' relation to current (new) time
            }
        }

        private void UpdateInfo()
        {
            newestDeltaTime = _newestFrame.DeltaTime;
            averageDeltaTime = _frames.Average(frame => frame.DeltaTime);
            highestDeltaTime = _frames.Max(frame => frame.DeltaTime);
            lowestDeltaTime = _frames.Min(frame => frame.DeltaTime);
            averageFPS = Mathf.RoundToInt(1.0f / averageDeltaTime);
        }

        private void UpdateNewestFrame()
        {
            _newestFrame = new Frame(Time.unscaledTimeAsDouble, Time.unscaledDeltaTime);
            _frames.Add(_newestFrame); // add current (new) time to list of times to be compared
        }
        
        private void CullOldTimes()
        {
            _frames.RemoveAll(frame => frame.Time + cullDelay < _newestFrame.Time);
        }
    }
}
