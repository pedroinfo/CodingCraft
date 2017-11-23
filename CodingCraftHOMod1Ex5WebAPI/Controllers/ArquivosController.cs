using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CodingCraftHOMod1Ex5WebAPI.Models;
using CodingCraftHOMod1Ex5WebAPI.Models.Contexto;
using System.Web;
using System.IO;
using System.Net.Http.Headers;
using CodingCraftHOMod1Ex5WebAPI.Streaming;

namespace CodingCraftHOMod1Ex5WebAPI.Controllers
{
    public class ArquivosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public IEnumerable<dynamic> GetArquivos()
        {
            return db.Arquivos.Include(a => a.ArquivoTipo).ToList();
        }
        
        // GET: api/Arquivos/5
        [ResponseType(typeof(Arquivo))]
        public async Task<IHttpActionResult> GetArquivo(int id)
        {
            Arquivo arquivo = await db.Arquivos.FindAsync(id);
            if (arquivo == null)
            {
                return NotFound();
            }
            return Ok(arquivo);
        }


        [HttpGet]
        [Route("api/arquivos/download/{id}")]
        public async Task<HttpResponseMessage> Download(int id)
        {
            Arquivo arquivo = await db.Arquivos.FindAsync(id);

            var registro = new ArquivoRegistro
            {
                ArquivoId = id,
                DataDownload = DateTime.Now
            };

            db.ArquivosRegistros.Add(registro);
            await db.SaveChangesAsync();

            var path = HttpContext.Current.Server.MapPath("~/Uploads/" + arquivo.Path); 
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = Path.GetFileName(path);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentLength = stream.Length;
            return result;
        }

        [HttpGet]
        [Route("api/arquivos/versoes/{id}")]
        public IEnumerable<dynamic> Versoes(int id)
        {
            return db.ArquivosVersoes.Include(a=>a.ArquivoTipo).Where(x=>x.ArquivoId == id).ToList();
        }


        [HttpGet]
        [Route("api/videos/video/{path}")]
        public HttpResponseMessage GetVideo(string path)
        {
            string path1 = "/video/" + path;
            var video = new VideoStream(path1);
            var response = Request.CreateResponse();
            response.Content = new PushStreamContent(video.WriteToStream, new MediaTypeHeaderValue("video/" + ".mp4"));
            return response;
        }

        [HttpGet]
        [Route("api/audios/audio/{path}")]
        public HttpResponseMessage GetAudio(string path)
        {
            string path1 = "/audio/" + path;
            var audio = new VideoStream(path1);
            var response = Request.CreateResponse();
            response.Content = new PushStreamContent(audio.WriteToStream, new MediaTypeHeaderValue("video/" + ".mp3"));
            return response;
        }



        // PUT: api/Arquivos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutArquivo(int id, Arquivo arquivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != arquivo.ArquivoId)
            {
                return BadRequest();
            }

            var httpRequest = HttpContext.Current.Request;
            var docfiles = new List<string>();
            foreach (string file in httpRequest.Files)
            {
                var postedFile = httpRequest.Files[file];
                var filePath = "";
                var resultAdd = AddFile(postedFile);
                arquivo.Path = resultAdd.Path;
                arquivo.ArquivoTipoId = resultAdd.ArquivoTipoId;
                docfiles.Add(filePath);
            }

