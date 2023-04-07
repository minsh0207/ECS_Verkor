using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_mst_user_class Request
    /// </summary>
    public class _jsonMstUserClassRequest : __baseRequest
    {
        public _jsonMstUserClassRequest() { }
        ~_jsonMstUserClassRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_mst_user_class Response
    /// </summary>
    public class _jsonMstUserClassResponse : _baseResponse
    {
        public _jsonMstUserClassResponse() { }
        ~_jsonMstUserClassResponse() { }

        public List<_mst_user_class> DATA;
    }

}
