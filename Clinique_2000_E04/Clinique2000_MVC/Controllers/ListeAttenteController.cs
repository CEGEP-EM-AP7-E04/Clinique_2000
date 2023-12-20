﻿using Clinique2000_Core.Models;
using Clinique2000_Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Clinique2000_MVC.Controllers
{
    public class ListeAttenteController : Controller
    {

        public Clinique2000Services _services { get; set; }

        public ListeAttenteController(Clinique2000Services service)
        {
            _services = service;
        }




        // GET: ListeAttenteController
        public async Task<ActionResult> Index()
        {

            IReadOnlyList<ListeAttente> listListAttente = await _services.listeAttente.ObtenirToutAsync();
            return View(listListAttente);

        }


        public async Task<ActionResult> Details(int id)
        {
            ListeAttente listeAttente = await _services.listeAttente.ObtenirParIdAsync(id);
            return View(listeAttente);
        }


        // GET: ListeAttenteController/Create
        public async Task<ActionResult> Create(int id)
        {

            ListeAttente list = await _services.listeAttente.ObtenirParIdAsync(id);
            return View(list);

        }

        // POST: ListeAttenteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ListeAttente listeAttente)
        {

            if (ModelState.IsValid)
            {
                await _services.listeAttente.CreerAsync(listeAttente);
                RedirectToAction("Index");
            }


            return View(listeAttente);
        }
        [HttpGet]
        // GET: ListeAttenteController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            
             ListeAttente list = await _services.listeAttente.ObtenirParIdAsync(id);
             return View(list);
         
      
        }

        // POST: ListeAttenteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ListeAttente listeAttente)
        {
            if (ModelState.IsValid)
            {
               await _services.listeAttente.EditerAsync(listeAttente);
                RedirectToAction("Index");
            }
          

            return View(listeAttente);
        }

        // GET: ListeAttenteController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            ListeAttente list=  await _services.listeAttente.ObtenirParIdAsync(id);

            return View(list);
        }

        // POST: ListeAttenteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(ListeAttente listeAttente)
        {
            if (ModelState.IsValid)
            {
                await _services.listeAttente.SupprimerAsync(listeAttente.ListeAttenteID);

            }

            return View(listeAttente);
        }
    }
}