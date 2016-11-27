using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MyTree;

namespace TreeViewerWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public MyTree<string> Tree = new MyTree<string>();  //создания класса дерева
        public TreeNode<string> trenode; //создания элемента дерева

        public MainWindow()
        {
            InitializeComponent();
            menu.IsEnabled = true;
        }

        //Метод вызывающий диалоговое окно при нажатии на элемент дерева
        public void NodeDialog(object sender, MouseButtonEventArgs e)
        {
            Button btn = sender as Button;
            trenode = btn.Tag as TreeNode<string>;
            NodeDialog DialogWindow = new NodeDialog();
            DialogWindow.Owner = this;
            DialogWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            DialogWindow.ShowDialog();
            grid.Children.Clear();
            if (Tree.Head != null) PrintTree<string>(Tree.Head, 0, 0);
        }

        //Метод отрисовки дерева при добавлении элемнта
        public void PrintTree<T>(TreeNode<T> tree, double StartX, double StartY)
        {
            int number_d = 0;
            Button tb = new Button();
            tb.Content = Convert.ToString(tree.Value);
            tb.Height = 30;
            tb.Width = 50;
            tb.Tag = tree;
            tb.HorizontalAlignment = HorizontalAlignment.Left;
            tb.VerticalAlignment = VerticalAlignment.Top;
            tb.Margin = new Thickness(StartX, StartY, 0, 0);
            tb.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(this.NodeDialog);
            grid.Children.Add(tb);
            for (int i = 0; i < tree.Children.Count; i++)
            {

                if (i != 0)
                {
                    number_d += CountNodes(tree.Children[i - 1]);
                    LineGeometry line = new LineGeometry();
                    line.StartPoint = new Point((StartX + 25), (StartY + 30));
                    line.EndPoint = new Point((StartX + 60), (StartY + 50 * (i + 1) + number_d * 50 + 15));
                    System.Windows.Shapes.Path ptline = new System.Windows.Shapes.Path();
                    ptline.Stroke = Brushes.Black;
                    ptline.Data = line;
                    grid.Children.Add(ptline);
                    PrintTree<T>(tree.Children[i], StartX + 60, StartY + 50 * (i + 1) + number_d * 50);
                }
                else
                {
                    LineGeometry line = new LineGeometry();
                    line.StartPoint = new Point((StartX + 25), (StartY + 30));
                    line.EndPoint = new Point((StartX + 60), (StartY + 75));
                    System.Windows.Shapes.Path ptline = new System.Windows.Shapes.Path();
                    ptline.Stroke = Brushes.Black;
                    ptline.Data = line;
                    grid.Children.Add(ptline);
                    PrintTree<T>(tree.Children[i], StartX + 60, StartY + 50);
                }
            }
        }

        private int CountNodes<T>(TreeNode<T> node)
        {
            int sum = 0;
            foreach (var v in node.Children)
            {
                sum += CountNodes(v);
            }
            return sum += node.Children.Count;
        }

        public int WidthTree<T>(TreeNode<T> tree)
        {
            int count = tree.Children.Count;
            List<int> list = new List<int>();
            foreach (var v in tree.Children)
            {
                SearchWidthTree<T>(v, list, 1);
            }
            foreach (var v in list)
            {
                if (count < v) count = v;
            }
            return count;
        }

        private void SearchWidthTree<T>(TreeNode<T> tree, List<int> list, int level)
        {
            if (list.Count < level) list.Add(0);
            list[level - 1] += tree.Children.Count;
            foreach (var v in tree.Children)
            {
                SearchWidthTree<T>(v, list, level + 1);
            }
        }

        
        //Создания корня дерева
        private void CreateNew_Click(object sender, RoutedEventArgs e)
        {
            trenode = null;
            NodeDialog DialogWindow = new NodeDialog();
            DialogWindow.Owner = this;
            DialogWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            DialogWindow.ShowDialog();
            grid.Children.Clear();
            if (Tree.Head != null) PrintTree<string>(Tree.Head, 0, 0);
        }
        //Очистка формы от дерева
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите очистить Дерево?", "Очистка Дерева", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Tree.Clear();
                grid.Children.Clear();
                trenode = null;
            }
        }
    }
}
