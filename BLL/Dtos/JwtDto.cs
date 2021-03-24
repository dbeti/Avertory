using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos
{
	public class JwtDto
	{
		public string Token { get; set; }
		public DateTime Expiration { get; set; }
	}
}
