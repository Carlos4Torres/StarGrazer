// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""83ecedae-78e0-471e-a3e3-8e8a8a6e80b8"",
            ""actions"": [
                {
                    ""name"": ""PlayerMove"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b4f8cb0d-f333-491f-83b5-d7168f0f7cf4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ReticleMove"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d3a1d9e1-6635-412e-86c2-9786ed24a1c9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""854bb3c4-a4da-47e8-855f-9722e2eb42d5"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlayerMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c00a47b-72bf-4891-819d-533926d14fa0"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReticleMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_PlayerMove = m_Gameplay.FindAction("PlayerMove", throwIfNotFound: true);
        m_Gameplay_ReticleMove = m_Gameplay.FindAction("ReticleMove", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_PlayerMove;
    private readonly InputAction m_Gameplay_ReticleMove;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PlayerMove => m_Wrapper.m_Gameplay_PlayerMove;
        public InputAction @ReticleMove => m_Wrapper.m_Gameplay_ReticleMove;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @PlayerMove.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerMove;
                @PlayerMove.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerMove;
                @PlayerMove.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPlayerMove;
                @ReticleMove.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReticleMove;
                @ReticleMove.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReticleMove;
                @ReticleMove.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReticleMove;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PlayerMove.started += instance.OnPlayerMove;
                @PlayerMove.performed += instance.OnPlayerMove;
                @PlayerMove.canceled += instance.OnPlayerMove;
                @ReticleMove.started += instance.OnReticleMove;
                @ReticleMove.performed += instance.OnReticleMove;
                @ReticleMove.canceled += instance.OnReticleMove;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnPlayerMove(InputAction.CallbackContext context);
        void OnReticleMove(InputAction.CallbackContext context);
    }
}
