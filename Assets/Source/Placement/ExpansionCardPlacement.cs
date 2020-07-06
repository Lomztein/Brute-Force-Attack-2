﻿using Lomztein.BFA2.Modification.Modifiers.ModProviders;
using Lomztein.BFA2.Turrets.ExpansionCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Placement
{
    public class ExpansionCardPlacement : ISimplePlacement
    {
        public event Action OnPlaced;
        public event Action OnFinished;

        private GameObject _obj;
        private IExpansionCard _expansionCard;
        private IExpansionCardAcceptor _target;

        private Func<bool>[] _placeRequirements;

        public ExpansionCardPlacement(params Func<bool>[] placeRequirements)
        {
            _placeRequirements = placeRequirements;
        }

        public bool Pickup(GameObject obj)
        {
            _obj = obj;
            _expansionCard = obj.GetComponent<IExpansionCard>();
            return _expansionCard != null;
        }

        public bool Place()
        {
            if (_target != null)
            {
                if (_placeRequirements.All(x => x() == true))
                {
                    GameObject cardGO = UnityEngine.Object.Instantiate(_obj);
                    IExpansionCard card = cardGO.GetComponent<IExpansionCard>();
                    if (_target.InsertCard(card))
                    {
                        _expansionCard.ApplyTo(_target);
                        OnPlaced?.Invoke();
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ToPosition(Vector2 position, Quaternion rotation)
        {
            IExpansionCardAcceptor[] options = Physics2D.OverlapPointAll(position)
                .Select(x => x.GetComponent<IExpansionCardAcceptor>())
                .Where(x => x != null && _expansionCard.CompatableWith(x.Attributes))
                .ToArray();

            if (options.Length == 1)
            {
                _target = options.First();
            }
            else
            {
                _target = null;
            }
            return true;
        }

        public override string ToString()
        {
            if (_target != null)
            {
                if (!_target.HasCapacity ())
                {
                    return $"{_target} is at max expansion card capacity.";
                }
                else
                {
                    return $"Insert {_expansionCard} into {_target}?";
                }
            }
            else
            {
                return _expansionCard.ToString();
            }
        }

        public bool Finish()
        {
            UnityEngine.Object.Destroy(_obj);
            OnFinished?.Invoke();
            return true;
        }
    }
}
