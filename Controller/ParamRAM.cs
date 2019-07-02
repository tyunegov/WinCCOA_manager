using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Controller
{
    class ParamRAM:Manager
    {
        public ParamRAM()
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
