using Common.Exceptions;
using Gui;

internal class Program {
    public static void Main() {
        Thread MainThread = new Thread(Start);
        MainThread.Start();
        Display.ConstructInstance();
        
    }
    public static void Start() {
        Thread.Sleep(1000);
        Display.WriteGeneric("test");
    }

}

