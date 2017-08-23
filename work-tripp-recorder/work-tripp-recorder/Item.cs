using SQLite;
using System;

namespace work_tripp_recorder
{
    [Table("Item")]
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public string CityStart { get; set; }
        public string CityEnd { get; set; }
        public string VisitedEntity { get; set; }
        public string Purpose { get; set; }
        public int MileageStart { get; set; }
        public int MileageEnd { get; set; }

        [Ignore]
        public string ReadableTitle
        {
            get
            {
                return $"{Id}. {CityStart} to {CityEnd}";
            }
        }
    }
}