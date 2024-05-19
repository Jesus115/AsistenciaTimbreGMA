using System;
namespace JAbarcaPFinal.Models
{
	public class registroAsistencia
	{
        public int userid { get; set; }
        public int idempresa { get; set; }
        public int id_biometrico { get; set; }
        public string foto { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string nombreCat { get; set; }
        public int idcatalogo { get; set; }
        public int idusuario { get; set; }
        public DateTime fecha { get; set; }


        public registroAsistencia()
		{
		}
	}
}

