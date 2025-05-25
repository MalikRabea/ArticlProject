using System.Security.Claims;
using ArticlPro.Code;
using ArticlPro.Core;
using ArticlPro.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ArticlPro.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IDataHelper<AuthorPost> dataHelper;
        private readonly Code.FilesHelper fileHelper;
        private readonly IWebHostEnvironment webHost;
        private int pageItem;
        private Task<AuthorizationResult> result;
        private string UserId;
        private readonly IAuthorizationService authorizationService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IDataHelper<Author> dataHelperForAuthor;
        private readonly IDataHelper<Category> dataHelperForCategory;

        public PostController(
            IDataHelper<AuthorPost> dataHelper,
            IDataHelper<Author> dataHelperForAuthor,
            IDataHelper<Category> dataHelperForCategory,
            IWebHostEnvironment webHost, 
            IAuthorizationService authorizationService,
             UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)

        {
            this.dataHelper = dataHelper;
            this.webHost = webHost;
            this.dataHelperForAuthor = dataHelperForAuthor;
            this.dataHelperForCategory = dataHelperForCategory;
            this.authorizationService = authorizationService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            fileHelper = new Code.FilesHelper(this.webHost);
            pageItem = 5;
           


        }
        // GET: PostController
        public ActionResult Index(int? id)
        {
            setUser();
            if (result.Result.Succeeded)
            {
                //Admin
                if (id == 0 || id == null)
                {
                    return View(dataHelper.GetAllData().Take(pageItem));

                }
                else
                {
                    var data = dataHelper.GetAllData().Where(x => x.Id > id).Take(pageItem);
                    return View(data);
                }

            }
            else
            {
                //User
                if (id == 0 || id == null)
                {
                    return View(dataHelper.GetDataByUser(UserId).Take(pageItem));
                }
                else
                {
                    var data = dataHelper.GetDataByUser(UserId).Where(x => x.Id > id).Take(pageItem);
                    return View(data);
                }


            }
        }


        public ActionResult Search(string SearchItem)
        {
            setUser();
            if (result.Result.Succeeded) 
            {
                if (SearchItem == null)
                {
                    return View("Index", dataHelper.GetAllData());

                }
                else
                {
                    return View("Index", dataHelper.Search(SearchItem));
                }
            }
            else 
            {
                if (SearchItem == null)
                {
                    return View("Index", dataHelper.GetDataByUser(UserId));

                }
                else
                {
                    return View("Index", dataHelper.Search(SearchItem).Where(x=>x.UserId==UserId).ToList());
                }
            }
            
        }

        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            setUser();
            return View(dataHelper.Find(id));
        }

        // GET: PostController/Create
        public ActionResult Create()
        {
            setUser();
            return View();
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CoreView.AuthorPostView collection)
        {
            setUser();
            try
            {
                var post = new AuthorPost
                {
                    AddedDate = DateTime.Now,
                    Author = collection.Author,
                    AuthorId = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.Id).First(),
                    Category = collection.Category,
                    CategoryId = dataHelperForCategory.GetAllData().Where(x => x.Name == collection.PostCategory).Select(x => x.Id).First(),
                    FullName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.FullName).First(),
                    PostCategory = collection.PostCategory,
                    PostDescription = collection.PostDescription,
                    PostTitle = collection.PostTitle,
                    UserId = UserId,
                    UserName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.UserName).First(),
                    PostImageUrl = fileHelper.UploadFile(collection.PostImageUrl, "Images")
                };
                dataHelper.Add(post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Edit/5
        public ActionResult Edit(int id)
        {
            var authorpost = dataHelper.Find(id);
            CoreView.AuthorPostView authorPostView = new CoreView.AuthorPostView
            {
                AddedDate = authorpost.AddedDate,
                Author = authorpost.Author,
                AuthorId = authorpost.AuthorId,
                Category = authorpost.Category,
                CategoryId = authorpost.CategoryId,
                FullName = authorpost.FullName,
                PostCategory = authorpost.PostCategory,
                PostDescription = authorpost.PostDescription,
                PostTitle = authorpost.PostTitle,
                UserId = authorpost.UserId,
                UserName = authorpost.UserName,
                Id = authorpost.Id
            };
            return View(authorPostView);
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CoreView.AuthorPostView collection)
        {
            try
            {
                setUser();

                var Post = new AuthorPost
                {
                    AddedDate = DateTime.Now,
                    Author = collection.Author,
                    AuthorId = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.Id).First(),
                    Category = collection.Category,
                    CategoryId = dataHelperForCategory.GetAllData().Where(x => x.Name == collection.PostCategory).Select(x => x.Id).First(),
                    FullName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.FullName).First(),
                    PostCategory = collection.PostCategory,
                    PostDescription = collection.PostDescription,
                    PostTitle = collection.PostTitle,
                    UserId = UserId,
                    UserName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.UserName).First(),
                    PostImageUrl = fileHelper.UploadFile(collection.PostImageUrl, "Images"),
                    Id = collection.Id
                };
                dataHelper.Edit(id, Post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            
            return View(dataHelper.Find(id));
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, AuthorPost collection)
        {
            try
            {
                dataHelper.Delete(id);
                string filePath = "~/Images/" + collection.PostImageUrl;
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private void setUser()
        {

            result = authorizationService.AuthorizeAsync(User, "Admin");
            UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

        }
    }
}
