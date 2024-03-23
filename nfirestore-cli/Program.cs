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

                   DatabaseSelector? selector = null;

                   if (!o.IsFullyPopulated())
                   {
                       selector = new DatabaseSelector(o);
                       Application.Run(selector);
                   }

                   if(selector?.Exit ?? false)
                   {
                       return;
                   }

                   Application.Run(new MainWindow(o));

                   Application.Shutdown();

               });
    }
}