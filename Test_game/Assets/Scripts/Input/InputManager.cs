using Gameplay;
using UnityEngine;

namespace Input
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        private Board _board;
        private InputSettings _input;

        private bool _hasLetGoOfMoveKeys;
        private Vector2 _lastMousePos;
        private Field _lastHighlightedField;
        
        private void Awake()
        {
            Instance = this;
            _input = new InputSettings();
        }

        private void OnEnable()
        {
            _input.Enable();
        }
        private void OnDisable()
        {
            _input.Disable();
        }

        private void Start()
        {
            _board = Board.Instance;
            if (_board == null) throw new UnityException("No board in scene to send move input to!");
            
            _hasLetGoOfMoveKeys = true;
            
            // mouse binds
            _input.Board.Click.performed += ctx => MouseClick();
            _input.Board.MousePos.performed += ctx => MousePos(ctx.ReadValue<Vector2>());
            
            // keyboard binds
            _input.Board.MoveKeys.performed += ctx => MoveKeys(ctx.ReadValue<Vector2>());
            _input.Board.MoveKeys.canceled += ctx => _hasLetGoOfMoveKeys = true;
        }


        private void MouseClick()
        {
            ClickOnScreen();
        }
        
        private void ClickOnScreen() // Click on field
        {
            var clickedField = TryFindFieldUnderMouse(_lastMousePos);
            if (clickedField != null) _board.TryClickField(clickedField);
        }
        private void MousePos(Vector2 pos)
        {
            _lastMousePos = pos;

            // Highlight field
            Field fieldUnderMouse = TryFindFieldUnderMouse(pos);
            if (fieldUnderMouse == _lastHighlightedField) return; // both same field or both null, no need to do anything
            if (_lastHighlightedField != null)
            {
                _lastHighlightedField.HighlightField(false); // unhighlight old field
                _lastHighlightedField = null;
            }
            if (fieldUnderMouse != null)
            {
                fieldUnderMouse.HighlightField(true); // highlight new field
                _lastHighlightedField = fieldUnderMouse; // remember
            }
        }
        
        // Get field (for highlighting and moving using mouse)
        private Field TryFindFieldUnderMouse(Vector2 mousePos)
        {
            Camera cam = Camera.main;
            if (cam == null) throw new UnityException("No main camera in scene!");
            Ray mouseRay = cam.ScreenPointToRay(mousePos);
            bool hit = Physics.Raycast(mouseRay, out RaycastHit hitData, Mathf.Infinity, LayerMask.GetMask("Field"));
            float distance = hit ? hitData.distance : 6f;
            Debug.DrawRay(mouseRay.origin, mouseRay.direction * distance, hit ? Color.green : Color.red, 0.25f);
            
            if (!hit) return null;
            return hitData.transform.GetComponentInParent<Field>();
        }
        
        // Simply move in direction
        private void MoveKeys(Vector2 move)
        {
            if (!_hasLetGoOfMoveKeys) return;
            _hasLetGoOfMoveKeys = false;
            _board.TryMove(Vector2Int.RoundToInt(move));
        }
    }
}
