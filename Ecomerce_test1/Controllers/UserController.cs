using Ecomerce_test1.Models;
using Ecomerce_test1.Models.Entities;
using Ecomerce_test1.Models.viewModel;
using Ecomerce_test1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Ecomerce_test1.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;
        private IWebHostEnvironment webHostEnvironment;
        UserManager<ApllecationUser> userManager;
        SignInManager<ApllecationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ICategoriesService categoryService;
        private readonly IProductService productService;

        public UserController(ApplicationDbContext _applicationDbContext,
                                 IWebHostEnvironment _webHostEnvironment,
                                 IProductService _productService,
                                 ICategoriesService _categoryService,
                                 UserManager<ApllecationUser> _userManager ,
                                 SignInManager<ApllecationUser> _signInManager,
                                 RoleManager<IdentityRole> roleManager)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
            this.roleManager = roleManager;
            this.applicationDbContext = _applicationDbContext;
            this.productService = _productService;
            this.categoryService = _categoryService;
            this.webHostEnvironment = _webHostEnvironment;
        }

        [HttpGet]
        public IActionResult login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                ApllecationUser user =await userManager.FindByNameAsync(loginModel.UserName);
                if (user != null)
                {
                  SignInResult signInResult = await signInManager.PasswordSignInAsync(user, loginModel.Password, loginModel.isPersist, false);
                    if (signInResult.Succeeded)
                    {
                        #region session 
                        string userId = user.Id;
                        ShoppingSession oldshoppingSession = applicationDbContext.ShoppingSessions.Where(s => s.UserId == userId).FirstOrDefault();
                        SessionInfo(oldshoppingSession);
                        #endregion
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("password", "your password is not correct");
                        return View(loginModel);
                    }
                }
            }

            return View(loginModel);
        }

        [HttpGet]
        public IActionResult regisrtation()
        {
            RegisterModel registermodel = new RegisterModel();
            ViewData["action"] = "Create Acount";
            return View(registermodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> regisrtation(RegisterModel registerModel , IFormFile postedFile)
        {
     
            
            if (ModelState.IsValid)
            {
                ApllecationUser apllecationUserRegestration = new ApllecationUser();
                
                #region product Images 
                
                if (postedFile != null)
                {
                    string wwwPath = this.webHostEnvironment.WebRootPath;
                    string contentPath = this.webHostEnvironment.ContentRootPath;

                    string path = Path.Combine(this.webHostEnvironment.WebRootPath, "img", "user");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileName = Path.GetFileName(postedFile.FileName);
                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    { postedFile.CopyTo(stream); }
                    apllecationUserRegestration.UserImage = fileName;
                   
                }

                #endregion
                apllecationUserRegestration.UserName = registerModel.UserName;
                apllecationUserRegestration.Email = registerModel.Email;

                IdentityResult result = await userManager.CreateAsync(apllecationUserRegestration, registerModel.Password);
                if (result.Succeeded)
                {
                    
                        //await userManager.AddToRoleAsync(ApllecationUserRegestration, "admin");
                    
                    // await userManager.AddToRoleAsync(ApllecationUserRegestration, "admin");
                    //List < string> roles = new System.Collections.Generic.List<string>();
                    //roles.Add("admin");
                    //roles.Add("cstm");
                    //await userManager.AddToRolesAsync(ApllecationUserRegestration, roles);
                    //await userManager.AddToRolesAsync(ApllecationUserRegestration, new[] { "sda", "dasd" });
                    //await userManager.AddToRolesAsync(ApllecationUserRegestration, new List<string>() { "sda", "dasd" });

                   // await signInManager.SignInAsync(ApllecationUserRegestration ,false);


                    return RedirectToAction("login");
                }
                else {
                    foreach (var error in result.Errors) {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            

            return View("regisrtation", registerModel);
        }
        public async Task<IActionResult> Logout(RegisterModel registerModel)
        { 
             await signInManager.SignOutAsync();
            HttpContext.Session.Remove("shoppingSession");
            HttpContext.Session.Remove("allProductQuantity") ;
            return RedirectToAction("Index", "Home");
        }





        //***************************************************************************************************
        //admin section 

        #region create users with role 
        [Authorize(Roles = "Admin")]
        [HttpGet]//from anchor tag
        public IActionResult AddAdmin()
        {
            ViewData["roles"]= roleManager.Roles.ToList();
            return View("regisrtation");
        }

        [HttpPost]//pree submit button
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdmin(RegisterModel registerModel,string[] roles)
        {
            if (ModelState.IsValid == true)
            {
                //save object db
               
                ApllecationUser ApllecationUserRegestration = new ApllecationUser();
                ApllecationUserRegestration.UserName = registerModel.UserName;
                ApllecationUserRegestration.Email = registerModel.Email;
                IdentityResult result = await userManager.CreateAsync(ApllecationUserRegestration, registerModel.Password);//hash passwo
                if (result.Succeeded)
                {
                    if (roles!= null )
                    {
                        foreach (string roleID in roles)
                        {
                            IdentityRole role = await roleManager.FindByIdAsync(roleID);
                            //enroll in role
                            await userManager.AddToRoleAsync(ApllecationUserRegestration, role.Name);
                        }
                        
                     
                    }
                   
                    //save sucess
                    await signInManager.SignInAsync(ApllecationUserRegestration, false);//cookie
                    return RedirectToAction("Index", "Department");
                }
                else
                {
                    foreach (var Error in result.Errors)
                    {
                        ModelState.AddModelError("", Error.Description);
                    }
                }
            }
            return View(registerModel);
        }
        #endregion

        #region Admin page
        [Authorize(Roles = "Admin")]
        public async Task <IActionResult> Admin()
        {
             AdminPage adminPage = new AdminPage();


            adminPage.productNumber = productService.GetAll().Count();
            adminPage.categoryNumber = applicationDbContext.Categories.Count();
            adminPage.UserNumber = userManager.Users.Count();
            var reseller = await userManager.GetUsersInRoleAsync("Reseller");
            string rouleIdResseler = applicationDbContext.Roles.Where(s => s.Name == "Reseller").Select(s => s.Id).FirstOrDefault().ToString();
            string rouleIdAdmin = applicationDbContext.Roles.Where(s => s.Name == "ADMIN").Select(s => s.Id).FirstOrDefault().ToString();

            adminPage.resellerNumber = applicationDbContext.UserRoles.Select(s=>s.RoleId== rouleIdResseler).Count();
            adminPage.AdminNumber = applicationDbContext.UserRoles.Select(s => s.RoleId == rouleIdAdmin).Count();


            foreach (var category in categoryService.GetAll())
            {
                adminPage.prCategories[category.Name] = applicationDbContext.ProductCategories.Where(s => s.CategoryId == category.Id).Count();
            }
     

            return View(adminPage);
        }




        #endregion

        //***************************************************
        //add role 

        [Authorize(Roles = "Admin")]
        [HttpGet]//from anchor tag
        public IActionResult NEw()
        {
            return View();
        }

        [HttpPost]//pree submit button
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NEw(string RoleName)
        {
            if (RoleName != null)
            {
                IdentityRole role = new IdentityRole(RoleName);
                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return View();
                }
                else
                {
                    TempData["msg"] = result.Errors.FirstOrDefault().Description;
                }
            }
            ViewData["RoleName"] = RoleName;
            return View();
        }

        private void SessionInfo(ShoppingSession oldshoppingSession) {
            if (oldshoppingSession != null)
            {
                HttpContext.Session.SetInt32("shoppingSession", oldshoppingSession.Id);
                List<CartItem> cartItems = applicationDbContext.CartItems.Include(a => a.Product).Where(s => s.SessionId == oldshoppingSession.Id).ToList();
                int totalPrice = 0;
                int allProductQuantity = 0;
                for (int i = 0; i < cartItems.Count; i++)
                {
                    CartItem cartItem = cartItems[i];
                    int price = applicationDbContext.Products.Where(s => s.Id == cartItem.ProductId).FirstOrDefault().Price;
                    totalPrice += price * cartItem.Quantity;
                    allProductQuantity += cartItem.Quantity;
                }
                HttpContext.Session.SetInt32("allProductQuantity", allProductQuantity);
            }









        }




    }
}
