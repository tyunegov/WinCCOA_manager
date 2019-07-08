using ETM.WCCOA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2.Controller
{
    class ParamStorage : DataPoint
    {
        List<string> disks=new List<string>();

        public ParamStorage(OaManager manager) : base(manager)
        {            
            DptName = "Storage_Param";
            DpName += "_Storage";
        }

        protected override string WriteValuesExampleDpName(string key)
        {
            string _key = key.Substring(0, key.Length - 2);
            description($"{DpName}.{_key}", _key);


            return DpName + "." + _key;
        }

        public override void Connected()
        {
            foreach (var v in SystemParametrs.ParamHD())
            {
                DpName = $"Disk_{v.ElementAt(1).Value}".Replace(":\\", "");
                disks.Add(DpName);
                Parametrs = v;
                base.Connected();
            }
        }

        /// <summary>
        /// Переопределяет в родительском классе переменную parametrs
        /// </summary>
        protected override void WriteValuesExample()
        {
            List<string> _disks = new List<string>();
            foreach (var v in SystemParametrs.ParamHD())
            {
                try
                {
                    DpName = $"Disk_{v.ElementAt(1).Value}".Replace(":\\", "");
                    _disks.Add(DpName);
                    Parametrs = v;
                    base.WriteValuesExample();
                }
                catch { }
            }
            DeleteDP(_disks);
        }

        /// <summary>
        /// Удаляет точку при отключении
        /// </summary>
        /// <param name="_disks"></param>
        protected void DeleteDP(List<string> _disks)
        {
            foreach(var v in disks.Except(_disks))
            {
                GetDataModel().DeleteDp(v);
            }
            disks = _disks;
        }
    }
}
