﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Placement
{
    public class PlacementController : MonoBehaviour
    {
        public static PlacementController Instance;
        private InputMaster _inputMaster;

        [SerializeField] private IPlacementBehaviour[] _behaviours;

        public bool Busy => _behaviours.Any(x => x.Busy);

        public void Awake()
        {
            Instance = this;
            _behaviours = GetComponents<IPlacementBehaviour>();

            _inputMaster = new InputMaster();
            _inputMaster.General.CancelPause.performed += Cancel;
            _inputMaster.General.Enable();
            _inputMaster.Enable();
        }

        private void OnDestroy()
        {
            InputMaster master = new InputMaster();
            _inputMaster.General.CancelPause.performed -= Cancel;
        }

        private void Cancel(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            StartCoroutine(Cancel());
        }

        private IEnumerator Cancel ()
        {
            yield return new WaitForEndOfFrame();
            CancelAll();
        }

        public void TakePlacement (IPlacement placement)
        {
            CancelAll();
            placement.Init();
            GetBehaviour(placement.GetType()).TakePlacement(placement);
        }

        public void CancelAll ()
        {
            foreach (var behaviour in _behaviours)
            {
                behaviour.Cancel();
            }
        }

        private IPlacementBehaviour GetBehaviour(Type placementType) => _behaviours.First(x => x.CanHandleType(placementType));
    }
}
