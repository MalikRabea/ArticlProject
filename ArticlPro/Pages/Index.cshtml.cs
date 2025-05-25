using ArticlPro.Core;
using ArticlPro.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArticlPro.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDataHelper<Core.Category> dataHelperForCategory;
        private readonly IDataHelper<AuthorPost> dataHelperForAuthorPost;
        public readonly int NoOfItem;

        public IndexModel(ILogger<IndexModel> logger,
            IDataHelper<Core.Category>dataHelperForCategory,
            IDataHelper<Core.AuthorPost> dataHelperForAuthorPost


            )
        {
            _logger = logger;
            this.dataHelperForCategory = dataHelperForCategory;
            this.dataHelperForAuthorPost = dataHelperForAuthorPost;
            NoOfItem = 6;

            ListOfCategory = new List<Core.Category>();
            ListOfAuthorPost = new List<Core.AuthorPost>();
        }

        public List<Core.Category> ListOfCategory { get; set; }

        public List<Core.AuthorPost> ListOfAuthorPost { get; set; }
        public void OnGet(string LoadState , string CategoryName,string search,int id)
        {
            GetAllCategory();
            if (LoadState == null || LoadState == "All")
            {
                GetAllAuthorPost();
            
            }
            else if (LoadState == "Category")
            {
                GetDateByCategoryName(CategoryName);

            }else if (LoadState == "Search")
            {
                SearchData(search);
            }
            else if (LoadState == "Next")
            {
                GetNextData(id);
            }
            else if (LoadState == "Prev")
            {
                GetNextData(id-NoOfItem);
            }


        }

        //Get the list of categories
        private void GetAllCategory()
        {
            ListOfCategory = dataHelperForCategory.GetAllData();
        }

        private void GetAllAuthorPost()
        {
            ListOfAuthorPost = dataHelperForAuthorPost.GetAllData().Take(NoOfItem).ToList();
        }

        private void GetDateByCategoryName(string CategoryName)
        {
            ListOfAuthorPost = dataHelperForAuthorPost.GetAllData().Where(x=>x.PostCategory==CategoryName).Take(NoOfItem).ToList();
        }

        private void SearchData(string SearchItem)
        {
            ListOfAuthorPost = dataHelperForAuthorPost.Search(SearchItem);
        }

        private void GetNextData(int id)
        {
            ListOfAuthorPost = dataHelperForAuthorPost.GetAllData().Where(x => x.Id > id).Take(NoOfItem).ToList();
        }
    }
}
