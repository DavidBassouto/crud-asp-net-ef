using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using API.Context;


namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;

        public ContatoController(AgendaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();

            return CreatedAtAction(nameof(listById), new { id = contato.Id }, contato);

        }
        [HttpGet]
        public IActionResult listAllContacts()
        {
            var contatos = _context.Contatos.ToList();

            return Ok(contatos);

        }
        [HttpGet("{id}")]
        public IActionResult listById(int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato == null)
                return NotFound();

            return Ok(contato);

        }

        [HttpGet("ObterPorNome")]
        public IActionResult listByName(string nome)
        {
            var contatos = _context.Contatos.Where(contato => contato.Nome.Contains(nome));

            if (contatos == null)
                return NotFound();

            return Ok(contatos);

        }


        [HttpPatch("{id}")]
        public IActionResult updateById(int id, Contato contatoAtualizado)
        {
            var contatoToUpdate = _context.Contatos.Find(id);

            if (contatoToUpdate == null)
                return NotFound();

            contatoToUpdate.Nome = (contatoAtualizado.Nome == null) ? contatoToUpdate.Nome : contatoAtualizado.Nome;
            contatoToUpdate.Telefone = (contatoAtualizado.Telefone == null) ? contatoToUpdate.Telefone : contatoAtualizado.Telefone;
            contatoToUpdate.Ativo = contatoAtualizado.Ativo;


            _context.Contatos.Update(contatoToUpdate);
            _context.SaveChanges();

            return Ok(contatoToUpdate);
        }

        [HttpPatch("is_active/{id}")]
        public IActionResult updateIsActive(int id, bool isActive)
        {
            var contatoToUpdate = _context.Contatos.Find(id);

            if (contatoToUpdate == null)
                return NotFound();

            contatoToUpdate.Ativo = isActive;


            _context.Contatos.Update(contatoToUpdate);
            _context.SaveChanges();

            return Ok(contatoToUpdate);
        }

        [HttpDelete("{id}")]
        public IActionResult deleteById(int id)
        {
            var contato = _context.Contatos.Find(id);

            if (contato == null)
                return NotFound();

            _context.Contatos.Remove(contato);
            _context.SaveChanges();

            return NoContent();

        }


    }
}