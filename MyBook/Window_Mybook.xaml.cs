using AWModel;
using PhotoDataModel_V2;
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
using System.Windows.Shapes;

namespace MyBook
{
    /// <summary>
    /// Window_Mybook.xaml 的互動邏輯
    /// </summary>
    public partial class Window_Mybook : Window
    {
        public Window_Mybook()
        {
            InitializeComponent();
        }
        AdventureWorksEntities dbContext = new AdventureWorksEntities();
        enum pageBtnName { FirstPageBtn, PreviousPageBtn, NextPageBtn, LastPageBtn }
        public int currentPage { get; set; }

        private void Flicker_Btn_Click(object sender, RoutedEventArgs e)
        {
            List<PhotoDataItem> items = PhotoDataSource.Search("Microsoft", 15);
            this.book1.Items.Clear();
            foreach (var item in items)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(item.ImagePath, UriKind.Absolute);   //轉型以取得Image.Source
                bitmap.EndInit();
                Image img = new Image();
                img.Margin = new Thickness(5);
                img.Stretch = Stretch.Fill;
                img.Source = bitmap;
                this.book1.Items.Add(img);
            }
        }
        private void AWEntity_Btn_Click(object sender, RoutedEventArgs e)
        {
            var q = dbContext.ProductPhoto;

            this.book1.Items.Clear();
            foreach (var p in q)
            {
                MemoryStream ms = new MemoryStream(p.LargePhoto);
                ms.Seek(0, SeekOrigin.Begin);
                var bitmap = new BitmapImage();
                bitmap.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.BeginInit();
                bitmap.StreamSource = ms;
                bitmap.EndInit();
                Image img = new Image();
                img.Margin = new Thickness(5);
                img.Stretch = Stretch.Fill;
                img.Source = bitmap;
                this.book1.Items.Add(img);
            }
        }

        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            switch (ToEnum<pageBtnName>(btn.Name))
            {
                case pageBtnName.FirstPageBtn:
                    TurnToFirstPage();
                    break;
                case pageBtnName.LastPageBtn:
                    TurnToLastPage();
                    break;
                case pageBtnName.NextPageBtn:
                    this.book1.AnimateToNextPage(true, 700);
                    currentPage = currentPage < this.book1.Items.Count - 1 ? currentPage + 1 : currentPage;
                    break;
                case pageBtnName.PreviousPageBtn:
                    this.book1.AnimateToPreviousPage(true, 700);
                    currentPage = currentPage > 0 ? currentPage - 1 : currentPage;
                    break;
            }

        }
        
        private void TurnToLastPage()
        {
            if (currentPage < (this.book1.Items.Count -1))
            {
                for (int i = 0; i < this.book1.Items.Count -1 - currentPage; i++)
                {
                    this.book1.AnimateToNextPage(true, 200);
                }
            }
            currentPage = this.book1.Items.Count - 1;
        }

        private void TurnToFirstPage()
        {
            if (currentPage > 0)
            {
                for (int i = 0; i < currentPage; i++)
                {
                    this.book1.AnimateToPreviousPage(true, 200);
                }
            }
            currentPage = 0;
        }

        public T ToEnum<T>(string enumValue) where T : struct, IConvertible
        {
            return (T)Enum.Parse(typeof(T), enumValue);
        }

        
    }
}
