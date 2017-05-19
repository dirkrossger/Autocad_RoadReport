using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace RoadReport.UI
{
    internal static class panelHandler
    {
        static string _panelTitle = "Report";
        public static Report CurrentControl;
        public static PaletteSet _CurrentPanel;
        public static Palette MyPaletteSet;


        public static void PanelOpen()
        {
            if (MyPaletteSet == null)
            {
                CurrentControl = new Report(null);
                #region -> Events definieren
                CurrentControl.OnDoubleClick += CurrentControl_OnDoubleClick;
                CurrentControl.OnRefreshClick += CurrentControl_OnRefreshClick;
                #endregion <- Events definieren
                ElementHost _eh = new ElementHost();
                _eh.Dock = DockStyle.Fill;
                _eh.Child = CurrentControl;

                _CurrentPanel = new PaletteSet(_panelTitle, "mReport", new Guid("81DE2C7E-51BD-4D5C-8E2E-A06C8B6E6027"));
                _CurrentPanel.Visible = true;
                try
                {
                    MyPaletteSet = _CurrentPanel.Add(_panelTitle, _eh);
                    MyPaletteSet.PaletteSet.Visible = true;
                }
                catch
                { }

            }
            else
                MyPaletteSet.PaletteSet.Visible = !MyPaletteSet.PaletteSet.Visible;
        }

        static void CurrentControl_OnRefreshClick(object sender, Report.RefreshClickEventArgs e)
        {
            CurrentControl.DataContext = new Objects.Datas();
        }

        static void CurrentControl_OnDoubleClick(object sender, Report.DoubleClickEventArgs e)
        {
            e.ClickedDataObject.ZoomTo();
        }
    }
}
