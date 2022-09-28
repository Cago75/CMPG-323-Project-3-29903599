using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DeviceManagement_WebApp.Repository
{
    public class DeviceRepository
    {
        private readonly ConnectedOfficeContext _context = new ConnectedOfficeContext();
       

        // GET: Device
        public List<Device> GetAll()
        {
            return _context.Device.Include(d => d.Category)
                                  .Include(d => d.Zone)
                                  .ToList();
            
        }

        // GET: Device/Details/5
        public Device Details(Guid? id)
        {

            return _context.Device.Include(d => d.Category)
                                  .Include(d => d.Zone)
                                  .FirstOrDefault(m => m.DeviceId == id);
        }

        // POST: Device/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Create(Device device)
        {
            device.DeviceId = Guid.NewGuid();
            _context.Device.Add(device);
            _context.SaveChangesAsync(); 
            //return RedirectToAction(nameof(Index));
        }

        // POST: Device/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool Edit(Device device)
        {
             _context.Device.Update(device);
             _context.SaveChangesAsync();
            try
            {
                _context.Device.Update(device);
                _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(device.DeviceId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

        }

        // POST: Device/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public void DeleteConfirmed(Guid id)
        {
            var device = _context.Device.Find(id);
            _context.Device.Remove(device);
            _context.SaveChangesAsync();
        }
        public SelectList listselect(String text)
        {
            SelectList newList;

            if (text == "Category") 
            {
                newList = new SelectList(_context.Category, text + "Id", text + "Name");
            }
            else if (text == "Zone")
            {
                newList = new SelectList(_context.Zone, text + "Id", text + "Name");
            }
            else
            {
                newList = null;
            }
            
            return newList;
        }
        private bool DeviceExists(Guid id)
        {
            return _context.Device.Any(e => e.DeviceId == id);
        }

    }
}
