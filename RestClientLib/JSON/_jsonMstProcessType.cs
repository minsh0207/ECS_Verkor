using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_mst_process_type Request
    /// </summary>
    public class _jsonMstProcessTypeRequest : __baseRequest
    {
        public _jsonMstProcessTypeRequest() { }
        ~_jsonMstProcessTypeRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_mst_process_type Response
    /// </summary>
    public class _jsonMstProcessTypeResponse : _baseResponse
    {
        public _jsonMstProcessTypeResponse() { }
        ~_jsonMstProcessTypeResponse() { }

        public List<_mst_process_type> DATA;
    }

}
