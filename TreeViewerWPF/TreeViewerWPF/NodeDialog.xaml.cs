using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MyTree;

namespace TreeViewerWPF
{
    /// <summary>
    /// Логика взаимодействия для NodeDialog.xaml
    /// </summary>
    public partial class NodeDialog : Window
    {
        MainWindow main;
        public NodeDialog()
        {
            InitializeComponent();
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            main = this.Owner as MainWindow;
            main.Tree.Add(main.trenode, tb_AddContent.Text);
            this.Close();
        }

        private void btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            main = this.Owner as MainWindow;
            main.Tree.Remove(main.trenode);
            this.Close();
        }
    }
}
