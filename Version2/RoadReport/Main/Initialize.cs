using AssemblyInfo;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

namespace RoadReport.Main
{
    public partial class mInitialize : Autodesk.AutoCAD.Runtime.IExtensionApplication
    {
        public void Initialize()
        {
            #region Acinit
            Database _db = Autodesk.AutoCAD.DatabaseServices.HostApplicationServices.WorkingDatabase;
            Document _doc = Application.DocumentManager.MdiActiveDocument;
            Editor _ed = _doc.Editor;
            #endregion AcInit
            #region Ausgabe der Befehle
            _ed.WriteMessage("\n******************************************************");
            foreach (CommandInfo _ci in AssemblyInfo.AssemblyInfo.GetCommandInfos())
            { _ed.WriteMessage("\n" + _ci.Command + " --> " + _ci.CommandDescription); }
            #endregion Ausgabe der Befehle
            #region Abspann
            _ed.WriteMessage("\n******************************************************");
            _ed.WriteMessage("\n*              COD-Zusatzbefehle geladen             *");
            _ed.WriteMessage("\n*          CAD on demand GmbH - www.cad-od.de        *");
            _ed.WriteMessage("\n******************************************************");
            #endregion Abspann
        }

        public void Terminate()
        { 
            // nichts zu tun beim Beenden von AutoCAD
        }
    }
}
