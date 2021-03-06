using System.Reflection;
using UnityEditor;

namespace DC.Tools
{
    public abstract class Button
    {
        public readonly string DisplayName;

        public readonly MethodInfo Method;

        private readonly ButtonSpacing _spacing;
        private readonly bool _disabled;

        protected Button(MethodInfo method, ButtonAttribute buttonAttribute)
        {
            DisplayName = string.IsNullOrEmpty(buttonAttribute.Name)
                ? ObjectNames.NicifyVariableName(method.Name)
                : buttonAttribute.Name;

            Method = method;

            _spacing = buttonAttribute.Spacing;

            bool inAppropriateMode = EditorApplication.isPlaying
                ? buttonAttribute.Mode == ButtonMode.EnabledInPlayMode
                : buttonAttribute.Mode == ButtonMode.DisabledInPlayMode;

            _disabled = !(buttonAttribute.Mode == ButtonMode.AlwaysEnabled || inAppropriateMode);
        }

        public void Draw(object[] targets)
        {
            using (new EditorGUI.DisabledScope(_disabled))
            {
                using (new DrawUtility.VerticalIndent(
                    _spacing.ContainsFlag(ButtonSpacing.Before),
                    _spacing.ContainsFlag(ButtonSpacing.After)))
                {
                    DrawInternal(targets);
                }
            }
        }

        internal static Button Create(MethodInfo method, ButtonAttribute buttonAttribute)
        {
            var parameters = method.GetParameters();

            if (parameters.Length == 0)
            {
                return new ButtonWithoutParams(method, buttonAttribute);
            }
            else
            {
                return new ButtonWithParams(method, buttonAttribute, parameters);
            }
        }

        protected abstract void DrawInternal(object[] targets);
    }
}