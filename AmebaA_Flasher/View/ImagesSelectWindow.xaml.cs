using System.Windows;

namespace AmebaA_Flasher.View
{
    public partial class ImagesSelectWindow : Window
    {
        public ImagesSelectWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
