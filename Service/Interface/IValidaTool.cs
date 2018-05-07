using System.Net;
using System.Threading.Tasks;
using associatetool.Model;

namespace associatetool{

    public interface IValidaTool{
        
        Task<(Tag_Address,HttpStatusCode)> associando(Tool t);
        Task<(Tag_Address,HttpStatusCode)> desassociando(Tool t);
    }    
}