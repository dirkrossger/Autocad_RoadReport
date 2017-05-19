using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadReport.Objects
{
    public class Datas:List<Data>
    {
        #region ----------------------------------------- Properties

        #endregion -------------------------------------- Properties


        #region ----------------------------------------- Constructors
        public Datas() 
        {
            using (Transaction tr = HostApplicationServices.WorkingDatabase.TransactionManager.StartTransaction())
            {
                SelectionSet Sset = GetSelection();
                Initialize(Sset, tr);
            }
        }

        public Datas(SelectionSet _sst, Transaction _tr)
        {
            Initialize(_sst, _tr);
        }

        #endregion -------------------------------------- Constructors


        #region ----------------------------------------- Methods
        private SelectionSet GetSelection()
        {
            Document acadDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Editor ed = acadDoc.Editor;
            try
            {
                TypedValue[] filList = { new TypedValue(0, "LwPolyline,Spline,Arc,Line,Circle,Ellipse,Hatch") };
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
        private void Initialize(SelectionSet _sst, Transaction _tr)
        {
            if (_sst != null && _sst.Count > 0)
            {
                for (int _ssi = 0; _ssi < _sst.Count; _ssi++)
                {
                    this.Add(new Data(_tr, _sst[_ssi].ObjectId));
                }
            }
        }
        #endregion -------------------------------------- Method
    }
}
