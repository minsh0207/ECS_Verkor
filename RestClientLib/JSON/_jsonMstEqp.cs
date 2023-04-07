using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_mst_eqp Request
    /// </summary>
    public class _jsonMstEqpRequest : __baseRequest
    {
        public _jsonMstEqpRequest() { }
        ~_jsonMstEqpRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_mst_eqp Response
    /// </summary>
    public class _jsonMstEqpResponse : _baseResponse
    {
        public _jsonMstEqpResponse() { }
        ~_jsonMstEqpResponse() { }

        public List<_mst_eqp> DATA;
    }

}
