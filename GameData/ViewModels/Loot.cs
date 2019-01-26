using GameData.Models;
using GameData.Tools;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace GameData.ViewModels
{
    public enum ItemType
    {
        Sword,
        Axe,
        Helm,
        Shield,
    }

    public class Loot : NotifyProperty
    {
        #region Private Fields
        private float m_armor;

        private bool m_canSalvage;

        private float m_damage;

        private ItemType m_itemType;

        private int m_sockets;

        private int m_value = 1;
        #endregion Private Fields

        #region Public Constructors

        public Loot()
        {
            OCUserControls = new ObservableCollection<FrameworkElement>();
            var titleWidth = GetType().GetProperties().GetMaximumTextValue();

            foreach (var property in GetType().GetProperties())   //Dynamically creates all the user controls based on property types.
            {
                if (property.GetUserControls(titleWidth, out FrameworkElement control, out string title))
                {
                    OCUserControls.Add(control);
                }
            }
        }

        #endregion Public Constructors

        #region Public Properties
        public ObservableCollection<FrameworkElement> OCUserControls { get; set; }

        [Description("How much incoming damage is reduced by (non used for non-armor item types)")]
        [Range(0, 1000)]
        public float Armor
        {
            get { return m_armor; }
            set
            {
                SetField(ref m_armor, value);
            }
        }

        [Description("Can this item be salvaged into crafting components?")]
        [DisplayName("Salvageable")]
        public bool CanSalvage
        {
            get { return m_canSalvage; }
            set
            {
                SetField(ref m_canSalvage, value);
            }
        }

        [Description("How much damage this weapon inflicts per blow (not used for non-weapon types)")]
        [Range(0, 2000)]
        public float Damage
        {
            get { return m_damage; }
            set
            {
                SetField(ref m_damage, value);
            }
        }

        public int SocketCount
        {
            get { return m_sockets; }
            set
            {
                SetField(ref m_sockets, value);
            }
        }

        public ItemType Type
        {
            get { return m_itemType; }
            set
            {
                SetField(ref m_itemType, value);
            }
        }

        [Range(1, 5000)]
        [Description("Value of this item in gold coins if sold to a vendor")]
        [DisplayName("Value (Gold)")]
        public int Value
        {
            get { return m_value; }
            set
            {
                SetField(ref m_value, value);
            }
        }

        #endregion Public Properties
    }
}