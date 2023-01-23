using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital_Managment.Models
{
    public class Pagination
    {
        public int Page { get; set; } = 1; // current page
        public int PageSize { get; set; } = 10; // number of items per page
        public int TotalPages { get; set; } // total number of pages
        public int TotalItems { get; set; } // total number of items
        public int StartItem { get; set; } // the index of the first item on the current page
        public string searchQuery{ get; set; }
        public string SelectedSortOption { get; set; }
        public List<SelectListItem> SortOptions { get; set; }
        public Pagination() {
            SortOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "price-asc", Text = "Price (Low to High)" },
            new SelectListItem { Value = "price-desc", Text = "Price (High to Low)" },
        };
        }
        public Pagination(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

        public void CalculateTotalPages(int totalItems)
        {
            TotalItems = totalItems;
            TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
        }

        public void CalculateStartItem()
        {
            StartItem = (Page - 1) * PageSize;
        }
    }

}
