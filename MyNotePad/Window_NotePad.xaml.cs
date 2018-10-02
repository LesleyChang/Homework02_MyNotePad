using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class Window_NotePad : Window
    {
        public Window_NotePad()
        {
            InitializeComponent();
        }
        public enum MyBackgroundColor { LightBackground, DarkBackground, CustoStyle }
        public enum MyFileMenuItem { NewFile, OpenFile, SaveFile, SaveNewFile }
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private void ColorMenu_Click(object sender, RoutedEventArgs e)
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
        private void FileMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.Source as MenuItem;
            switch (ToEnum<MyFileMenuItem>(menuItem.Name))
            {
                case MyFileMenuItem.NewFile:
                    ToCreateNewText();
                    break;
                case MyFileMenuItem.OpenFile:
                    ToReadOpenFileDialogFile();
                    break;
                case MyFileMenuItem.SaveFile:
                    ToSaveFile();
                    break;
                case MyFileMenuItem.SaveNewFile:
                    ToSaveNewFile();
                    break;
            }
        }

        private void ToCreateNewText()
        {
            this.openFileDialog1 = new OpenFileDialog();
            this.saveFileDialog1 = new SaveFileDialog();
            textBox1.Text = "";
            this.Title = "MyNotePad --New Text";
        }
        private void ToReadOpenFileDialogFile()
        {
            this.openFileDialog1 = new OpenFileDialog();
            this.openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            this.openFileDialog1.Title = "Open File";
            if (this.openFileDialog1.ShowDialog() == true)
            {
                this.textBox1.Text = "";
                var textLines = File.ReadLines(this.openFileDialog1.FileNames[0]);
                foreach (string s in textLines)
                {
                    this.textBox1.Text += s + Environment.NewLine;
                }
            }
            this.Title = $"MyNotePad --{this.openFileDialog1.FileNames[0]}";
        }
        private void ToSaveFile()
        {
            try
            {
                File.Delete(openFileDialog1.FileNames[0]);
                File.WriteAllLines(openFileDialog1.FileNames[0], textBox1.Text.Split(Environment.NewLine.ToCharArray()));
            }
            catch (Exception)
            {
                ToSaveNewFile();
            }
        }
        private void ToSaveNewFile()
        {
            this.saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog1.FileName = "NewText";
            if (saveFileDialog1.ShowDialog() == true)
            {
                File.WriteAllLines(saveFileDialog1.FileName, textBox1.Text.Split(Environment.NewLine.ToCharArray()));
            }
            this.Title = $"MyNotePad --{this.saveFileDialog1.FileName}";
        }

        public T ToEnum<T>(string enumValue) where T : struct, IConvertible
        {
            return (T)Enum.Parse(typeof(T), enumValue);
        }
    }
}
