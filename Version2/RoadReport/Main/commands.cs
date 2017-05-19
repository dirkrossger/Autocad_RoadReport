using AssemblyInfo;
using Autodesk.AutoCAD.Runtime;

namespace RoadReport.Main
{
    public class commands
    {
        [CommandDescription(Description="Öffnet das Report-Panel")]
        [CommandMethod("mReport")]
        public void cmdMREPORT()
        {
            UI.panelHandler.PanelOpen();
        }

        [CommandDescription(Description = "Führt den Befehl XXX aus")]
        [CommandMethod("xxx-Command")]
        public void cmdXXX()
        {
            xxx.Run();
        }

        //[CommandDescription(Description = "Führt den Befehl YYY aus")]
        [CommandMethod("yyy-Command")]
        public void cmdYYY()
        {
            yyy.Run();
        }
    }
}
