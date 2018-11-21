using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Api.Data;
using DatingApp.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Controllers {
    [Route ("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ValuesController : ControllerBase {
        public ValuesController (DataContext _db) => this._db = _db;
        private DataContext _db { get; set; }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetAsync () {
            return Ok (await _db.Values.ToListAsync ());
        }

        // GET api/values/5
        [AllowAnonymous]
        [HttpGet ("{id}")]
        public async Task<IActionResult> Get (int id) => Ok (await _db.Values.SingleOrDefaultAsync (a => a.Id == id));

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] string value) {
            try {
                Value NewObject = new Value () { Name = value };
                _db.Values.Add (NewObject);
                await _db.SaveChangesAsync ();
                return Ok (true);
            } catch {
                return Ok (false);
            }
        }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public void Put (int id, [FromBody] string value) { }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public void Delete (int id) { }
    }
}