using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;

namespace VSDesktops
{
    class Desktop
    {
        private readonly DTE2 _appObject;

        public Desktop(string name, string text, DTE2 appObject, AddIn addInInstance, CommandBar bar, Commands2 commands)
        {
            _appObject = appObject;
            var contextGUIDS = new object[] { };
            var command = commands.AddNamedCommand2(addInInstance, name, text, string.Empty, true, 0, ref contextGUIDS);
            command.AddControl(bar, bar.Controls.Count);
        }
    }
}
