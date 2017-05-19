using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyInfo
{
    
    internal static class AssemblyInfo
    {
        static public List<CommandInfo> GetCommandInfos()
        {
            List<CommandInfo> _CommandInfos = new List<CommandInfo>();


            foreach (Module _modul in Assembly.GetExecutingAssembly().GetModules(true))
            {
                foreach (Type _type in _modul.GetTypes())
                {
                    foreach (MethodInfo _method in _type.GetMethods())
                    {
                        string _CommandName = "";
                        string _CommandDescription = "";
                        #region CommandName
                        object[] _CommandAttributes = _method.GetCustomAttributes(typeof(CommandMethodAttribute), true);
                        foreach (object attb in _CommandAttributes)
                        {
                            CommandMethodAttribute cma = attb as CommandMethodAttribute;
                            if (cma != null)
                            {
                                _CommandName = cma.GlobalName;
                            }
                        } 
                        #endregion

                        object[] _DescriptionAttributes = _method.GetCustomAttributes(typeof(CommandDescription), true);
                        foreach (object attb in _DescriptionAttributes)
                        {
                            CommandDescription cma = attb as CommandDescription;
                            if (cma != null)
                            {
                                _CommandDescription = cma.Description;
                            }
                        }
                        if (_CommandAttributes.Length>0)
                            _CommandInfos.Add(new CommandInfo() { Command = _CommandName, CommandDescription = _CommandDescription });
                    }
                }
            }
            return _CommandInfos;
        }
    }
}
