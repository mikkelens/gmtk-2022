using Entities.Players;
using Management.Inputs;
using UnityEngine;

namespace Management
{
    [DefaultExecutionOrder(-10)]
    public partial class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        // ReSharper disable MemberCanBePrivate.Global
        public InputManager Input { get; private set; }
        public Player Player { get; private set; }
        public UIManager UI { get; private set; }
        public EventsManager EventsManager { get; private set; }
        // ReSharper restore MemberCanBePrivate.Global

        private GameState _state;
        public GameState State
        {
            get => _state;
            set
            {
                GameState previousState = _state;
                _state = _state switch
                {
                    GameState.Waiting => value,
                    GameState.Playing => value,
                    GameState.PausedDuringPlay => value,
                    GameState.Ended => GameState.Ended,
                    _ => GameState.Waiting,
                };
                if (_state != previousState)
                {
                    if (_state == GameState.PausedDuringPlay)
                    {
                        UI.TogglePauseMenu(true);
                    }
                    if (previousState == GameState.PausedDuringPlay)
                    {
                        UI.TogglePauseMenu(false);
                    }
                }
            }
        }

        private void Awake() => Instance = this;

        private void Start()
        {
            Input = InputManager.Instance;
            if (Input == null) Debug.LogWarning("InputManager is missing. Player input cannot be read.");
         
            Player = Player.Instance;
            if (Player == null) Debug.LogWarning("Player is missing. No player to play as.");
            
            EventsManager = EventsManager.Instance; 
            if (EventsManager == null) Debug.LogWarning("EventsManager is missing. Game cannot start read.");
            
            UI = UIManager.Instance; 
            if (UI == null) Debug.LogWarning("EventsManager is missing. Game cannot start read.");

            State = GameState.Playing;
            EventsManager.StartEventLoop(this);
        }

    }
}