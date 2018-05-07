

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using associatetool.Model;
using System;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace associatetool.Controllers{
    [Route("")]
    public class AssociateController : Controller{

        private readonly IValidaTool validaTool;        

        public AssociateController(IValidaTool validaTool){
            this.validaTool = validaTool;            
        }

        [HttpPost("associate")]
        [Produces("application/json")]
        public async Task<IActionResult> Associate([FromBody] Tool t){   
            if(t.position == null){ 
                var (result, status) = await validaTool.desassociando(t);                     
                if(status == HttpStatusCode.OK)
                    return Ok(result);
                else   
                    return BadRequest(); 
            }    
            else if(t.position>0 && t.position<16){
                var (result, status) = await validaTool.associando(t);                     
                if(status == HttpStatusCode.OK)
                    return Ok(result);
                else   
                    return BadRequest(); 
            }                   
            else   
                return BadRequest();                      
        }                   
    }
}
