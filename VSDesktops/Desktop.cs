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
        private readonly Command _command;

        public Desktop(string name, string text, DTE2 appObject, AddIn addInInstance, CommandBar bar, Commands2 commands)
        {
            _appObject = appObject;
            var contextGUIDS = new object[] { };
            _command = commands.AddNamedCommand2(addInInstance, name, text, string.Empty, true, 0, ref contextGUIDS);
            _command.AddControl(bar, bar.Controls.Count);
        }

        public string CommandName
        {
            get
            {
                return _command.Name;
            }
        }

        private void HideAll()
        {
            foreach (var window in GetAll())
            {
                window.Visible = false;
            }
        }

        private IEnumerable<Window> GetAll()
        {
            return _appObject.Windows.Cast<Window>()
                .Where(w => w.Type == vsWindowType.vsWindowTypeDocument);
        }

        private void Show(IEnumerable<Window> windows)
        {
            foreach (var window in windows)
            {
                window.Visible = true;
            }
        }

        public void Restore()
        {
            HideAll();
            Show(_windows);
        }

        public void Save()
        {
            _windows = GetAll().ToList();
        }

        private IList<Window> _windows = new List<Window>();
    }
}
