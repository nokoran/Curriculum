using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Curriculum.Models;
using Curriculum.Data;
using System.Threading.Tasks;
using System.Linq;
using Curriculum.Entities;
using Curriculum.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Curriculum.Controllers
{
    [Authorize (Roles = "Administrator")]
    public class RoomsController : Controller
    {
        private readonly RoomRepository _roomRepository;
        
        public RoomsController(RoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        // GET: CoursesController
        public ActionResult Index()
        {
            var rooms = _roomRepository.GetAllAsync().GetAwaiter().GetResult();
            return View(rooms);
        }
        
        // GET: RolesController
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Room());
        }

        // POST: RolesController
        [HttpPost]
        public ActionResult Create(Room room)
        {
            if(!_roomRepository.ExistsByNameAsync(room.name).GetAwaiter().GetResult())
            {
                _roomRepository.AddAsync(room).GetAwaiter().GetResult();
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Index");
        }
        
        // GET: RolesController/Edit/5
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var room = _roomRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // POST: RolesController/EditConfirmed/5
        [HttpPost]
        public ActionResult EditConfirmed(Guid id, Room room)
        {
            var existingRoom = _roomRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (existingRoom != null)
            {
                existingRoom.name = room.name;
                _roomRepository.UpdateAsync(existingRoom).GetAwaiter().GetResult();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        
        // GET: RolesController/Delete/5
        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var room = _roomRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // POST: RolesController/DeleteConfirmed/5
        [HttpPost]
        public ActionResult DeleteConfirmed(Guid id)
        {
            var course = _roomRepository.GetByIdAsync(id).GetAwaiter().GetResult();
            if (course != null)
            {
                _roomRepository.DeleteAsync(id).GetAwaiter().GetResult();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
