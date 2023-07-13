using SQLite;

namespace Ejercicio2_3_Movil2_2doP.Models
{
    public class Photograph
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public byte[] Image { get; set; }

        public string Description { get; set; }
    }
}
