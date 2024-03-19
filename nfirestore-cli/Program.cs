using CommandLine;
using nfirestore_cli;
using Terminal.Gui;

public class MainProgram
{
    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
               .WithParsed<Options>(o =>
               {
                   Application.Init();

                   Application.Run(new nfirestore_cli.MainWindow(o));

                   Application.Shutdown();

               });
    }
}