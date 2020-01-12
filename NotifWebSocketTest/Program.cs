using System;
using WebSocketSharp;

namespace NotifWebSocketTest
{
    class Program
    {
        //static IHostBuilder CreateSocketServerBuilder()
        //{
        //    return SuperSocketHostBuilder.Create<StringPackageInfo, CommandLinePipelineFilter>()
        //        .UseCommand((commandOptions) =>
        //        {
        //            // register commands one by one
        //            commandOptions.AddCommand<ADD>();
        //            commandOptions.AddCommand<MULT>();
        //            commandOptions.AddCommand<SUB>();

        //            // register all commands in one aassembly
        //            //commandOptions.AddCommandAssembly(typeof(SUB).GetTypeInfo().Assembly);
        //        })
        //        .ConfigureAppConfiguration((hostCtx, configApp) =>
        //        {
        //            configApp.AddInMemoryCollection(new Dictionary<string, string>
        //            {
        //                { "serverOptions:name", "TestServer" },
        //                { "serverOptions:listeners:0:ip", "Any" },
        //                { "serverOptions:listeners:0:port", "4040" }
        //            });
        //        })
        //        .ConfigureLogging((hostCtx, loggingBuilder) =>
        //        {
        //            loggingBuilder.AddConsole();
        //        });
        //}

        static void Main(string[] args)
        {
            using (var ws = new WebSocket("ws://localhost:8888/EUR-lpierre020413"))
            {
                ws.OnMessage += (sender, e) =>
                    Console.WriteLine("Laputa says: " + e.Data);

                ws.Connect();
                ws.Send("BALUS");
                Console.ReadKey(true);
            }
        }
    }
}
