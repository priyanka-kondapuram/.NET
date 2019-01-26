using GameData.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace GameData.Tools
{
    /// <summary>
    /// This class inherits ValidationRule for data validations.
    /// </summary>
    public class ValidationRules : ValidationRule
    {
        #region Public Constructors

        public ValidationRules()
        {
        }

        #endregion Public Constructors

        #region Public Properties
        public RangeAttribute RangeAttribute { get; set; }
        public Type PropertyType { get; set; }
        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Used to validate the user input.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string obj)
            {
                if (PropertyType == typeof(Point))     //Setting error messages for Point types
                {
                    try
                    {
                        string[] coords = obj.Split(',');
                        Point point = new Point(int.Parse(coords[0]), int.Parse(coords[1]));
                    }
                    catch
                    {
                        return new ValidationResult(false, "Ivalid Coordinates!");
                    }
                }
                if (PropertyType == typeof(int)) //Setting error messages for Int types
                {
                    try
                    {
                        Convert.ToInt32(obj);
                    }
                    catch (OverflowException e)
                    {
                        return new ValidationResult(false, "Number is too large");
                    }
                    catch (Exception e)
                    {
                        return new ValidationResult(false, "Value must be an integer");
                    }
                }
                if (PropertyType == typeof(float)) //Setting error messages for Float types
                {
                    try
                    {
                        if (Convert.ToDouble(obj) > RangeAttribute.Maximum || Convert.ToDouble(obj) < RangeAttribute.Minimum)
                        {
                            return new ValidationResult(false, $"Value must be between {RangeAttribute.Minimum} - {RangeAttribute.Maximum}");
                        }
                    }
                    catch (Exception e)
                    {
                        var j = e.GetType().Name;
                        var k = e.GetType().FullName;
                        return new ValidationResult(false, "Value must be a number");
                    }
                }
            }

            return ValidationResult.ValidResult;
        }

        #endregion Public Methods
    }
}