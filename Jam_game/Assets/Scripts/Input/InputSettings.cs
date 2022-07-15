//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Settings/InputSettings.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Input
{
    public partial class @InputSettings : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputSettings()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputSettings"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""6d759961-5e0f-4d4b-8618-52065809d013"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""5494fc9e-b8a3-4bd0-909f-8f161e494b92"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f0085e7d-3a49-47a6-94f2-91ba5de76eb4"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""a228fc6b-c4d2-4682-bbb1-6e37bf868d87"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9b480bfa-8fc7-43aa-9bff-bd97db42a2ae"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3253824c-4c11-4ae9-abd9-2b0f52a45d34"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6f641037-5d42-4bb3-9868-8a9c726f3148"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c8755284-7c58-4f66-b2f5-c2e854de7925"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Combat"",
            ""id"": ""426244c4-8e23-4ea3-a037-182345ef6c27"",
            ""actions"": [
                {
                    ""name"": ""MouseAim"",
                    ""type"": ""Value"",
                    ""id"": ""96f8d0dc-5c6c-4c98-8f87-f4c26011c6fe"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ControllerAim"",
                    ""type"": ""Value"",
                    ""id"": ""bcd0c360-272b-4062-a043-f406a429939b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Melee"",
                    ""type"": ""Button"",
                    ""id"": ""f5e5d6c0-0a22-4cf1-b3f8-db741e6a4f36"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""8d3e84ac-247f-495b-a9aa-b133cf81b954"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8939a389-d8af-48eb-811c-8822c4ea78e5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Melee"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""943c12c0-e621-45ef-8e48-0c610f9b98e7"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""ControllerAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed537fa7-71cf-42f0-bd15-8e0dcd7b7825"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e46b08a0-21d2-4e8c-9d92-4e5b10fc3d69"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KBM"",
                    ""action"": ""MouseAim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KBM"",
            ""bindingGroup"": ""KBM"",
            ""devices"": []
        },
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": []
        }
    ]
}");
            // Movement
            m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
            m_Movement_Move = m_Movement.FindAction("Move", throwIfNotFound: true);
            // Combat
            m_Combat = asset.FindActionMap("Combat", throwIfNotFound: true);
            m_Combat_MouseAim = m_Combat.FindAction("MouseAim", throwIfNotFound: true);
            m_Combat_ControllerAim = m_Combat.FindAction("ControllerAim", throwIfNotFound: true);
            m_Combat_Melee = m_Combat.FindAction("Melee", throwIfNotFound: true);
            m_Combat_Throw = m_Combat.FindAction("Throw", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }
        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }
        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // Movement
        private readonly InputActionMap m_Movement;
        private IMovementActions m_MovementActionsCallbackInterface;
        private readonly InputAction m_Movement_Move;
        public struct MovementActions
        {
            private @InputSettings m_Wrapper;
            public MovementActions(@InputSettings wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Movement_Move;
            public InputActionMap Get() { return m_Wrapper.m_Movement; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
            public void SetCallbacks(IMovementActions instance)
            {
                if (m_Wrapper.m_MovementActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                }
                m_Wrapper.m_MovementActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                }
            }
        }
        public MovementActions @Movement => new MovementActions(this);

        // Combat
        private readonly InputActionMap m_Combat;
        private ICombatActions m_CombatActionsCallbackInterface;
        private readonly InputAction m_Combat_MouseAim;
        private readonly InputAction m_Combat_ControllerAim;
        private readonly InputAction m_Combat_Melee;
        private readonly InputAction m_Combat_Throw;
        public struct CombatActions
        {
            private @InputSettings m_Wrapper;
            public CombatActions(@InputSettings wrapper) { m_Wrapper = wrapper; }
            public InputAction @MouseAim => m_Wrapper.m_Combat_MouseAim;
            public InputAction @ControllerAim => m_Wrapper.m_Combat_ControllerAim;
            public InputAction @Melee => m_Wrapper.m_Combat_Melee;
            public InputAction @Throw => m_Wrapper.m_Combat_Throw;
            public InputActionMap Get() { return m_Wrapper.m_Combat; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(CombatActions set) { return set.Get(); }
            public void SetCallbacks(ICombatActions instance)
            {
                if (m_Wrapper.m_CombatActionsCallbackInterface != null)
                {
                    @MouseAim.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnMouseAim;
                    @MouseAim.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnMouseAim;
                    @MouseAim.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnMouseAim;
                    @ControllerAim.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnControllerAim;
                    @ControllerAim.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnControllerAim;
                    @ControllerAim.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnControllerAim;
                    @Melee.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnMelee;
                    @Melee.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnMelee;
                    @Melee.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnMelee;
                    @Throw.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnThrow;
                    @Throw.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnThrow;
                    @Throw.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnThrow;
                }
                m_Wrapper.m_CombatActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @MouseAim.started += instance.OnMouseAim;
                    @MouseAim.performed += instance.OnMouseAim;
                    @MouseAim.canceled += instance.OnMouseAim;
                    @ControllerAim.started += instance.OnControllerAim;
                    @ControllerAim.performed += instance.OnControllerAim;
                    @ControllerAim.canceled += instance.OnControllerAim;
                    @Melee.started += instance.OnMelee;
                    @Melee.performed += instance.OnMelee;
                    @Melee.canceled += instance.OnMelee;
                    @Throw.started += instance.OnThrow;
                    @Throw.performed += instance.OnThrow;
                    @Throw.canceled += instance.OnThrow;
                }
            }
        }
        public CombatActions @Combat => new CombatActions(this);
        private int m_KBMSchemeIndex = -1;
        public InputControlScheme KBMScheme
        {
            get
            {
                if (m_KBMSchemeIndex == -1) m_KBMSchemeIndex = asset.FindControlSchemeIndex("KBM");
                return asset.controlSchemes[m_KBMSchemeIndex];
            }
        }
        private int m_ControllerSchemeIndex = -1;
        public InputControlScheme ControllerScheme
        {
            get
            {
                if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
                return asset.controlSchemes[m_ControllerSchemeIndex];
            }
        }
        public interface IMovementActions
        {
            void OnMove(InputAction.CallbackContext context);
        }
        public interface ICombatActions
        {
            void OnMouseAim(InputAction.CallbackContext context);
            void OnControllerAim(InputAction.CallbackContext context);
            void OnMelee(InputAction.CallbackContext context);
            void OnThrow(InputAction.CallbackContext context);
        }
    }
}
