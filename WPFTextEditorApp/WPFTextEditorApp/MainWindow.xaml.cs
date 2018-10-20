using Microsoft.Win32;
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

using System.IO;

namespace WPFTextEditorApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
        }

        private bool _isModified = false;

        bool _cancelSaveDialog = false;

        OpenFileDialog openFileDialog = new OpenFileDialog();
        SaveFileDialog saveFileDialog = new SaveFileDialog();        

        private void SaveOrNo()
        {
            if (_isModified)
                switch (MessageBox.Show("Do you wand save changes?", "Save", MessageBoxButton.YesNoCancel))
                {
                    case MessageBoxResult.None:
                    case MessageBoxResult.Cancel:
                        return;
                    case MessageBoxResult.Yes:
                        this.SaveFile_Executed(this, null);
                        if (_cancelSaveDialog)
                        {
                            _cancelSaveDialog = false;
                            return;
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        break;
                }
        }

        private void NewFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveOrNo();
            if (_cancelSaveDialog)
                return;
            textBox.Text = string.Empty;
            _isModified = false;
        }

        private void OpenFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveOrNo();
            if (openFileDialog.ShowDialog() == true)
            {
                textBox.Text = File.ReadAllText(openFileDialog.FileName);
                _isModified = false;
            }
        }

        private void SaveFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (openFileDialog.FileName == string.Empty)
                SaveAsFile_Executed(this, null);
            else File.WriteAllText(openFileDialog.FileName, textBox.Text);
        }

        private void SaveAsFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, textBox.Text);
                _isModified = false;
            }
            else _cancelSaveDialog = true;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _isModified = true;
        }

        private void metuItemExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveOrNo();
        }

        private void menuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            About aboutWindow = new About();
            aboutWindow.Show();
        }

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
