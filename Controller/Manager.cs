using ETM.WCCOA;
using ETM.WCCOA.Trace;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2.Controller
{
    class Manager
    {
        static OaManager myManager = null;

        public string DpName {protected set; get; }
        public string DptName {protected set; get; }
        public Dictionary<string, dynamic> Parametrs {protected set; get; }
        private OaProcessModel dataModel = myManager.ProcessModel;
    //    DescriptionDPElement description = new DescriptionDPElement();

        protected OaProcessModel GetDataModel()
        {
            return dataModel;
        }

        public Manager()
        {
            DpName = SystemParametrs.MachineName();
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


        /// <summary>
        /// Создание и запись значений в DP
        /// </summary>
        public virtual void Connected()
        {
            ModifyDataModelExample().Wait();
            Thread myThread = new Thread(new ThreadStart(Write));
            myThread.Start();
        }

        /// <summary>
        /// Создание точек данных
        /// </summary>
        /// <returns></returns>
        async Task ModifyDataModelExample()
        {
                if (await dataModel.IsDpPathExistingAsync(DpName))
                    await dataModel.DeleteDpAsync(DpName);
                await dataModel.CreateDpAsync(DpName, DptName);
        }

        /// <summary>
        /// Запись значений в точку данных
        /// </summary>
        protected virtual void WriteValuesExample()
        {
            
                WriteValuesExampleVerification().Wait();
                OaProcessValues valueAccess = myManager.ProcessValues;
                foreach (var v in Parametrs)
                {
                    string _dpName = WriteValuesExampleDpName(v.Key);
                    //      await valueAccess.SetDpValueAsync(dpPath+".", variant); В этом варианте вылетает с ошибкой 
                    valueAccess.FireDpValue($"{_dpName}:_original.._value", v.Value);
                try
                {
                    valueAccess.FireDpValue($"{_dpName}:_common.._value", "Описание");
                }
                catch { }
                }
        }

        /// <summary>
        /// Обновление значений
        /// </summary>
        void Write()
        {
            while (true)
            {
                WriteValuesExample();
                Thread.Sleep(500);
            }
        }
        
        async Task WriteValuesExampleVerification()
        {
            if (!(await dataModel.IsDpPathExistingAsync(DpName)))
                await dataModel.CreateDpAsync(DpName, DptName);
        }

        /// <summary>
        /// Создание имени для DPelement
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual string WriteValuesExampleDpName(string key)
        {
           return $"{DpName}.{key}";
        }        
    }
}
