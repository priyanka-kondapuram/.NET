using GameData.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace GameData.View
{
    public static class LootEditorCommands
    {
        #region Public Fields
        public static RoutedUICommand ConvertToShield = new RoutedUICommand("Convert to Shield", "ConvertToShield", typeof(LootEditor));
        #endregion Public Fields
    }

    /// <summary>
    /// Interaction logic for LootEditor.xaml
    /// </summary>
    public partial class LootEditor : Window
    {
        #region Public Fields

        public static readonly Loot MagicAxe = new Loot()
        {
            Type = ItemType.Axe,
            SocketCount = 2,
            Damage = 100,
            Armor = 0,
            CanSalvage = true,
        };

        #endregion Public Fields

        #region Public Constructors

        static LootEditor()
        {
            CommandManager.RegisterClassCommandBinding(
                typeof(LootEditor),
                new CommandBinding(LootEditorCommands.ConvertToShield,
                new ExecutedRoutedEventHandler(OnConvertToShieldCommand)));
        }

        public LootEditor()
        {
            InitializeComponent();
            this.DataContext = MagicAxe;
        }

        #endregion Public Constructors

        #region Private Methods

        // this is the instance of loot that you will display in your property grid implementation
        // (in reality - it wouldn't be static but would come from some designer authored item data)
        private static void OnConvertToShieldCommand(object sender, ExecutedRoutedEventArgs e)
        {
            Loot loot = e.Parameter as Loot;

            if (loot != null)
            {
                loot.Type = ItemType.Shield;
                loot.Armor = 100;
                loot.SocketCount = 0;
                loot.Damage = 0;
                loot.CanSalvage = false;
            }
        }

        #endregion Private Methods
    }
}