using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_mst_user Request
    /// </summary>
    public class _jsonMstUserRequest : __baseRequest
    {
        public _jsonMstUserRequest() { }
        ~_jsonMstUserRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_mst_user Response
    /// </summary>
    public class _jsonMstUserResponse : _baseResponse
    {
        public _jsonMstUserResponse() { }
        ~_jsonMstUserResponse() { }

        public List<_mst_user> DATA;
    }

}
