using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_mst_trouble Request
    /// </summary>
    public class _jsonMstTroubleRequest : __baseRequest
    {
        public _jsonMstTroubleRequest() { }
        ~_jsonMstTroubleRequest() { }
    }


    /// <summary>
    /// JSON Format Body : tb_mst_trouble Response
    /// </summary>
    public class _jsonMstTroubleResponse : _baseResponse
    {
        public _jsonMstTroubleResponse() { }
        ~_jsonMstTroubleResponse() { }

        public List<_mst_trouble> DATA;
    }

}
