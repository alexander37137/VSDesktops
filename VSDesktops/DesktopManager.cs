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
    class DesktopManager
    {
        public DesktopManager(DTE2 applicationObject)
        {
            _applicationObject = applicationObject;
        }

        private const int DesktopCount = 3;
        private readonly IDictionary<string, Command> buttons = new Dictionary<string, Command>();

        public void CreateButtons(CommandBars cmd, Commands2 commands, AddIn _addInInstance)
        {
            var contextGUIDS = new object[] { };
            var bar = cmd["Standard"];
            for (int i = 0; i < DesktopCount; i++)
            {
                var commandName = string.Format("Desk{0}", i);
                var commandText = string.Format("Desk {0}", i + 1);
                var command = commands.AddNamedCommand2(_addInInstance, commandName, commandText, string.Empty, true, 0, ref contextGUIDS);
                command.AddControl(bar, bar.Controls.Count);
                buttons.Add(commandName, command);
            }

        }

        public bool HandleExec(string commandName)
        {
            if (!buttons.ContainsKey(commandName))
            {
                return false;
            }

            return true;
        }


        //private void SwitchDesktop()
        //{
        //    var windows =
        //        _applicationObject.Windows.Cast<Window>()
        //            .Where(w => w.Type == vsWindowType.vsWindowTypeDocument);
        //    foreach (var window in windows)
        //    {
        //        window.Visible = _isFirstDesktop;
        //    }
        //    _isFirstDesktop = !_isFirstDesktop;
        //}

        private readonly DTE2 _applicationObject;

    }
}
