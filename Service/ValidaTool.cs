using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using associatetool.Data;
using associatetool.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace associatetool{

    public class ValidaTool : IValidaTool{        

        private HttpClient client = new HttpClient();
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext context;                 
        
        public ValidaTool(IConfiguration configuration, ApplicationDbContext context){            
            this._configuration = configuration;            
            this.context = context;
        }
        //dia 
        public async Task<(Tag_Address,HttpStatusCode)> associando(Tool t){         
            try{      
                var registro = context.Tag_Address.FirstOrDefault(tag => tag.id == t.position);   
                registro.ferramenta_id = t.toolId.ToString();            
                //Salvar objeto registro no banco update
                context.Tag_Address.Update(registro); 
                await context.SaveChangesAsync();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));            
                var builder = new UriBuilder(_configuration["apiInterLevel"]);                          
                string url = builder.ToString();    
                Console.WriteLine(_configuration["workstation"]);                 
                // //Criar o objeto a ser postado                 
                var message = await client.PostAsync(url,new StringContent(JsonConvert.SerializeObject(new TagEndPointModel(registro.codigo,t.toolId.ToString(),_configuration["workstation"])), Encoding.UTF8, "application/json"));         
                if (message.IsSuccessStatusCode){                    
                    message = await client.PostAsync(url,new StringContent(JsonConvert.SerializeObject(new TagEndPointModel(registro.nome_ferramenta,t.name.ToString(),_configuration["workstation"])), Encoding.UTF8, "application/json"));                                                     
                    Console.WriteLine("Cadastrou nome da ferramenta na tag = " + registro.nome_ferramenta);
                }
                if(message.IsSuccessStatusCode){
                    message = await client.PostAsync(url,new StringContent(JsonConvert.SerializeObject(new TagEndPointModel(registro.vida_util,t.currentLife.ToString(),_configuration["workstation"])), Encoding.UTF8, "application/json"));                     
                    Console.WriteLine("Cadastrou vida util current da ferramenta na tag = " + registro.vida_util);
                }
                if(message.IsSuccessStatusCode){
                    message = await client.PostAsync(url,new StringContent(JsonConvert.SerializeObject(new TagEndPointModel(registro.vida_util_max,t.lifeCycle.ToString(),_configuration["workstation"])), Encoding.UTF8, "application/json"));                         
                    Console.WriteLine("Cadastrou vida util max da ferramenta na tag = " + registro.vida_util_max);
                }
                if(message.IsSuccessStatusCode){
                    Console.WriteLine(_configuration["lista"]);
                    List<Lista> l = JsonConvert.DeserializeObject<List<Lista>>(_configuration["lista"]);
                    message = await client.PostAsync(url,new StringContent(JsonConvert.SerializeObject(new TagEndPointModel(registro.vida_util_unidade,l.FirstOrDefault(r => r.key.Equals(t.unitOfMeasurement)).value,_configuration["workstation"])), Encoding.UTF8, "application/json"));         
                    Console.WriteLine("Cadastrou vida util unidade da ferramenta na tag = " + registro.vida_util_unidade);
                }
                if(message.IsSuccessStatusCode){
                    message = await client.PostAsync(url,new StringContent(JsonConvert.SerializeObject(new TagEndPointModel(registro.trigger,"1",_configuration["workstation"])), Encoding.UTF8, "application/json"));             
                    return (registro,HttpStatusCode.OK);                                    
                }    
            }catch(Exception e){
                Console.WriteLine("Error : ");
                Console.WriteLine(e);
                return (null,HttpStatusCode.BadRequest);            
            }             
            return (null,HttpStatusCode.BadRequest);
        }    
        
        public async Task<(Tag_Address,HttpStatusCode)> desassociando(Tool t){ 
            try{                                                
                var registro = context.Tag_Address.FirstOrDefault(tag => tag.ferramenta_id.Equals(t.toolId.ToString()));                 
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                                    
                var url = new UriBuilder(_configuration["apiInterLevel"]);
                string urlTool = new UriBuilder(_configuration["apiTool"]).ToString();                                             
                t.currentLife = Double.Parse(JObject.Parse(await client.GetStringAsync(url + "?tag=" + registro.vida_util_acumulado))["value"].ToString());      
                string strContent = JsonConvert.SerializeObject(t);
                var inputMessage = new HttpRequestMessage{
                    Content = new StringContent(strContent,Encoding.UTF8,"application/json")
                };     
                inputMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                 
                var message = await client.PutAsync(urlTool+"/"+t.toolId.ToString(), inputMessage.Content);      
                if (message.IsSuccessStatusCode){                                 
                    Console.WriteLine("Atualizou vida util da ferramenta");
                    message = await client.PostAsync(url.ToString(),new StringContent(JsonConvert.SerializeObject(new TagEndPointModel(registro.trigger,"0",_configuration["workstation"])), Encoding.UTF8, "application/json"));                                                          
                    if(message.IsSuccessStatusCode){
                        registro.ferramenta_id = null;
                        await context.SaveChangesAsync();
                        return (registro,HttpStatusCode.OK);
                    }                                                
                    else
                        return (registro,HttpStatusCode.BadRequest);    
                }else{
                    return (registro,HttpStatusCode.BadRequest);    
                }            
            }catch(Exception ex){
                return (null,HttpStatusCode.BadRequest);
            }               
        }       
    }
}