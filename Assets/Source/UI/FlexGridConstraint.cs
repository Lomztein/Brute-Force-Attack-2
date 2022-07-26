using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.BFA2.UI
{
    [ExecuteAlways]
    public class FlexGridConstraint : MonoBehaviour
    {
        public GridLayoutGroup Layout;
        public GridLayoutGroup.Constraint Constraint;
        public int ConstraintMax;

        public void Update()
        {
            Layout.constraint = Constraint;
            Layout.constraintCount = Mathf.Min(transform.childCount, ConstraintMax);
        }
    }
}
