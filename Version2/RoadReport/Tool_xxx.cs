using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadReport
{
    internal partial class xxx : acMain.acMain
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
