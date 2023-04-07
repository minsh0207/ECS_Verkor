using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_mst_eqp_type Request
    /// </summary>
    public class _jsonMstEqpTypeRequest : __baseRequest
    {
        public _jsonMstEqpTypeRequest() { }
        ~_jsonMstEqpTypeRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_mst_eqp_type Response
    /// </summary>
    public class _jsonMstEqpTypeResponse : _baseResponse
    {
        public _jsonMstEqpTypeResponse() { }
        ~_jsonMstEqpTypeResponse() { }

        public List<_mst_eqp_type> DATA;
    }

}