            try
            {
                using (var context = new ApplicationDbContext())
                {
                        var versaoAntiga = new ArquivoVersao();
                        var registroAtual = await context.Arquivos.FindAsync(id);

                        versaoAntiga.ArquivoId = registroAtual.ArquivoId;
                        versaoAntiga.ArquivoTipoId = registroAtual.ArquivoTipoId;
                        versaoAntiga.NomeArquivo = registroAtual.NomeArquivo;
                        versaoAntiga.Path = registroAtual.Path;
                        versaoAntiga.DataEntrada = registroAtual.DataEntrada;
                        context.ArquivosVersoes.Add(versaoAntiga);
                        await context.SaveChangesAsync();
                }

                arquivo.DataEntrada = DateTime.Now;
                db.Entry(arquivo).State = EntityState.Modified;
                await  db.SaveChangesAsync();


            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArquivoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Arquivos
        [ResponseType(typeof(Arquivo))]
        public async Task<IHttpActionResult> PostArquivo(Arquivo arquivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            
            var docfiles = new List<string>();
            foreach (string file in httpRequest.Files)
            {
                var postedFile = httpRequest.Files[file];
                var filePath = "";

                var resultAdd = AddFile(postedFile);
                
                arquivo.ArquivoTipoId = resultAdd.ArquivoTipoId;
                arquivo.Path = resultAdd.Path;
                arquivo.DataEntrada = DateTime.Now;

                docfiles.Add(filePath);
            }
            result = Request.CreateResponse(HttpStatusCode.Created, docfiles);

            if (result == null)
             result = Request.CreateResponse(HttpStatusCode.BadRequest);
            
            db.Arquivos.Add(arquivo);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = arquivo.ArquivoId }, arquivo);
        }

        private Arquivo AddFile(HttpPostedFile postedFile)
        {
            string type = postedFile.ContentType;

            string filePath = "";
            Arquivo arquivo = new Arquivo();

            if (type.Contains("image"))
            {
                filePath = HttpContext.Current.Server.MapPath("~/Uploads/image/" + postedFile.FileName);
                arquivo.ArquivoTipoId = db.ArquivosTipos.FirstOrDefault(x => x.Descricao.Equals("image")).ArquivoTipoId;
            }
            else if (type.Contains("video"))
            {
                filePath = HttpContext.Current.Server.MapPath("~/Uploads/video/" + postedFile.FileName);
                arquivo.ArquivoTipoId = db.ArquivosTipos.FirstOrDefault(x => x.Descricao.Equals("video")).ArquivoTipoId;
            }
            else if (type.Contains("audio"))
            {
                filePath = HttpContext.Current.Server.MapPath("~/Uploads/audio/" + postedFile.FileName);
                arquivo.ArquivoTipoId = db.ArquivosTipos.FirstOrDefault(x => x.Descricao.Equals("audio")).ArquivoTipoId;
            }
            else if (type.Contains("text"))
            {
                filePath = HttpContext.Current.Server.MapPath("~/Uploads/text/" + postedFile.FileName);
                arquivo.ArquivoTipoId = db.ArquivosTipos.FirstOrDefault(x => x.Descricao.Equals("text")).ArquivoTipoId;
            }
            else if (type.Contains("word"))
            {
                filePath = HttpContext.Current.Server.MapPath("~/Uploads/word/" + postedFile.FileName);
                arquivo.ArquivoTipoId = db.ArquivosTipos.FirstOrDefault(x => x.Descricao.Equals("word")).ArquivoTipoId;
            }
            else if (type.Contains("excel") || type.Contains("spreadsheet"))
            {
                filePath = HttpContext.Current.Server.MapPath("~/Uploads/excel/" + postedFile.FileName);
                arquivo.ArquivoTipoId = db.ArquivosTipos.FirstOrDefault(x => x.Descricao.Equals("excel")).ArquivoTipoId;
            }
            else if (type.Contains("powerpoint"))
            {
                filePath = HttpContext.Current.Server.MapPath("~/Uploads/ppt/" + postedFile.FileName);
                arquivo.ArquivoTipoId = db.ArquivosTipos.FirstOrDefault(x => x.Descricao.Equals("powerpoint")).ArquivoTipoId;
            }
            else
            {
                filePath = HttpContext.Current.Server.MapPath("~/Uploads/outros/" + postedFile.FileName);
                arquivo.ArquivoTipoId = db.ArquivosTipos.FirstOrDefault(x => x.Descricao.Equals("outros")).ArquivoTipoId;
            }
            arquivo.Path = Path.GetFileName(Path.GetDirectoryName(filePath)) + @"/" + Path.GetFileName(filePath);

            postedFile.SaveAs(filePath);
            return arquivo;
        }

        // DELETE: api/Arquivos/5
        [ResponseType(typeof(Arquivo))]
        public async Task<IHttpActionResult> DeleteArquivo(int id)
        {
            Arquivo arquivo = await db.Arquivos.FindAsync(id);
            if (arquivo == null)
            {
                return NotFound();
            }

            db.Arquivos.Remove(arquivo);
            await db.SaveChangesAsync();

            try
            {
                File.Delete(HttpContext.Current.Server.MapPath("~/Uploads/" + arquivo.Path));
            }
            catch (Exception)
            {

            }

            return Ok(arquivo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArquivoExists(int id)
        {
            return db.Arquivos.Count(e => e.ArquivoId == id) > 0;
        }
    }
}