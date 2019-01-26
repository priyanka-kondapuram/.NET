using GameData.Models;
using GameData.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace GameData.Tools
{
    /// <summary>
    /// This Class contains various extension methods for 'PropertyInfo'.
    /// </summary>
    public static class Extensions
    {
        #region Public Methods
        /// <summary>
        /// Creates and returns Framework Elements based on the type of the property. 
        /// </summary>
        public static bool GetUserControls(this PropertyInfo property, int titleWidth, out FrameworkElement element, out string title)
        {
            var description = property.GetDescription();
            var displayName = property.GetDisplayName() ?? property.Name;
            var propertyType = property.PropertyType;
            var propertyName = property.Name;
            if (propertyType == typeof(ObservableCollection<FrameworkElement>))
            {
                title = null;
                element = null;
                return false;
            }
            if (propertyType.IsEnum)
            {
                var uc = new CustomComboBox();
                uc.UCTitle.Text = displayName;
                uc.UCTitle.MinWidth = titleWidth * 10;

                if (description != null)
                {
                    uc.UCTitle.ToolTip = uc.UCValue.ToolTip = description;
                }
                uc.UCValue.ItemsSource = Enum.GetValues(property.PropertyType);
                uc.UCValue.SetBinding(ComboBox.SelectedValueProperty, propertyName);
                element = uc;
                title = displayName;
                return true;
            }
            else if (propertyType == typeof(bool))
            {
                var uc = new CustomCheckBox();
                uc.UCTitle.Text = displayName;
                uc.UCTitle.MinWidth = titleWidth * 10;

                if (description != null)
                {
                    uc.UCTitle.ToolTip = uc.UCValue.ToolTip = description;
                }
                uc.UCValue.SetBinding(CheckBox.IsCheckedProperty, propertyName);
                title = displayName;
                element = uc;
                return true;
            }
            else
            {
                var uc = new CustomTextBox();
                uc.UCTitle.Text = displayName;
                uc.UCTitle.MinWidth = titleWidth * 10;

                if (description != null)
                {
                    uc.UCTitle.ToolTip = description;
                    uc.UCValue.ToolTip = description;
                }

                var binding = new Binding(propertyName);

                property.GetRange(out double minRange, out double maxRange);
                binding.ValidationRules.Add(new ValidationRules { RangeAttribute = new RangeAttribute(minRange, maxRange), PropertyType = propertyType });

                uc.UCValue.SetBinding(TextBox.TextProperty, binding);
                title = displayName;
                element = uc;
                return true;
            }
        }

        /// <summary>
        /// Returns the maximum length of the property names. 
        /// </summary>
        public static int GetMaximumTextValue(this PropertyInfo[] properties)
        {
            var result = 0;
            foreach (var item in properties)
            {
                var name = item.Name;
                if (item.PropertyType != typeof(ObservableCollection<FrameworkElement>) && name.Length > result)
                {
                    result = name.Length;
                }
            }
            return result;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Checks for the description attribute and return the description.
        /// </summary>
        private static string GetDescription(this PropertyInfo property)
        {
            bool hasDescription = Attribute.IsDefined(property, typeof(DescriptionAttribute)); //Checking if the property has DescriptionAttribute.

            if (hasDescription)
            {
                return ((DescriptionAttribute)(Attribute.GetCustomAttribute(property, typeof(DescriptionAttribute)))).Description; //Storing the description.
            }
            return null;
        }

        /// <summary>
        /// Checks for the range attribute and return the range values.
        /// </summary>
        private static void GetRange(this PropertyInfo property, out double minRange, out double maxRange)
        {
            bool hasRange = Attribute.IsDefined(property, typeof(RangeAttribute)); //Checking if the property has DescriptionAttribute.

            if (hasRange)
            {
                minRange = ((RangeAttribute)(Attribute.GetCustomAttribute(property, typeof(RangeAttribute)))).Minimum;
                maxRange = ((RangeAttribute)(Attribute.GetCustomAttribute(property, typeof(RangeAttribute)))).Maximum;
                return;
            }
            minRange = 0.0;
            maxRange = 0.0;
        }

        /// <summary>
        /// Checks for the display name attribute and return the display name.
        /// </summary>
        private static string GetDisplayName(this PropertyInfo property)
        {
            bool hasDisplayName = Attribute.IsDefined(property, typeof(DisplayNameAttribute)); //Checking if the property has DisplayName Attribute.
            if (hasDisplayName)
            {
                return ((DisplayNameAttribute)(Attribute.GetCustomAttribute(property, typeof(DisplayNameAttribute)))).DisplayName;
            }
            return null;
        }

        #endregion Private Methods
    }
}