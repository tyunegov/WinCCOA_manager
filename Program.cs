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
        static void Main(string[] args)
        {
            Console.WriteLine("Запуск менеджера");
            Manager.EstablishConection(args);
            Console.WriteLine("Менеджер поключен");


            Manager pc = new ParamPC();
            pc.Connected();
            Console.WriteLine("Информация о компьютере записана");

            Manager storage = new ParamStorage();
            storage.Connected();
            Console.WriteLine("Информация об устройствах памяти записана");

            Manager ram = new ParamRAM();
            ram.Connected();
        }

    /*    static void test()
        {
            LocalizedString text = new LocalizedString();
           // text.SetText(new CultureInfo(25), "rus");
            text.SetText(new CultureInfo(1033), "end");
            Console.WriteLine("_____________________________________________________________________________________" +
                "____________________________________________________________________________________________________" +
                "________________________________________________________________________________________________________" +
                "____________________________________________________________________________________________________");
           Console.WriteLine(text.GetText());

            OaSimpleDataPointElement oa = new OaSimpleDataPointElement();
            oa.SetDescription(text);
        }*/
    }
    
}

