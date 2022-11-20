using System.Windows;
using System.Windows.Controls;

namespace CRUD_WPF
{
    /// <summary>
    /// Lógica interna para CadastroOng.xaml
    /// </summary>
    public partial class CadastroOng : Window
    {
        public CadastroOng()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
