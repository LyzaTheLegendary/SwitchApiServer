using Common.Exceptions;
using Terminal.Gui;

namespace Gui {
    public static class Display {
        private static TerminalWindow? window;
        public static void ConstructInstance() {
            window = (TerminalWindow)Application.Top;
            Application.Run<TerminalWindow>();
        }
        private static TerminalWindow GetInstance() => window ?? throw new mException("Tried to get Terminal, But no termnal was set!");
        public static void WriteGeneric(string text) => GetInstance().WriteToGenericConsole(text);
    }
}
