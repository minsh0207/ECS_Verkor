using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestClientLib
{
    /// <summary>
    /// JSON Format Body : tb_dat_trouble Request
    /// </summary>
    public class _jsonDatTroubleRequest : __baseRequest
    {
        public _jsonDatTroubleRequest() { }
        ~_jsonDatTroubleRequest() { }        
    }


    /// <summary>
    /// JSON Format Body : tb_dat_trouble Response
    /// </summary>
    public class _jsonDatTroubleResponse : _baseResponse
    {
        public _jsonDatTroubleResponse() { }
        ~_jsonDatTroubleResponse() { }

        public List<_dat_trouble> DATA;
    }

}
