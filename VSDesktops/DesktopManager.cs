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
        private DesktopManager(DTE2 applicationObject)
        {
            ApplicationObject = applicationObject;
        }

        private static DesktopManager _instance;
        public static DesktopManager Instance(DTE2 applicationObject)
        {
            return _instance ?? (_instance = new DesktopManager(applicationObject));
        }

        private const int DesktopCount = 3;
        private readonly IDictionary<string, Desktop> _desktops = new Dictionary<string, Desktop>();

        public void CreateButtons(CommandBars cmd, Commands2 commands, AddIn addInInstance)
        {
            var bar = cmd["Standard"];
            for (int i = 0; i < DesktopCount; i++)
            {
                var commandName = string.Format("Desk{0}", i);
                var commandText = string.Format("Desk {0}", i + 1);
                var desktop = new Desktop(commandName, commandText, ApplicationObject, addInInstance, bar, commands);
                _desktops.Add(desktop.CommandName, desktop);
            }
            _currentDesktop = _desktops.First().Value;
            _currentDesktop.Save();
        }

        public bool HandleExec(string commandName)
        {
            if (!_desktops.ContainsKey(commandName))
            {
                return false;
            }
            _currentDesktop.Save();
            _currentDesktop = _desktops[commandName];
            _currentDesktop.Restore();

            return true;
        }

        private DTE2 ApplicationObject
        {
            get;
            set;
        }

        private Desktop _currentDesktop;

    }
}
