

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using associatetool.Model;
using System;
using Microsoft.Extensions.Configuration;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;

namespace associatetool.Controllers{
    [Route("")]
    public class AssociateController : Controller{

        private HttpClient client = new HttpClient();
        private readonly IValidaTool validaTool;        

        public AssociateController(IValidaTool validaTool){
            this.validaTool = validaTool;            
        }

        [HttpPost("associate")]
        [Produces("application/json")]
        public async Task<IActionResult> Associate([FromBody] Tool t){  
            if(ModelState.IsValid){ 
                Console.WriteLine(JsonConvert.SerializeObject(t));
                if(t==null)
                    return BadRequest();  
                if(t.position == null){ 
                    var (result, status) = await validaTool.desassociando(t);                     
                    if(status == HttpStatusCode.OK)
                        return Ok(result);
                    else   
                        return BadRequest(); 
                }    
                else{
                    var (result, status) = await validaTool.associando(t);                     
                    if(status == HttpStatusCode.OK)
                        return Ok(result);
                    else   
                        return BadRequest(); 
                }                                   
            }
            return BadRequest(ModelState);
        }

        [HttpPost("tags")]
        [Produces("application/json")]
        public async Task<IActionResult> CadTags(){  
            int i = 0, t=1;
            Console.WriteLine("teste");
            try{
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
                Teste teste = null;
                for(i = 2; i<= 40; i++){
                    teste = new Teste("ferramenta"+(i+3), i);
                    Console.WriteLine(teste.ToString());
                    await client.PostAsync("http://localhost:8005/api/tool",new StringContent(JsonConvert.SerializeObject(teste), Encoding.UTF8, "application/json"));                       
                }    
                // t=2;    
                // for(i = 1; i<= 92; i++)
                //     await client.PostAsync("http://35.170.191.75:8001/api/tags",new StringContent(JsonConvert.SerializeObject(new Teste("vida_util", i+"pos_vida_acumu", "P"+i)), Encoding.UTF8, "application/json"));   
            }catch(Exception e){
                Console.Write("Erro : i= ");
                Console.Write(i);  
            }        
            return BadRequest(ModelState);
        }                                      
    }
}
