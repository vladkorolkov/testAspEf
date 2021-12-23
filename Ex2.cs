using System;
using System.Collections.Generic;

namespace southSoundWebsite
{
    public partial class Ex2
    {
        public byte N { get; set; }
        public string Площадка { get; set; } = null!;
        public string Территория { get; set; } = null!;
        public string ТипКонтента { get; set; } = null!;
        public string ВидИспользованияКонтентаЗагрузкаСтриммингПодпискаРадио { get; set; } = null!;
        public string Исполнитель { get; set; } = null!;
        public string? UpcАльбомаКодТрекаПоБазеЛицензиара { get; set; }
        public string IsrcКонтента { get; set; } = null!;
        public string НазваниеТрека { get; set; } = null!;
        public string НазваниеАльбома { get; set; } = null!;
        public string Копирайт { get; set; } = null!;
        public byte КоличествоЗагрузокПрослушиваний { get; set; }
        public decimal СуммаДенежныхСредствПолученныхЛицензиатом { get; set; }
        public string ДоляЛицензиара { get; set; } = null!;
        public decimal ВознаграждениеВРубБезНдс { get; set; }
        public string КодЛицензиата { get; set; } = null!;
    }
}
