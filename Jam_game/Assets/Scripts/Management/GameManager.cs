﻿using System;
using Gameplay.Entities.PlayerScripts;
using Input;
using UnityEngine;

namespace Management
{
    [DefaultExecutionOrder(-10)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        public Player player; // player in scene. input needs this reference.

        private InputController _inputController;
        private int _killCount;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            player = Player.Instance;
            _inputController = InputController.Instance;
            if (_inputController == null) throw new Exception("InputManager is missing. Player input cannot be read.");
        }

        private void AddKillStatistic(int number = 1)
        {
            _killCount += number;
            // todo: update ui
        }
    }
}