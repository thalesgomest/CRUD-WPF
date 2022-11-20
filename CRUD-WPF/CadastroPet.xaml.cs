using System;
using System.Linq;
using System.Windows;

namespace CRUD_WPF
{
    /// <summary>
    /// Lógica interna para CadastroPet.xaml
    /// </summary>
    public partial class CadastroPet : Window
    {
        public CadastroPet()
        {
            InitializeComponent();
            SexoComboBox.ItemsSource = Enum.GetValues(typeof(Model.Sexo)).Cast<Model.Sexo>();
            PorteComboBox.ItemsSource = Enum.GetValues(typeof(Model.Porte)).Cast<Model.Porte>();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
