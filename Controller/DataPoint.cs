using ETM.WCCOA;
using ETM.WCCOA.Driver;
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
    class DataPoint
    {
        static OaManager myManager = null;

        public string DpName { protected set; get; }
        public string DptName { protected set; get; }
        public Dictionary<string, dynamic> Parametrs { protected set; get; }
        private OaProcessModel dataModel;
        private DescriptionDPElement element;

        protected OaProcessModel GetDataModel()
        {
            return dataModel;
        }

        public DataPoint(OaManager manager)
        {
            myManager = manager;
            dataModel = myManager.ProcessModel;
            DpName = SystemParametrs.MachineName();
            element = new DescriptionDPElement();
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

        #region Write
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
            }
        }

        /// <summary>
        /// Создание имени для DPelement
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual string WriteValuesExampleDpName(string key)
        {
            string _dpName = $"{DpName}.{key}";
            description(_dpName, key);
            return _dpName;
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
        #endregion

        protected virtual void description(string _dpName, string key)
        {
            OaProcessModel valueAccess = myManager.ProcessModel;
            valueAccess.FireDpDescription(_dpName, element.search(key));            
        }
    }
}
