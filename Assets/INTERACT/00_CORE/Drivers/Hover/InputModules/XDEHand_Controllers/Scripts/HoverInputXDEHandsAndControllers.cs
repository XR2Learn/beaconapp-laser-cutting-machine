using Hover.Core.Cursors;
using UnityEngine;

namespace Hover.InputModules.XDEHands
{

    /*================================================================================================*/
    [ExecuteInEditMode]
    public class HoverInputXDEHandsAndControllers : MonoBehaviour
    {

        public HoverCursorDataProvider CursorDataProvider;

        [Space(12)]

        public Transform HandLeftPalm;
        //public Transform HandLeftThumb;
        public Transform HandLeftIndex;
        //public Transform HandLeftMiddle;
        public Transform ControllerLeft;
        private Transform ControllerLeftCursor;

        [Space(12)]

        public Transform HandRightPalm;
        //public Transform HandRightThumb;
        public Transform HandRightIndex;
        //public Transform HandRightMiddle;
        public Transform ControllerRight;
        private Transform ControllerRightCursor;


        ////////////////////////////////////////////////////////////////////////////////////////////////
        /*--------------------------------------------------------------------------------------------*/
        public void Awake()
        {
            CursorUtil.FindCursorReference(this, ref CursorDataProvider, false);

            if (Application.isPlaying)
            {
                if (ControllerLeft != null)
                {
                    GameObject go = new GameObject("ControllerUICursor");
                    go.transform.parent = ControllerLeft;
                    ControllerLeftCursor = go.transform;
                    ControllerLeftCursor.transform.localPosition = new Vector3(0.0f, -0.07f, 0.045f);
                }

                if (ControllerRight != null)
                {
                    GameObject go = new GameObject("ControllerUICursor");
                    go.transform.parent = ControllerRight;
                    ControllerRightCursor = go.transform;
                    ControllerRightCursor.transform.localPosition = new Vector3(0.0f, -0.07f, 0.055f);
                }
            }
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
            UpdateXDEHandsAndControllerCursors();
            CursorDataProvider.ActivateAllCursorsBasedOnUsage();
        }


        private void UpdateXDEHandsAndControllerCursors()
        {
            // XDE Hand
            if (HandLeftPalm != null && CursorDataProvider.HasCursorData(CursorType.LeftPalm))
                UpdateCursor(HandLeftPalm, CursorType.LeftPalm);
            //if (HandLeftThumb != null && CursorDataProvider.HasCursorData(CursorType.LeftThumb))
                //UpdateCursor(HandLeftThumb, CursorType.LeftThumb);
            if (HandLeftIndex != null && CursorDataProvider.HasCursorData(CursorType.LeftIndex))
                UpdateCursor(HandLeftIndex, CursorType.LeftIndex);
            //if (HandLeftMiddle != null && CursorDataProvider.HasCursorData(CursorType.LeftMiddle))
                //UpdateCursor(HandLeftMiddle, CursorType.LeftMiddle);
            if (HandRightPalm != null && CursorDataProvider.HasCursorData(CursorType.RightPalm))
                UpdateCursor(HandRightPalm, CursorType.RightPalm);
            //if (HandRightThumb != null && CursorDataProvider.HasCursorData(CursorType.RightThumb))
                //UpdateCursor(HandRightThumb, CursorType.RightThumb);
            if (HandRightIndex != null && CursorDataProvider.HasCursorData(CursorType.RightIndex))
                UpdateCursor(HandRightIndex, CursorType.RightIndex);
            //if (HandRightMiddle != null && CursorDataProvider.HasCursorData(CursorType.RightMiddle))
                //UpdateCursor(HandRightMiddle, CursorType.RightMiddle);

            // Controller Cursor (wired to CursorType.Pinky)
            if (ControllerLeftCursor != null && CursorDataProvider.HasCursorData(CursorType.LeftPinky))
                UpdateCursor(ControllerLeftCursor.transform, CursorType.LeftPinky);
            if (ControllerRightCursor != null && CursorDataProvider.HasCursorData(CursorType.RightPinky))
                UpdateCursor(ControllerRightCursor.transform, CursorType.RightPinky);
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
