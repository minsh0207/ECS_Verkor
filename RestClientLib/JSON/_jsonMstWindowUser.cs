using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_mst_window_user Request
    /// </summary>
    public class _jsonMstWindowUserRequest : __baseRequest
    {
        public _jsonMstWindowUserRequest() { }
        ~_jsonMstWindowUserRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_mst_window_user Response
    /// </summary>
    public class _jsonMstWindowUserResponse : _baseResponse
    {
        public _jsonMstWindowUserResponse() { }
        ~_jsonMstWindowUserResponse() { }

        public List<_mst_window_user> DATA;
    }

}
