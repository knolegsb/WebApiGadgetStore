using DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Store.Controllers
{
    public class GadgetsController : ApiController
    {
        private StoreDbContext db = new StoreDbContext();

        // GET: api/gadgets
        public IQueryable<Gadget> GetGadgets()

        {
            var gadgets = db.Gadgets;
            return gadgets;
        }

        // GET: api/gadgets/5
        [ResponseType(typeof(Gadget))]
        public async Task<IHttpActionResult> GetGadget(int id)
        {
            Gadget gadget = await db.Gadgets.FindAsync(id);
            if (gadget == null)
            {
                return NotFound();
            }
            return Ok(gadget);
        }

        // PUT: api/gadgets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGadget(int id, Gadget gadget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gadget.GadgetId)
            {
                return BadRequest();
            }

            db.Entry(gadget).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GadgetExists(id))
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

        // POST: api/gadgets
        [ResponseType(typeof(Gadget))]
        public async Task<IHttpActionResult> PostGadget(Gadget gadget)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Gadgets.Add(gadget);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = gadget.GadgetId }, gadget);
        }

        // DELETE: api/gadgets/5
        [ResponseType(typeof(Gadget))]
        public async Task<IHttpActionResult> DeleteGadget(int id)
        {
            Gadget gadget = await db.Gadgets.FindAsync(id);
            if (gadget == null)
            {
                return NotFound();
            }

            db.Gadgets.Remove(gadget);
            await db.SaveChangesAsync();

            return Ok(gadget);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GadgetExists(int id)
        {
            return db.Gadgets.Count(g => g.GadgetId == id) > 0;
        }
    }
}
