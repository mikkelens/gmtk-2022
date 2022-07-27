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
        [SerializeField] private double cullDelay = 1.0;
        [HorizontalGroup("Settings/CullGroup")]
        [SerializeField] private int frameCount;
        
        private double _lastTime;
        private double _newestTime;
        private List<double> _times;

        private void Start()
        {
            _times = new List<double>();
        }

        private void Update()
        {
            double newTime = GetNewTime();
            CullOldTimes(newTime);
            lastFrameTime = newTime - _lastTime;
            _times.Add(newTime);
            UpdateInfo(newTime);
        }
        private void UpdateInfo(double newTime)
        {
            double totalDeltaTime = _times.Sum(time => newTime - time);
            double highestDeltatime = _times.Max(time => newTime - time);
            double lowestDeltatime = _times.Min(time => newTime - time);
            averageFrameTime = totalDeltaTime / _times.Count;
            averageFPS = Mathf.RoundToInt((float)(1.0f / averageFrameTime));
            highestFPS = Mathf.RoundToInt((float)(1.0f / lowestDeltatime)); // lower frame time is higher fps
            lowestFPS = Mathf.RoundToInt((float)(1.0f / highestDeltatime)); // and vice versa
            frameCount = _times.Count;
        }
        private void CullOldTimes(double newTime)
        {
            _times.RemoveAll(time => time < newTime - cullDelay);
        }
        
        private double GetNewTime()
        {
            double newTime = Time.timeAsDouble;
            if (_newestTime !< newTime) return _newestTime;
            _lastTime = _newestTime;
            return _newestTime = newTime;
        }
    }
}
