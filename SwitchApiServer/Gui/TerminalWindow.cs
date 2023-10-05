using Terminal.Gui;

namespace Gui {
    public class TerminalWindow : Window {
        private MenuBar menu;
        private TextView genericConsole;
        public TerminalWindow() {
            menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem("[Console Logs]", new MenuItem[] {
                    new MenuItem("General Logs","Generic information, Default console",EnableGenericConsole),
                    new MenuItem("Error Logs","This console will provide all errors!",() => {}),
                    new MenuItem("Network Logs","Packet specific information, Networking nerds only",() => {})
                })
            });
            Add(menu);
            AddGenericConsole();
            EnableGenericConsole();
        }
        public void AddGenericConsole() {
            
            genericConsole = new TextView() {
                //X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                ReadOnly = true,
                Visible = false,
                Text = "",
            };
            
            Add(genericConsole);
            
        }
        public void EnableGenericConsole() {
            genericConsole.Visible = !genericConsole.Visible;
        }
        public void WriteToGenericConsole(string text) {
            genericConsole.Text += "\n" + text;
        }
    }
}
