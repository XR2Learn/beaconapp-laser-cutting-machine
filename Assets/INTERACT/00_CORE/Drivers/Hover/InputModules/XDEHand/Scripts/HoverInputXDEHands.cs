using Hover.Core.Cursors;
using UnityEngine;

namespace Hover.InputModules.XDEHands
{

    /*================================================================================================*/
    [ExecuteInEditMode]
    public class HoverInputXDEHands : MonoBehaviour
    {

        public HoverCursorDataProvider CursorDataProvider;

        [Space(12)]

        public Transform LeftPalm;
        public Transform LeftThumb;
        public Transform LeftIndex;
        public Transform LeftMiddle;
        public Transform LeftRing;
        public Transform LeftPinky;

        [Space(12)]

        public Transform RightPalm;
        public Transform RightThumb;
        public Transform RightIndex;
        public Transform RightMiddle;
        public Transform RightRing;
        public Transform RightPinky;


        ////////////////////////////////////////////////////////////////////////////////////////////////
        /*--------------------------------------------------------------------------------------------*/
        public void Awake()
        {
            CursorUtil.FindCursorReference(this, ref CursorDataProvider, false);

        }

        /*--------------------------------------------------------------------------------------------*/
        public void Update()
        {
            if (!CursorUtil.FindCursorReference(this, ref CursorDataProvider, true))
            {
                return;
            }

            if (!Application.isPlaying)
            {
                return;
            }

            CursorDataProvider.MarkAllCursorsUnused();
            UpdateXDEHandsCursors();
            CursorDataProvider.ActivateAllCursorsBasedOnUsage();
        }


        private void UpdateXDEHandsCursors()
        {
            if (CursorDataProvider.HasCursorData(CursorType.LeftPalm))
                UpdateCursor(LeftPalm, CursorType.LeftPalm);
            if (CursorDataProvider.HasCursorData(CursorType.LeftThumb))
                UpdateCursor(LeftThumb, CursorType.LeftThumb);
            if (CursorDataProvider.HasCursorData(CursorType.LeftIndex))
                UpdateCursor(LeftIndex, CursorType.LeftIndex);
            if (CursorDataProvider.HasCursorData(CursorType.LeftMiddle))
                UpdateCursor(LeftMiddle, CursorType.LeftMiddle);
            if (CursorDataProvider.HasCursorData(CursorType.LeftRing))
                UpdateCursor(LeftRing, CursorType.LeftRing);
            if (CursorDataProvider.HasCursorData(CursorType.LeftPinky))
                UpdateCursor(LeftPinky, CursorType.LeftPinky);
            if (CursorDataProvider.HasCursorData(CursorType.RightPalm))
                UpdateCursor(RightPalm, CursorType.RightPalm);
            if (CursorDataProvider.HasCursorData(CursorType.RightThumb))
                UpdateCursor(RightThumb, CursorType.RightThumb);
            if (CursorDataProvider.HasCursorData(CursorType.RightIndex))
                UpdateCursor(RightIndex, CursorType.RightIndex);
            if (CursorDataProvider.HasCursorData(CursorType.RightMiddle))
                UpdateCursor(RightMiddle, CursorType.RightMiddle);
            if (CursorDataProvider.HasCursorData(CursorType.RightRing))
                UpdateCursor(RightRing, CursorType.RightRing);
            if (CursorDataProvider.HasCursorData(CursorType.RightPinky))
                UpdateCursor(RightPinky, CursorType.RightPinky);
        }

        private void UpdateCursor(Transform fingerTransform, CursorType Type)
        {
            ICursorDataForInput data = CursorDataProvider.GetCursorDataForInput(Type);

            if (data == null)
            {
                return;
            }

            data.SetUsedByInput(true);
            data.SetWorldPosition(fingerTransform.position);
            data.SetWorldRotation(fingerTransform.rotation * Quaternion.Euler(90.0f,0,0));
        }

    }
}
