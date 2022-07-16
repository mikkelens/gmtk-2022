using Gameplay.Entities.PlayerScripts;
using Management;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private bool instantFollow = false;
    
        [HideIf("instantFollow")]
        [SerializeField] private float followSpeed = 25f;
    
        private GameManager _manager;
        private Player _player;
    
        private Vector3 _offset;
    
        private void Start()
        {
            _offset = transform.position;
        
            _manager = GameManager.Instance;
            if (_manager == null) throw new UnityException("No game manager in scene.");
            _player = _manager.player;
            if (_player == null) throw new UnityException("No player in scene.");
        }

        private void Update()
        {
            Vector3 playerPos = _player.transform.position;
            Vector3 target = playerPos + _offset;
            Vector3 newPosition;
            if (instantFollow)
                newPosition = target;
            else
                newPosition = Vector3.Lerp(transform.position, target, followSpeed * Time.deltaTime);

            transform.position = newPosition;
        }
    }
}