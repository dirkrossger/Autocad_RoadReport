using Autodesk.AutoCAD.DatabaseServices;

namespace RoadReport
{
    internal partial class yyy : acMain.acMain
    {
        public static void Run()
        {
            _myInit();
            using(Transaction _tr = _db.TransactionManager.StartTransaction())
            {
                // ... 
            }
        }
    }
}
