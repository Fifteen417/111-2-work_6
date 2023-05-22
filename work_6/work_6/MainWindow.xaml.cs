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

namespace work_6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // 清除rtbText的內容
            text.Document.Blocks.Clear();
            // 設定字型下拉選單的選單內容，存取你的電腦裡面的字型庫，將你安裝的字型清單都放進去
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            // 設定字體大小下拉選單的選單內容，設定8`72的數字，這要用來設定字體大小
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        }

        private void open_btn_click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // 跟記事本範例程式類似，不過要改成過濾為RTF檔案格式
            dlg.Filter = "RTF文件 (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(text.Document.ContentStart, text.Document.ContentEnd);
                // DataFormats 檔案格式也要設定為RTF檔案格式
                range.Load(fileStream, DataFormats.Rtf);
            }
        }

        private void save_btn_click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Filter = "RTF文件 (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                TextRange range = new TextRange(text.Document.ContentStart, text.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
            }
        }

        private void cmbFontFamily_sc(object sender, SelectionChangedEventArgs e)
        {
            // 判斷式：必須要有選擇項目，才會做文字格式改變
            if (cmbFontFamily.SelectedItem != null)
            // 將rtbText豐富文字框所選的項目，套用所設定的字型
            text.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);

        }

        private void cmbFontSize_sc(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontSize.SelectedItem != null)
            // 將rtbText豐富文字框所選的項目，套用所設定的字體大小
            text.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.SelectedItem);
        }

        private void bold_btn_click(object sender, RoutedEventArgs e)
        {
            // 取得你目前選取的文字，取得文字的字體粗細
            object temp = text.Selection.GetPropertyValue(Inline.FontWeightProperty);

            if ((temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold)))
                // 判斷：文字要有設定格式、設定為粗體，改變文字成為原來的粗細程度
                text.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Normal);
            else
                // 如果文字不是粗體，則改為粗體
                text.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Bold);
        }

        private void italic_btn_click(object sender, RoutedEventArgs e)
        {
            // 取得你目前選取的文字，取得文字的字體樣式（斜體或非斜體）
            object temp = text.Selection.GetPropertyValue(Inline.FontStyleProperty);

            if ((temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic)))
                // 判斷：文字要有設定格式、設定為斜體，改變文字成為原來的正體
                text.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Normal);
            else
                // 如果文字為正體，則改為斜體
                text.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Italic);
        }

        private void underline_btn_click(object sender, RoutedEventArgs e)
        {
            // 取得你目前選取的文字，取得文字的字體樣式（字體裝飾）
            object temp = text.Selection.GetPropertyValue(Inline.TextDecorationsProperty);

            if ((temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline)))
                // 判斷：文字要有設定格式、設定為底線，將文字移除底線
                text.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            else
                // 如果文字沒有底線，則增加底線
                text.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
        }
    }
}
