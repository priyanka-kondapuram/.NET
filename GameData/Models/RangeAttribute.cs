using System;

namespace GameData.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RangeAttribute : Attribute
    {
        #region Public Constructors

        public RangeAttribute(double min, double max)
        {
            Minimum = min;
            Maximum = max;
        }

        #endregion Public Constructors

        #region Public Properties
        public double Minimum { get; set; }
        public double Maximum { get; set; }
        #endregion Public Properties
    }
}