﻿using Clinique2000_Core.Models;
using Clinique2000_Core.ViewModels;
using Clinique2000_Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Clinique2000_MVC.Areas.Patients.Controllers
{
    [Area("Patients")]
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        public IClinique2000Services _services { get; set; }

        public PatientsController(
            IClinique2000Services service,
            UserManager<IdentityUser> userManager
            )
        {
            _services = service;
            _userManager = userManager;
        }


        // GET: PatientsController
        public async Task<IActionResult> Index()
        {
            //return View(); 
            var listeDePatients = await _services.patient.ObtenirToutAsync();

            return View("Index", listeDePatients);
        }

        // GET: PatientsController/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null || await _services.patient.ObtenirToutAsync() == null)
            {
                return View("NotFound");
            }
            var patientDetails = await _services.patient.ObtenirParIdAsync(id);
            if (patientDetails == null)
            {
                return View("NotFound");
            }
            return View(patientDetails);
        }

        // GET: PatientsController/Create
        public async Task<IActionResult> Create()
        {
                string courrielUserAuth = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(courrielUserAuth);
                bool estPatient = await _services.patient.UserEstPatientAsync(user.Id);

                if (!estPatient)
                {
                    var patientModel = new Patient
                    {
                        UserId = user.Id
                    };

                    return View(patientModel);
                }
                var patient = await _services.patient.GetPatientParUserIdAsync(user.Id);
                return RedirectToAction("Details", "Patients", new { id = patient.PatientId });
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,NAM,CodePostal,DateDeNaissance,Age,PatientId,Nom,Prenom,Genre")] Patient patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _services.patient.EnregistrerOuModifierPatient(patient);
                    return RedirectToAction(nameof(Index));
                }
                return View(patient);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View(patient);
            }
        }

        // GET: PatientsController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null || await _services.patient.ObtenirToutAsync() == null)
                {
                    return View("NotFound");
                }
                var patient = await _services.patient.ObtenirParIdAsync(id);

                if (patient == null)
                {
                    return View("NotFound");
                }
                return View(patient);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: PatientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientId,Nom,Prenom,Genre,NAM,CodePostal,DateDeNaissance,Age,UserId")] Patient patient)
        {
            try
            {
                if (id != patient.PatientId)
                {
                    return View("NotFound");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        await _services.patient.EnregistrerOuModifierPatient(patient);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!await _services.patient.VerifierExistencePatientParNAM(patient.NAM))
                        {
                            return View("NotFound");
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(patient);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View(patient);
            }
        }

        // GET: PatientsController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null || await _services.patient.ObtenirToutAsync() == null)
                {
                    return View("NotFound");
                }
                var patient = await _services.patient.ObtenirParIdAsync(id);

                if (patient != null)
                {
                    return View(patient);
                }
                else
                    return View("NotFound");
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }


        // POST: PatientsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int patientId)
        {
            try
            {
                var patient = await _services.patient.ObtenirParIdAsync(patientId);
                if (patient != null)
                {
                    await _services.patient.SupprimerAsync(patientId);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }
        }
    }
}
