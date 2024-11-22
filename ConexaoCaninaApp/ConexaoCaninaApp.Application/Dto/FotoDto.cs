using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCaninaApp.Application.Dto
{
	public class FotoDTO
	{
		public Guid FotoId { get; set; }
		public string CaminhoArquivo { get; set; }
		public string Descricao { get; set; }
	}
}
