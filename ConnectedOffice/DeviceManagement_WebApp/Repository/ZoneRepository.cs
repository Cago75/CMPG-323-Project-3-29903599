using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagement_WebApp.Repository
{
    public class ZoneRepository
    {
        private readonly ConnectedOfficeContext _context = new ConnectedOfficeContext();

        // GET: Zone
        public List<Zone> GetAll()
        {
            return _context.Zone.ToList();
        }

        // GET: Zone/Details/5
        public Zone Details(Guid? id)
        {

            return _context.Zone.FirstOrDefault(m => m.ZoneId == id);
        }

        // POST: Zone/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Create(Zone zone)
        {
            zone.ZoneId = Guid.NewGuid();
            _context.Zone.Add(zone);
            _context.SaveChangesAsync(); 
            //return RedirectToAction(nameof(Index));
        }

        // POST: Zone/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool Edit(Zone zone)
        {
             _context.Zone.Update(zone);
             _context.SaveChangesAsync();
            try
            {
                _context.Zone.Update(zone);
                _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneExists(zone.ZoneId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

        }

        // POST: Zone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public void DeleteConfirmed(Guid id)
        {
            var zone = _context.Zone.Find(id);
            _context.Zone.Remove(zone);
            _context.SaveChangesAsync();
        }
        private bool ZoneExists(Guid id)
        {
            return _context.Zone.Any(e => e.ZoneId == id);
        }

    }
}
