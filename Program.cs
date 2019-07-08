using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using ETM.WCCOA;
using ETM.WCCOA.Trace;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApp2.Controller;
using System.Globalization;


namespace ConsoleApp2
{
    class Program
    {
        static OaManager myManager = null;

        static void Main(string[] args)
        {
            Console.WriteLine("Запуск менеджера");
            try
            {
                EstablishConection(args);
                Console.WriteLine("Менеджер поключен");
            }
            catch(Exception e)
            {
                Console.WriteLine("Ошибка подключения менеджера. Проверьте наличие файла ETM.WCCOA.CSharpInterface.dll в папке bin");
                Console.WriteLine(e);
            }
            try
            {
                DataPoint pc = new ParamPC(myManager);
                pc.Connected();
                Console.WriteLine("Информация о компьютере записана");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            try
            {
                DataPoint storage = new ParamStorage(myManager);
                storage.Connected();
                Console.WriteLine("Информация об устройствах памяти записана");
            }
           catch (Exception e)
           {
               Console.WriteLine(e);
           }
           try
           {
                DataPoint ram = new ParamRAM(myManager);
               ram.Connected();
               Console.WriteLine("Информация об ОЗУ записана");

           }
           catch (Exception e)
           {
               Console.WriteLine(e);
           }
        //   myManager.StopAsync();
       }

       /// <summary>
       /// Подключение менеджера к проекту
       /// </summary>
       /// <param name="args"></param>
       public static async void EstablishConection(string[] args)
       {
           myManager = OaSdk.CreateManager();
           myManager.Init(ManagerSettings.DefaultApiSettings, args);
           await myManager.StartAsync();
       }
   }

}

