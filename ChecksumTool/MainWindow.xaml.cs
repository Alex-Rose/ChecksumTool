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

namespace ChecksumTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public string FilePath
        {
            get { return (string)GetValue(FilePathProperty); }
            set { SetValue(FilePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilePathProperty =
            DependencyProperty.Register("FilePath", typeof(string), typeof(MainWindow));



        public string Hash
        {
            get { return (string)GetValue(HashProperty); }
            set { SetValue(HashProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Hash.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HashProperty =
            DependencyProperty.Register("Hash", typeof(string), typeof(MainWindow));



        public string SourceHash
        {
            get { return (string)GetValue(SourceHashProperty); }
            set { SetValue(SourceHashProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ComprareHash.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceHashProperty =
            DependencyProperty.Register("SourceHash", typeof(string), typeof(MainWindow));



        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            Hash = "Please select a file";
            FilePath = @"C:\Users\Alexandre\Downloads\gpg4win-2.2.3.exe";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hash = Checksum.GetChecksum(HashAlgorithm.SHA1, new[] {FilePath});
            CompareHash();
        }

        private void TextBox_Drop(object sender, DragEventArgs e)
        {
            
        }

        private void TextBox_PreviewDrop(object sender, DragEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            var files = e.Data.GetData(DataFormats.FileDrop) as IEnumerable<string>;
            FilePath = files.FirstOrDefault();
            DropZone.Visibility = Visibility.Hidden;
        }

        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            DropZone.Visibility = Visibility.Visible;
        }

        private void Window_DragLeave(object sender, DragEventArgs e)
        {
            DropZone.Visibility = Visibility.Hidden;
        }

        private void CompareHash()
        {
            if (!string.IsNullOrEmpty(SourceHash))
            {
                if (Hash.ToLower() == SourceHash.ToLower().Trim())
                {
                    ComparisonResult.Text = "Hash match";
                    ComparisonResult.Foreground = Brushes.Green;
                }
                else
                {
                    ComparisonResult.Text = "Hash mismatch";
                    ComparisonResult.Foreground = Brushes.Red;
                }
            }
        }

        private void ComparisonResult_KeyUp(object sender, KeyEventArgs e)
        {
            CompareHash();
        }
    }
}
