using System;
using System.Collections.Generic;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;

namespace RoadReport
{
    class cSelection
    {
        public string Type { get; set; }
        public string Handle { get; set; }
        public string Layer { get; set; }
        public double Area { get; set; }
        public double Length { get; set; }

        public static SelectionSet GetSelection()
        {
            Document acadDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = acadDoc.Editor;

            try
            {
                TypedValue[] filList = { new TypedValue(0, "Polyline,LwPolyline,Spline,Arc,Line,Circle,Ellipse,Hatch") };
                SelectionFilter filter = new SelectionFilter(filList);

                PromptSelectionOptions pso = new PromptSelectionOptions();
                pso.MessageForAdding = "\nSelect Object to export report:";
                PromptSelectionResult pr = ed.GetSelection(pso, filter);

                SelectionSet SSet = null;

                Database acWdb = HostApplicationServices.WorkingDatabase;
                using (Transaction tr = acWdb.TransactionManager.StartTransaction())
                {

                    switch (pr.Status)
                    {
                        case PromptStatus.OK:
                            SSet = pr.Value;
                            break;

                        case PromptStatus.Cancel:
                            ed.WriteMessage("\nSelect canceled");
                            return null;

                        case PromptStatus.Error:
                            ed.WriteMessage("\nWrong Choice");
                            return null;

                        default:
                            ed.WriteMessage("\nNothing select!");
                            break;

                    }
                    tr.Commit();

                    return SSet;
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        static public void ZoomToEntity()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            PromptEntityOptions peo = new PromptEntityOptions("\nSelect an entity:");
            PromptEntityResult per = ed.GetEntity(peo);

            if (per.Status != PromptStatus.OK)
                return;

            Extents3d ext;
            
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                Entity ent = (Entity)tr.GetObject(per.ObjectId, OpenMode.ForRead);
                ext = ent.GeometricExtents;
                tr.Commit();
            }

            ext.TransformBy(ed.CurrentUserCoordinateSystem.Inverse());

            ZoomWin(ed, ext.MinPoint, ext.MaxPoint);
        }

        private static void ZoomWin(Editor ed, Point3d min, Point3d max)
        {
            Point2d min2d = new Point2d(min.X, min.Y);
            Point2d max2d = new Point2d(max.X, max.Y);

            ViewTableRecord view = new ViewTableRecord();

            view.CenterPoint = min2d + ((max2d - min2d) / 2.0);
            view.Height = max2d.Y - min2d.Y;
            view.Width = max2d.X - min2d.X;
            ed.SetCurrentView(view);
        }

    }
}
