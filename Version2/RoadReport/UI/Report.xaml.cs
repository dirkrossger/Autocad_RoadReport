using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RoadReport.UI
{
    /// <summary>
    /// Interaktionslogik für Report.xaml
    /// </summary>
    public partial class Report : UserControl
    {
        #region -> Events
        #region --> DoubleClickEvent
        public delegate void DoubleClickEventHandler(object sender, DoubleClickEventArgs e);
        public event DoubleClickEventHandler OnDoubleClick;
        protected virtual void DoOnDoubleClick(DoubleClickEventArgs e)
        {
            if (OnDoubleClick != null)
                OnDoubleClick(this, e);
        }
        public class DoubleClickEventArgs : EventArgs
        {
            public Objects.Data ClickedDataObject { get; set; }
            
            public DoubleClickEventArgs(Objects.Data _obj)
            {
                ClickedDataObject = _obj;
            }


        }
        #endregion <-- DoubleClickEvent

        #region --> RefreshEvent
        public delegate void RefreshClickEventHandler(object sender, RefreshClickEventArgs e);
        public event RefreshClickEventHandler OnRefreshClick;
        protected virtual void DoOnRefreshClick(RefreshClickEventArgs e)
        {
            if (OnRefreshClick != null)
                OnRefreshClick(this, e);
        }
        public class RefreshClickEventArgs : EventArgs
        {
            public RefreshClickEventArgs()
            {
                // Empty - falls doch irgendwann notwendig
            }


        }
        #endregion <-- RefreshEvent
        
        #endregion <- Events

        public Report(Objects.Datas _daten)
        {
            InitializeComponent();
            mDataGrid.AutoGeneratingColumn += mDataGrid_AutoGeneratingColumn;
            this.DataContext = _daten;
        }

        void mDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor _pd = (PropertyDescriptor)e.PropertyDescriptor;
            DisplayAttribute _da = (DisplayAttribute)_pd.Attributes[typeof(DisplayAttribute)];
            if (_da != null)
            {
                e.Column.Header = _da.Name;
                e.Column.DisplayIndex = _da.Order;
                e.Column.Visibility = _da.AutoGenerateField ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
            }
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((DataGrid)sender).SelectedItem != null)
            {
                DoOnDoubleClick(new DoubleClickEventArgs(((Objects.Data)((DataGrid)sender).SelectedItem)));
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            DoOnRefreshClick(new RefreshClickEventArgs());
        }
    }
}
