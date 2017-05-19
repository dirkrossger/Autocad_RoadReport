using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyInfo
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandDescription:System.Attribute
    {
        #region ----------------------------------------- Properties
        public string Description { get; set; }
        #endregion -------------------------------------- Properties


        #region ----------------------------------------- Constructors
        public CommandDescription() { }
        #endregion -------------------------------------- Constructors


        #region ----------------------------------------- Methods

        #endregion -------------------------------------- Method
    }
}
