using System;
using System.Collections.Generic;
using UnityEditor;

namespace Editor
{
    public abstract class ExtendedEditor : UnityEditor.Editor
    {
        #region Static Fields

        protected const float FieldWidth = 40;
        protected const int FieldHeight = 16;
        protected const int FieldHorizontalGap = 5;
        protected const int FieldVerticalGap = 5;

        #endregion
        
        
        protected delegate void CreateInspectorUI();
        private InspectorComponent[] _components;

        private void OnEnable()
        {
            _components = RegisterComponents();
        }

        protected void DrawInspectorComponents()
        {
            foreach (var component in _components)
            {
                component.Draw();
            }
        }

        protected virtual InspectorComponent[] RegisterComponents()
        {
            return Array.Empty<InspectorComponent>();
        }
        
        protected class InspectorComponent
        {
            private readonly CreateInspectorUI _uiMethod;

            private string _foldoutTitle;
            private bool _hasFoldout;
            private bool _isExtended = true;

            public InspectorComponent(CreateInspectorUI uiMethod)
            {
                _uiMethod = uiMethod;
            }

            public static InspectorComponent Component(CreateInspectorUI uiMethod)
            {
                return new InspectorComponent(uiMethod);
            }

            public void Draw()
            {
                if (_hasFoldout)
                {
                    _isExtended = EditorGUILayout.Foldout(_isExtended, _foldoutTitle);
                }
                
                if (_isExtended) _uiMethod.Invoke();
            }

            #region Modification Methods

            public InspectorComponent Foldout(string title)
            {
                _foldoutTitle = title;
                _hasFoldout = true;
                _isExtended = false;

                return this;
            }

            #endregion
        }
    }
}
