using System;
using System.Collections.Generic;

namespace MyTree
{
    /// <summary>
    /// Класс представляет элемент дерева.
    /// </summary>
    /// <typeparam name="TypeNode">Любой тип</typeparam>
    public class TreeNode<TypeNode>
    {
        /// <summary>
        /// Конструктор, инициализирующий элемент переданным значением.
        /// </summary>
        /// <param name="value">значение элемента</param>
        public TreeNode(TypeNode value)
        {
            Value = value;
            Children = new List<TreeNode<TypeNode>>();
        }

        /// <summary>
        /// Коллекция потомков элемента.
        /// </summary>
        public List<TreeNode<TypeNode>> Children { get; internal set; }

        /// <summary>
        /// Представляет родительский элемент.
        /// </summary>
        public TreeNode<TypeNode> Parent { get; internal set; }

        /// <summary>
        /// Представляет значение элемента.
        /// </summary>
        public TypeNode Value { get; internal set; }

    }

    /// <summary>
    /// Представляет дерево
    /// </summary>
    /// <typeparam name="T">Любой тип</typeparam>
    public class MyTree<T>
    {
        /// <summary>
        /// Корень дерева
        /// </summary>
        public TreeNode<T> Head { get; private set; } 

        /// <summary>
        ///Количество элементов дерева
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// Добавление нового элемента дерева
        /// </summary>
        /// <param name="value">значение элемента</param>
        /// <param name="parent">родительский элемент или NULL, если добавляется корень дерева</param>
        public void Add(TreeNode<T> parent, T value)
        {
            if (parent == null)
            {
                Head = new TreeNode<T>(value); //Если родитель отсутствует, то мы создаем новый корень дерева (новое дерево).
                Length++;
            }
            else
            {
                if (Head == null) throw new InvalidOperationException("Дерево не имеет элементов"); //Если мы пытаемся в пустое дерево записать новый элемент с указанным (не пустым) родителем - выдаем ошибку.
                TreeNode<T> newnode = new TreeNode<T>(value);
                Length++;
                newnode.Parent = parent;
                parent.Children.Add(newnode);
            }
        }

        /// <summary>
        /// Удаление элемента
        /// </summary>
        /// <param name="removed_item">удаляемый элемент</param>
        public void Remove(TreeNode<T> removed_item)
        {
            if (removed_item == null) throw new ArgumentNullException("Удаляемый элемент не может быть NULL");
            if (removed_item.Parent == null) //Если удаляемый элемент - корень дерева.
            {
                Head = null;
                for (int i = 0; i < removed_item.Children.Count; i++) //В цикле мы первого наследника удаляемого элемента ставим на место корня и перезаписываем наследников.
                {
                    if (i == 0)
                    {
                        Head = removed_item.Children[0];
                        removed_item.Children[0].Parent = null;
                    }
                    else
                    {
                        removed_item.Children[i].Parent = Head;
                        Head.Children.Add(removed_item.Children[i]);
                    }
                }
            }
            else //Если удаляемый элемент имеет родителя.
            {
                for (int i = 0; i < removed_item.Parent.Children.Count; i++) //Удаляем элемент из коллекции наследников родителя
                {
                    if (removed_item.Parent.Children[i].GetHashCode() == removed_item.GetHashCode()) removed_item.Parent.Children.RemoveAt(i);
                }
                TreeNode<T> newnode=null;
                for (int i = 0; i < removed_item.Children.Count; i++) //В новый узел записываем первого наследника удаляемого элемента и добавляем к коллекции нового узла наследников удаляемого элемента.
                {
                    if (i == 0)
                    {
                        
                        newnode = removed_item.Children[0];
                        newnode.Parent = removed_item.Parent;
                    }
                    else
                    {
                        newnode.Children.Add(removed_item.Children[i]);
                        removed_item.Children[i].Parent = newnode;
                    }
                }
                if (newnode != null) removed_item.Parent.Children.Add(newnode); //Если удаляемый узел был не крайним в дереве - записываем его в коллекцию наследников родителя.
            }
        }

        /// <summary>
        /// Очистка дерева
        /// </summary>
        public void Clear()
        {
            Head = null;
            Length = 0;
        }

    }
}
