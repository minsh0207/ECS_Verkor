using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_mst_model Request
    /// </summary>
    public class _jsonMstModelRequest : __baseRequest
    {
        public _jsonMstModelRequest() { }
        ~_jsonMstModelRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_mst_model Response
    /// </summary>
    public class _jsonMstModelResponse : _baseResponse
    {
        public _jsonMstModelResponse() { }
        ~_jsonMstModelResponse() { }

        public List<_mst_model> DATA;
    }

}
