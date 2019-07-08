using ETM.WCCOA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Controller
{
    class ParamRAM:DataPoint
    {
        public ParamRAM(OaManager manager) : base(manager)
        {

            DptName = "RAM_Param";
            DpName += "_RAM";
        }

        protected override void WriteValuesExample()
        {
            Parametrs = SystemParametrs.ParamRAM();
            base.WriteValuesExample();
        }
    }
}
