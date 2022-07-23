using Gameplay.Entities.PlayerScripts;
using Management;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Gameplay
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private bool instantFollow = false;
        [HideIf("instantFollow")]
        [SerializeField] private float followSpeed = 25f;
        [HideIf("instantFollow")]
        [SerializeField] private float maxDistance = 5f;
    
        
        private GameManager _manager;
        private Player _player;
    
        private Vector3 _offset;

        private void Start()
        {
            _offset = transform.position;
        
            _manager = GameManager.Instance;
            if (_manager == null) throw new UnityException("No game manager in scene.");
            
            _player = Player.Instance;
            if (_player == null) throw new UnityException("No player in scene.");
        }

        private void LateUpdate()
        {
            Vector2 target = _player.transform.position.WorldToPlane() + _offset.WorldToPlane();

            if (instantFollow)
                InstantFollow(target);
            else
                SmoothFollow(target);
        }

        private void InstantFollow(Vector2 targetPos)
        {
            transform.position = targetPos.PlaneToWorld();
        }
        
        private void SmoothFollow(Vector2 targetPos)
        {
            // get direction
            Vector2 currentPos = transform.position.WorldToPlane();
            Vector2 direction = (targetPos - currentPos).normalized;
            
            // get speed
            float distance = Vector2.Distance(currentPos, targetPos);
            float speed = followSpeed / (1 - Mathf.Min(0.95f, distance / maxDistance));
            // Debug.Log($"Distance {distance}, Speed: {speed}");
            
            float increment = Mathf.Min(distance, speed * Time.deltaTime);
            Vector2 move = direction * increment;
            
            transform.Translate(move.PlaneToWorld(), Space.World);
        }
    }
}
