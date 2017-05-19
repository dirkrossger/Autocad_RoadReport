using System;
using System.Collections.Generic;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;
//using Autodesk.AutoCAD.Interop;

namespace RoadReport
{
    public class cCommands
    {
        private Form1 _Form1;

        [CommandMethod("xx")]
        public void Start()
        {
            _Form1 = new Form1();
            System.Windows.Forms.DialogResult result = _Form1.ShowDialog();
        }

        [CommandMethod("xLen")]
        public void GetLength()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            try
            {
                PromptEntityResult selRes = ed.GetEntity("\nPick a Entity:");
                if (selRes.Status == PromptStatus.OK)
                {
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        Entity ent = (Entity)tr.GetObject(selRes.ObjectId, OpenMode.ForRead);
                        ed.WriteMessage("\nThe length of the {0} is: {1}", ent.GetType().Name, mLength.GetLength(ent as Curve));
                        tr.Commit();
                    }
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(ex.ToString());
            }
        }

        [CommandMethod("xArea")]
        public void GetArea()
        {
            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;
            try
            {
                PromptEntityResult selRes = ed.GetEntity("\nPick a Entity:");
                if (selRes.Status == PromptStatus.OK)
                {
                    using (Transaction tr = db.TransactionManager.StartTransaction())
                    {
                        Entity ent = (Entity)tr.GetObject(selRes.ObjectId, OpenMode.ForRead);
                        ed.WriteMessage("\nThe area of the {0} is: {1}", ent.GetType().Name, mArea.GetArea(ent));
                        tr.Commit();
                    }
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage(ex.ToString());
            }
        }

        [CommandMethod("xReport")]
        public static void GetReport()
        {
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                SelectionSet Sset = cSelection.GetSelection();
                Objects.Datas _daten = new Objects.Datas(Sset, tr);
                //List<cSelection> Report = mReport.GetReportList(Sset);

                //try
                //{
                //    mReport.ShowReport(Report);
                //}
                //catch
                //{
                //}

                //tr.Commit();
            }
        }
        
        [CommandMethod("ZE")]
        public void ZoomEntity()
        {
            cSelection.ZoomToEntity();
        }

    }
}