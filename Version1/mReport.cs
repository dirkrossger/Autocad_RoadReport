using System;
using System.Collections.Generic;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Geometry;

namespace RoadReport
{
    internal class mReport
    {
        public static List<Datas> GetReportList(SelectionSet ss)
        {
            List<Datas> result = new List<Datas>();

            try
            {
                Database acWdb = HostApplicationServices.WorkingDatabase;
                using (Transaction tr = acWdb.TransactionManager.StartTransaction())
                {
                    for (int i = 0; i < ss.Count; i++)
                    {
                        Entity ent = tr.GetObject(ss[i].ObjectId, OpenMode.ForRead) as Entity;
                        double area = mArea.GetArea(ent);
                        double length = mLength.GetLength(ent as Curve);

                        result.Add(new Datas { Handle = ent.Handle.ToString(), Type = ent.GetType().Name, Layer = ent.Layer, Area = area, Length = length });
                    }
                    tr.Commit();

                    return result;
                }

            }
            catch (System.Exception ex) { }

            return null;
        }

        public static List<Datas> GetReport()
        {
            Database db = HostApplicationServices.WorkingDatabase;

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {

                SelectionSet Sset = cSelection.GetSelection();
                List<Datas> Report = mReport.GetReportList(Sset);

                try
                {
                    //mReport.ShowReport(Report);
                    return Report;
                }
                catch
                {
                }

                tr.Commit();
            }

            return null;
        }

        public static void ShowReport(List<Datas> report)
        {
            Document acadDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = acadDoc.Editor;

            for (int i = 0; i < report.Count; i++)
            {
                ed.WriteMessage("\nRow {0}: <Layer: {1}> <Type: {2}> <Area: {3}> <Length {4}>",
                      i, report[i].Layer, report[i].Type, report[i].Area, report[i].Length);
            }
        }


    }
}
