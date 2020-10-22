﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;

namespace SportsCentre.WPF.Controls.CheckboxList
{
    public class CheckableObservableCollection<T> : ObservableCollection<CheckWrapper<T>>
    {
        private ListCollectionView _selected;

        public CheckableObservableCollection()
        {
            _selected = new ListCollectionView(this);
            _selected.Filter = delegate (object checkObject) {
                return ((CheckWrapper<T>)checkObject).IsChecked;
            };
        }

        public void Add(T item)
        {
            this.Add(new CheckWrapper<T>(this) { Value = item });
        }

        public ICollectionView SelectedItems
        {
            get { return _selected; }
        }

        public void Refresh()
        {
            _selected.Refresh();
        }
    }
}
