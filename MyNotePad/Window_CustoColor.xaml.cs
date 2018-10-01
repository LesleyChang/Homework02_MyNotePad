using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace MyNotePad
{
    /// <summary>
    /// Window_CustoColor.xaml 的互動邏輯
    /// </summary>
    public partial class Window_CustoColor : Window
    {
        public Window_CustoColor()
        {
            this.ForeColor = Colors.Black;
            this.BackColor = Colors.White;
            InitializeComponent();
            LoadComboBoxItems();
        }
        public Color ForeColor { get; set; }
        public Color BackColor { get; set; }
        private void LoadComboBoxItems()
        {
            var q = typeof(Colors).GetProperties();

            this.ForegroundCB.ItemsSource = q.ToList();
            this.ForegroundCB.DisplayMemberPath = "Name";
            this.ForegroundCB.SelectedIndex = 7;
            this.BackgroundCB.ItemsSource = q.ToList();
            this.BackgroundCB.DisplayMemberPath = "Name";
            this.BackgroundCB.SelectedIndex = 137;

        }

        private void ForegroundCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox f = sender as ComboBox;
            string[] colorName = f.SelectedValue.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (f.SelectedItem != null)
            {
                this.TestLabel.Foreground = new SolidColorBrush((Color)ColorConverter
                    .ConvertFromString(colorName[1]));
                this.ForeColor = (Color)ColorConverter.ConvertFromString(colorName[1]);
            }

        }


        private void BackgroundCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox f = sender as ComboBox;
            string[] colorName = f.SelectedValue.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (f.SelectedItem != null)
            {
                this.TestLabel.Background = new SolidColorBrush((Color)ColorConverter
                    .ConvertFromString(colorName[1]));
                this.BackColor = (Color)ColorConverter.ConvertFromString(colorName[1]);
            }
        }

        private void OK_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Cancel_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
