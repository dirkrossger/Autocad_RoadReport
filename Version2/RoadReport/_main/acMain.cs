using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

namespace RoadReport.acMain
{
    internal class acMain
    {
        #region Acad-Globale Umgebungsvariablen
        protected static Database _db = null;
        protected static Document _doc = null;
        protected static Editor _ed = null;
        #endregion Acad-Globale Umgebungsvariablen
        internal static void _myInit()
        {
            _db = Autodesk.AutoCAD.DatabaseServices.HostApplicationServices.WorkingDatabase;
            _doc = Application.DocumentManager.MdiActiveDocument;
            _ed = _doc.Editor;
        }
    }
}
