using ETM.WCCOA;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class DescriptionDPElement
    {
        Dictionary<string, string> descriptionList{get;set;}

        public DescriptionDPElement()
        {

            fillDescriptionList();
        }

        /// <summary>
        /// Выбор мультистроки из списка
        /// </summary>
        /// <param name="name">Название по которому происходит поиск</param>
        /// <returns></returns>
        public string search(string name)
        {
            return descriptionList[name];
        }

        /// <summary>
        /// Заполнение списка мультистроками
        /// </summary>
        void fillDescriptionList()
        {
            AddDescription("VersionWin", "Windows", "Версия Windows");
            AddDescription("Count CPU", "Count CPU", "Количество ядер");
            AddDescription("ArchitectureCPU", "Architecture CPU", "Архитектура процессора");
        }

        /// <summary>
        /// Добавление в список мультистроки
        /// </summary>
        /// <param name="name">Ключ</param>
        /// <param name="descriptionEng">Описание на английском</param>
        /// <param name="descriptionRus">Описание на русском</param>
        void AddDescription(string name, string descriptionEng, string descriptionRus)
        {
            LocalizedString text = new LocalizedString();
            text.SetText(new CultureInfo(25), descriptionRus);
            text.SetText(new CultureInfo(1033), descriptionEng);
            descriptionList.Add(name, text.GetText());
        }


    }
}
