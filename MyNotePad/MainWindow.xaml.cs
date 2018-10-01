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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyNotePad
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public enum MyBackgroundColor { LightBackground, DarkBackground, CustoStyle }
        
        private void ColorMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = e.Source as MenuItem;
            switch (ToEnum<MyBackgroundColor>(item.Name))
            {
                case MyBackgroundColor.DarkBackground:
                    this.textBox1.Background = new SolidColorBrush(Colors.Black);
                    this.textBox1.Foreground = new SolidColorBrush(Colors.White);
                    break;
                case MyBackgroundColor.LightBackground:
                    this.textBox1.Background = new SolidColorBrush(Colors.White);
                    this.textBox1.Foreground = new SolidColorBrush(Colors.Black);
                    break;
                case MyBackgroundColor.CustoStyle:
                    Window_CustoColor w = new Window_CustoColor();

                    if (w.ShowDialog() == true)
                    {
                        this.textBox1.Foreground = new SolidColorBrush(w.ForeColor);
                        this.textBox1.Background = new SolidColorBrush(w.BackColor);
                    }
                    break;
            }
           

        }

        public T ToEnum<T>( string enumValue) where T : struct, IConvertible
        {
            return (T)Enum.Parse(typeof(T), enumValue);
        }
    }
}
