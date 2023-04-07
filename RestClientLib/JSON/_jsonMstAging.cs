using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_mst_Aging Request
    /// </summary>
    public class _jsonMstAgingtRequest : __baseRequest
    {
        public _jsonMstAgingtRequest() { }
        ~_jsonMstAgingtRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_mst_Aging Response
    /// </summary>
    public class _jsonMstAgingtResponse : _baseResponse
    {
        public _jsonMstAgingtResponse() { }
        ~_jsonMstAgingtResponse() { }

        public List<_mst_Aging> DATA;
    }

}
