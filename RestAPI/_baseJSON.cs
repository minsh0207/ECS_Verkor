using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPIServer
{

	public class __baseRequest
	{
		public __baseRequest() { }
		~__baseRequest() { }

		public string ACTION_ID { get; set; }
		public string ACTION_USER { get; set; }
		public string REQUEST_TIME { get; set; }
	}

	/// <summary>
	/// JSON base format : Response
	/// </summary>    
	public class _baseResponse
	{

		public _baseResponse() { }
		~_baseResponse() { }

		public string ACTION_ID { get; set; }
		public string RESPONSE_TIME { get; set; }
		public string RESPONSE_CODE { get; set; }
		public string RESPONSE_MESSAGE { get; set; }
		public string PROCESSING_TIME { get; set; }
	}

	public class _CommandRequest : __baseRequest
    {
		// EQP_TYPE
		public string EQP_TYPE { get; set; }
		//EQP_ID
		public string EQP_ID { get; set; }
		//UNIT_ID
		public string UNIT_ID { get; set; }
		//COMMAND_ID
		public string COMMAND { get; set; }

	}

}
