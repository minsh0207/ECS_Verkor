using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_mst_window Request
    /// </summary>
    public class _jsonMstWindowRequest : __baseRequest
    {
        public _jsonMstWindowRequest() { }
        ~_jsonMstWindowRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_mst_window Response
    /// </summary>
    public class _jsonMstWindowResponse : _baseResponse
    {
        public _jsonMstWindowResponse() { }
        ~_jsonMstWindowResponse() { }

        public List<_mst_window> DATA;
    }

}
