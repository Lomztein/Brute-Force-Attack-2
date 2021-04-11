// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""General"",
            ""id"": ""ce515a5b-9fb2-41b4-ab40-d15d2547afbb"",
            ""actions"": [
                {
                    ""name"": ""CancelPause"",
                    ""type"": ""Button"",
                    ""id"": ""ec58b7d3-1ee6-4334-a3fa-5ae7688447e8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrimaryClick"",
                    ""type"": ""Button"",
                    ""id"": ""cd9ba05f-9020-4e5e-a037-d00d36b404eb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondaryClick"",
                    ""type"": ""Button"",
                    ""id"": ""e07a1f54-8814-4fa0-bc1b-6271c096c54f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""66121cd7-7758-4491-9922-1a6b17ee9648"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""CancelPause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d09c980-279d-476e-a0fa-86b26b652ffa"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""PrimaryClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb5482fe-72b3-4af9-a296-3bc0b1e858fa"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""SecondaryClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Battlefield"",
            ""id"": ""35d19b7b-6281-4fce-b2d7-6205ed400ed9"",
            ""actions"": [
                {
                    ""name"": ""StartWave"",
                    ""type"": ""Button"",
                    ""id"": ""f6529fbb-5916-492a-af00-07ad6baf0d5d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleFastMode"",
                    ""type"": ""Button"",
                    ""id"": ""132dec6a-9b7d-46e4-8250-a6c145d6840c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""627fe6d6-90ae-484d-8ec0-756846ccc671"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""StartWave"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b5d35f0-f661-463a-b272-611baa2d99f6"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""ToggleFastMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Placement"",
            ""id"": ""2ed9b9c8-8c2f-4dfe-9e53-39e274c1807b"",
            ""actions"": [
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Button"",
                    ""id"": ""d061f0f3-82cf-498c-aad2-8d7dc5f20c8d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""QuickPlace"",
                    ""type"": ""Button"",
                    ""id"": ""d47deb4d-d0bf-430b-a57b-971ad0f9c400"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""da0cd24e-fd8b-4d35-9e36-eea8b9738eb5"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92911641-1365-4e48-b404-d64b8b4385d4"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""QuickPlace"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""7a253d34-fbc7-49d0-9f43-c1964b3d846c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""301b7388-3010-461c-8f69-84f9ba4fd415"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""a18291ec-56ae-49b1-b12f-a7ca5c026325"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""9d0133fd-8603-4f1b-b415-313484d68a4b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c711c8a4-a130-40ff-bd0f-03bb62df8fa5"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cb983865-4331-4f9c-85d0-90063dcda7df"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e6007acc-d214-4700-a3e8-7b19ddc0bbb4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""cd0a8b39-31fd-42f6-9a01-fabb870a75be"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d1595859-f455-4f06-873b-a6890d73153d"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""GeneralEditor"",
            ""id"": ""b6457672-4298-455d-b10c-f442ac7d49fd"",
            ""actions"": [],
            ""bindings"": []
        },
        {
            ""name"": ""AssemblyEditor"",
            ""id"": ""7f5942cd-e636-41ec-a763-9318cd57687d"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""485d8612-4323-4a98-9f4b-d448540affff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""333a4522-c6b7-45c0-9b70-6048995941b4"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MapEditor"",
            ""id"": ""e8e7b0b8-7b65-4547-b0c0-16d84295fde5"",
            ""actions"": [
                {
                    ""name"": ""SnapToGrid"",
                    ""type"": ""Button"",
                    ""id"": ""c80f0f7c-ff70-4872-a476-a1c8291c4356"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Clone"",
                    ""type"": ""Button"",
                    ""id"": ""2dce9d2e-ed20-4c37-bb62-691602de52d9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b5594786-aada-4a85-bc50-6d655037e8f0"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Default"",
                    ""action"": ""SnapToGrid"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ca98909-dec4-4ac6-94a4-7080eddefa81"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Clone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Default"",
            ""bindingGroup"": ""Default"",
            ""devices"": []
        }
    ]
}");
        // General
        m_General = asset.FindActionMap("General", throwIfNotFound: true);
        m_General_CancelPause = m_General.FindAction("CancelPause", throwIfNotFound: true);
        m_General_PrimaryClick = m_General.FindAction("PrimaryClick", throwIfNotFound: true);
        m_General_SecondaryClick = m_General.FindAction("SecondaryClick", throwIfNotFound: true);
        // Battlefield
        m_Battlefield = asset.FindActionMap("Battlefield", throwIfNotFound: true);
        m_Battlefield_StartWave = m_Battlefield.FindAction("StartWave", throwIfNotFound: true);
        m_Battlefield_ToggleFastMode = m_Battlefield.FindAction("ToggleFastMode", throwIfNotFound: true);
        // Placement
        m_Placement = asset.FindActionMap("Placement", throwIfNotFound: true);
        m_Placement_Rotate = m_Placement.FindAction("Rotate", throwIfNotFound: true);
        m_Placement_QuickPlace = m_Placement.FindAction("QuickPlace", throwIfNotFound: true);
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_Move = m_Camera.FindAction("Move", throwIfNotFound: true);
        m_Camera_Zoom = m_Camera.FindAction("Zoom", throwIfNotFound: true);
        // GeneralEditor
        m_GeneralEditor = asset.FindActionMap("GeneralEditor", throwIfNotFound: true);
        // AssemblyEditor
        m_AssemblyEditor = asset.FindActionMap("AssemblyEditor", throwIfNotFound: true);
        m_AssemblyEditor_Newaction = m_AssemblyEditor.FindAction("New action", throwIfNotFound: true);
        // MapEditor
        m_MapEditor = asset.FindActionMap("MapEditor", throwIfNotFound: true);
        m_MapEditor_SnapToGrid = m_MapEditor.FindAction("SnapToGrid", throwIfNotFound: true);
        m_MapEditor_Clone = m_MapEditor.FindAction("Clone", throwIfNotFound: true);
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

    // General
    private readonly InputActionMap m_General;
    private IGeneralActions m_GeneralActionsCallbackInterface;
    private readonly InputAction m_General_CancelPause;
    private readonly InputAction m_General_PrimaryClick;
    private readonly InputAction m_General_SecondaryClick;
    public struct GeneralActions
    {
        private @InputMaster m_Wrapper;
        public GeneralActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @CancelPause => m_Wrapper.m_General_CancelPause;
        public InputAction @PrimaryClick => m_Wrapper.m_General_PrimaryClick;
        public InputAction @SecondaryClick => m_Wrapper.m_General_SecondaryClick;
        public InputActionMap Get() { return m_Wrapper.m_General; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GeneralActions set) { return set.Get(); }
        public void SetCallbacks(IGeneralActions instance)
        {
            if (m_Wrapper.m_GeneralActionsCallbackInterface != null)
            {
                @CancelPause.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnCancelPause;
                @CancelPause.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnCancelPause;
                @CancelPause.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnCancelPause;
                @PrimaryClick.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnPrimaryClick;
                @PrimaryClick.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnPrimaryClick;
                @PrimaryClick.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnPrimaryClick;
                @SecondaryClick.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnSecondaryClick;
                @SecondaryClick.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnSecondaryClick;
                @SecondaryClick.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnSecondaryClick;
            }
            m_Wrapper.m_GeneralActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CancelPause.started += instance.OnCancelPause;
                @CancelPause.performed += instance.OnCancelPause;
                @CancelPause.canceled += instance.OnCancelPause;
                @PrimaryClick.started += instance.OnPrimaryClick;
                @PrimaryClick.performed += instance.OnPrimaryClick;
                @PrimaryClick.canceled += instance.OnPrimaryClick;
                @SecondaryClick.started += instance.OnSecondaryClick;
                @SecondaryClick.performed += instance.OnSecondaryClick;
                @SecondaryClick.canceled += instance.OnSecondaryClick;
            }
        }
    }
    public GeneralActions @General => new GeneralActions(this);

    // Battlefield
    private readonly InputActionMap m_Battlefield;
    private IBattlefieldActions m_BattlefieldActionsCallbackInterface;
    private readonly InputAction m_Battlefield_StartWave;
    private readonly InputAction m_Battlefield_ToggleFastMode;
    public struct BattlefieldActions
    {
        private @InputMaster m_Wrapper;
        public BattlefieldActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @StartWave => m_Wrapper.m_Battlefield_StartWave;
        public InputAction @ToggleFastMode => m_Wrapper.m_Battlefield_ToggleFastMode;
        public InputActionMap Get() { return m_Wrapper.m_Battlefield; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BattlefieldActions set) { return set.Get(); }
        public void SetCallbacks(IBattlefieldActions instance)
        {
            if (m_Wrapper.m_BattlefieldActionsCallbackInterface != null)
            {
                @StartWave.started -= m_Wrapper.m_BattlefieldActionsCallbackInterface.OnStartWave;
                @StartWave.performed -= m_Wrapper.m_BattlefieldActionsCallbackInterface.OnStartWave;
                @StartWave.canceled -= m_Wrapper.m_BattlefieldActionsCallbackInterface.OnStartWave;
                @ToggleFastMode.started -= m_Wrapper.m_BattlefieldActionsCallbackInterface.OnToggleFastMode;
                @ToggleFastMode.performed -= m_Wrapper.m_BattlefieldActionsCallbackInterface.OnToggleFastMode;
                @ToggleFastMode.canceled -= m_Wrapper.m_BattlefieldActionsCallbackInterface.OnToggleFastMode;
            }
            m_Wrapper.m_BattlefieldActionsCallbackInterface = instance;
            if (instance != null)
            {
                @StartWave.started += instance.OnStartWave;
                @StartWave.performed += instance.OnStartWave;
                @StartWave.canceled += instance.OnStartWave;
                @ToggleFastMode.started += instance.OnToggleFastMode;
                @ToggleFastMode.performed += instance.OnToggleFastMode;
                @ToggleFastMode.canceled += instance.OnToggleFastMode;
            }
        }
    }
    public BattlefieldActions @Battlefield => new BattlefieldActions(this);

    // Placement
    private readonly InputActionMap m_Placement;
    private IPlacementActions m_PlacementActionsCallbackInterface;
    private readonly InputAction m_Placement_Rotate;
    private readonly InputAction m_Placement_QuickPlace;
    public struct PlacementActions
    {
        private @InputMaster m_Wrapper;
        public PlacementActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotate => m_Wrapper.m_Placement_Rotate;
        public InputAction @QuickPlace => m_Wrapper.m_Placement_QuickPlace;
        public InputActionMap Get() { return m_Wrapper.m_Placement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlacementActions set) { return set.Get(); }
        public void SetCallbacks(IPlacementActions instance)
        {
            if (m_Wrapper.m_PlacementActionsCallbackInterface != null)
            {
                @Rotate.started -= m_Wrapper.m_PlacementActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_PlacementActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_PlacementActionsCallbackInterface.OnRotate;
                @QuickPlace.started -= m_Wrapper.m_PlacementActionsCallbackInterface.OnQuickPlace;
                @QuickPlace.performed -= m_Wrapper.m_PlacementActionsCallbackInterface.OnQuickPlace;
                @QuickPlace.canceled -= m_Wrapper.m_PlacementActionsCallbackInterface.OnQuickPlace;
            }
            m_Wrapper.m_PlacementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @QuickPlace.started += instance.OnQuickPlace;
                @QuickPlace.performed += instance.OnQuickPlace;
                @QuickPlace.canceled += instance.OnQuickPlace;
            }
        }
    }
    public PlacementActions @Placement => new PlacementActions(this);

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_Move;
    private readonly InputAction m_Camera_Zoom;
    public struct CameraActions
    {
        private @InputMaster m_Wrapper;
        public CameraActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Camera_Move;
        public InputAction @Zoom => m_Wrapper.m_Camera_Zoom;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMove;
                @Zoom.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);

    // GeneralEditor
    private readonly InputActionMap m_GeneralEditor;
    private IGeneralEditorActions m_GeneralEditorActionsCallbackInterface;
    public struct GeneralEditorActions
    {
        private @InputMaster m_Wrapper;
        public GeneralEditorActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputActionMap Get() { return m_Wrapper.m_GeneralEditor; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GeneralEditorActions set) { return set.Get(); }
        public void SetCallbacks(IGeneralEditorActions instance)
        {
            if (m_Wrapper.m_GeneralEditorActionsCallbackInterface != null)
            {
            }
            m_Wrapper.m_GeneralEditorActionsCallbackInterface = instance;
            if (instance != null)
            {
            }
        }
    }
    public GeneralEditorActions @GeneralEditor => new GeneralEditorActions(this);

    // AssemblyEditor
    private readonly InputActionMap m_AssemblyEditor;
    private IAssemblyEditorActions m_AssemblyEditorActionsCallbackInterface;
    private readonly InputAction m_AssemblyEditor_Newaction;
    public struct AssemblyEditorActions
    {
        private @InputMaster m_Wrapper;
        public AssemblyEditorActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_AssemblyEditor_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_AssemblyEditor; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AssemblyEditorActions set) { return set.Get(); }
        public void SetCallbacks(IAssemblyEditorActions instance)
        {
            if (m_Wrapper.m_AssemblyEditorActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_AssemblyEditorActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_AssemblyEditorActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_AssemblyEditorActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_AssemblyEditorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public AssemblyEditorActions @AssemblyEditor => new AssemblyEditorActions(this);

    // MapEditor
    private readonly InputActionMap m_MapEditor;
    private IMapEditorActions m_MapEditorActionsCallbackInterface;
    private readonly InputAction m_MapEditor_SnapToGrid;
    private readonly InputAction m_MapEditor_Clone;
    public struct MapEditorActions
    {
        private @InputMaster m_Wrapper;
        public MapEditorActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @SnapToGrid => m_Wrapper.m_MapEditor_SnapToGrid;
        public InputAction @Clone => m_Wrapper.m_MapEditor_Clone;
        public InputActionMap Get() { return m_Wrapper.m_MapEditor; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MapEditorActions set) { return set.Get(); }
        public void SetCallbacks(IMapEditorActions instance)
        {
            if (m_Wrapper.m_MapEditorActionsCallbackInterface != null)
            {
                @SnapToGrid.started -= m_Wrapper.m_MapEditorActionsCallbackInterface.OnSnapToGrid;
                @SnapToGrid.performed -= m_Wrapper.m_MapEditorActionsCallbackInterface.OnSnapToGrid;
                @SnapToGrid.canceled -= m_Wrapper.m_MapEditorActionsCallbackInterface.OnSnapToGrid;
                @Clone.started -= m_Wrapper.m_MapEditorActionsCallbackInterface.OnClone;
                @Clone.performed -= m_Wrapper.m_MapEditorActionsCallbackInterface.OnClone;
                @Clone.canceled -= m_Wrapper.m_MapEditorActionsCallbackInterface.OnClone;
            }
            m_Wrapper.m_MapEditorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SnapToGrid.started += instance.OnSnapToGrid;
                @SnapToGrid.performed += instance.OnSnapToGrid;
                @SnapToGrid.canceled += instance.OnSnapToGrid;
                @Clone.started += instance.OnClone;
                @Clone.performed += instance.OnClone;
                @Clone.canceled += instance.OnClone;
            }
        }
    }
    public MapEditorActions @MapEditor => new MapEditorActions(this);
    private int m_DefaultSchemeIndex = -1;
    public InputControlScheme DefaultScheme
    {
        get
        {
            if (m_DefaultSchemeIndex == -1) m_DefaultSchemeIndex = asset.FindControlSchemeIndex("Default");
            return asset.controlSchemes[m_DefaultSchemeIndex];
        }
    }
    public interface IGeneralActions
    {
        void OnCancelPause(InputAction.CallbackContext context);
        void OnPrimaryClick(InputAction.CallbackContext context);
        void OnSecondaryClick(InputAction.CallbackContext context);
    }
    public interface IBattlefieldActions
    {
        void OnStartWave(InputAction.CallbackContext context);
        void OnToggleFastMode(InputAction.CallbackContext context);
    }
    public interface IPlacementActions
    {
        void OnRotate(InputAction.CallbackContext context);
        void OnQuickPlace(InputAction.CallbackContext context);
    }
    public interface ICameraActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
    }
    public interface IGeneralEditorActions
    {
    }
    public interface IAssemblyEditorActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
    public interface IMapEditorActions
    {
        void OnSnapToGrid(InputAction.CallbackContext context);
        void OnClone(InputAction.CallbackContext context);
    }
}
