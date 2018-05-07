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

namespace associatetool{

    public class ValidaTool : IValidaTool{        

        private HttpClient client = new HttpClient();
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext context;                 
        
        public ValidaTool(IConfiguration configuration, ApplicationDbContext context){            
            this._configuration = configuration;            
            this.context = context;
        }

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
                // //Criar o objeto a ser postado
                
                var message = await client.PostAsync(url,new StringContent(JsonConvert.SerializeObject(new TagEndPointModel(registro.codigo,t.toolId.ToString(),"FUSAO")), Encoding.UTF8, "application/json"));         
                if (message.IsSuccessStatusCode){
                    message = await client.PostAsync(url,new StringContent(JsonConvert.SerializeObject(new TagEndPointModel(registro.nome_ferramenta,t.name.ToString(),"FUSAO")), Encoding.UTF8, "application/json"));                                 
                    if(message.IsSuccessStatusCode){
                        message = await client.PostAsync(url,new StringContent(JsonConvert.SerializeObject(new TagEndPointModel(registro.vida_util,t.currentLife.ToString(),"FUSAO")), Encoding.UTF8, "application/json"));                     
                        if(message.IsSuccessStatusCode){
                            message = await client.PostAsync(url,new StringContent(JsonConvert.SerializeObject(new TagEndPointModel(registro.vida_util_max,t.lifeCycle.ToString(),"FUSAO")), Encoding.UTF8, "application/json"));                         
                            if(message.IsSuccessStatusCode){
                                message = await client.PostAsync(url,new StringContent(JsonConvert.SerializeObject(new TagEndPointModel(registro.vida_util_unidade,t.unitOfMeasurement.ToString(),"FUSAO")), Encoding.UTF8, "application/json"));         
                                if(message.IsSuccessStatusCode){
                                    message = await client.PostAsync(url,new StringContent(JsonConvert.SerializeObject(new TagEndPointModel(registro.trigger,"1","FUSAO")), Encoding.UTF8, "application/json"));             
                                    return (registro,HttpStatusCode.OK);      
                                }else{
                                    return (registro,HttpStatusCode.BadRequest);      
                                }
                            }else{
                                return (registro,HttpStatusCode.BadRequest);      
                            }
                        }else{
                            return (registro,HttpStatusCode.BadRequest);      
                        }
                    }else{
                        return (registro,HttpStatusCode.BadRequest);      
                    }
                }else{
                    return (registro,HttpStatusCode.BadRequest);      
                }                                                      
            }catch{
                return (null,HttpStatusCode.BadRequest);            
            }    
        }    
        
        public async Task<(Tag_Address,HttpStatusCode)> desassociando(Tool t){ 
            try{                                                
                var registro = context.Tag_Address.FirstOrDefault(tag => tag.ferramenta_id.Equals(t.toolId.ToString()));                 
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                                    
                var url = new UriBuilder(_configuration["apiInterLevel"]);
                string urlTool = new UriBuilder(_configuration["apiTool"]).ToString();                             
                var result = await client.GetStringAsync(url+"?tag="+registro.vida_util_acumulado);            
                t.currentLife = Double.Parse(result.Replace("\"",""));      
                string strContent = JsonConvert.SerializeObject(t);
                var inputMessage = new HttpRequestMessage{
                    Content = new StringContent(strContent,Encoding.UTF8,"application/json")
                };     
                inputMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                 
                var message = await client.PutAsync(urlTool+"/"+t.toolId.ToString(), inputMessage.Content);      
                if (message.IsSuccessStatusCode){                                 
                    message = await client.PostAsync(url.ToString(),new StringContent(JsonConvert.SerializeObject(new TagEndPointModel(registro.trigger,"0","FUSAO")), Encoding.UTF8, "application/json"));                                      
                    if(message.IsSuccessStatusCode)
                        return (registro,HttpStatusCode.OK);
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