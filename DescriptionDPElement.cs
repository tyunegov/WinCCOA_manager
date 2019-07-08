using ETM.WCCOA;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class DescriptionDPElement
    {
        Dictionary<string, LocalizedString> descriptionList = new Dictionary<string, LocalizedString>();

        public DescriptionDPElement()
        {

            fillDescriptionList();
        }

        /// <summary>
        /// Выбор мультистроки из списка
        /// </summary>
        /// <param name="name">Название по которому происходит поиск</param>
        /// <returns></returns>
        public LocalizedString search(string name)
        {
            return descriptionList[name];
        }


        /// <summary>
        /// Заполнение списка мультистроками
        /// </summary>
        void fillDescriptionList()
        {
            AddDescription("VersionWin", "Windows", "Версия Windows");
            AddDescription("CountCPU", "Count CPU", "Количество ядер процессора");
            AddDescription("ArchitectureCPU", "Architecture CPU", "Архитектура процессора");
            AddDescription("ModelCPU", "Model CPU", "Модель процессора");
            AddDescription("LoadPercentageCPU","Load CPU", "Нагрузка процессора");
            AddDescription("TemperatureCPU", "Temperature CPU", "Температура процессора");
            AddDescription("Capacity", "Capacity disk, GB", "Объем диска, ГБ");
            AddDescription("Caption", "Caption disk", "Название диска");
            AddDescription("FileSystem", "File system", "Файловая система");
            AddDescription("FreeSpace", "Free space, GB", "Свободно памяти, ГБ");
            AddDescription("TotalMemorySize", "Total RAM, GB", "Всего оперативной памяти, ГБ");
            AddDescription("FreePhysicalMemory", "Free RAM, GB", "Доступно оперативной памяти, ГБ");
            AddDescription("PercentFreeMemory", "Percent free RAM","Доступно оперативной памяти, %");
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
            text.SetText(new CultureInfo(1049), descriptionRus);
            text.SetText(new CultureInfo(1033), descriptionEng);
            descriptionList.Add(name, text);
        }
    }
}
