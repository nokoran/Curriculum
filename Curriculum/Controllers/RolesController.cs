using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Curriculum.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public RolesController( RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        
        // GET: RolesController
        public ActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        
        // GET: RolesController
        [HttpGet]
        public ActionResult Create()
        {
            return View(new IdentityRole());
        }

        // POST: RolesController
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            if(!_roleManager.RoleExistsAsync(role.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(role).GetAwaiter().GetResult();
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Index");
        }
        
        // GET: RolesController/Edit/5
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var role = _roleManager.FindByIdAsync(id).GetAwaiter().GetResult();
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: RolesController/EditConfirmed/5
        [HttpPost]
        public ActionResult EditConfirmed(string id, IdentityRole role)
        {
            var existingRole = _roleManager.FindByIdAsync(id).GetAwaiter().GetResult();
            if (existingRole != null)
            {
                existingRole.Name = role.Name;
                existingRole.NormalizedName = role.NormalizedName;
                _roleManager.UpdateAsync(existingRole).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }
        
        // GET: RolesController/Delete/5
        [HttpGet]
        public ActionResult Delete(string id)
        {
            var role = _roleManager.FindByIdAsync(id).GetAwaiter().GetResult();
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

// POST: RolesController/DeleteConfirmed/5
        [HttpPost]
        public ActionResult DeleteConfirmed(string id)
        {
            var role = _roleManager.FindByIdAsync(id).GetAwaiter().GetResult();
            if (role != null)
            {
                _roleManager.DeleteAsync(role).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }
    }
}
